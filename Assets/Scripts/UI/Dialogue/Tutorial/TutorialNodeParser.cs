using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class TutorialNodeParser : MonoBehaviour
{
    [Header("Tutorial windows")]
    [SerializeField] private Text tutorialText;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private StarterAssetsInputs playerInputs;
    [SerializeField] private float dialogueDelay = 0.1f;

    private DialogueGrapgh graph;

    public static event Action EndDialog;

    private Coroutine parser;

    private void Start()
    {
        FindStartNode(graph);
    }

    private void FindStartNode(DialogueGrapgh grap)
    {
        if (grap != null)
        {
            foreach (BaseNode b in grap.nodes)
            {
                if (b.GetString() == "Start")
                {
                    graph.current = b;
                    break;
                }
            }
        }
    }

    public void StartNode(DialogueGrapgh dialogueGrapgh)
    {
        graph = dialogueGrapgh;
        StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode()
    {
        BaseNode node = graph.current;
        string data = node.GetString();
        string[] dataParts = data.Split('/');
        if (dataParts[0] == "Start")
        {
            tutorialPanel.SetActive(true);
            NextNode("exit");
        }
        if (dataParts[0] == "Tutorial")
        {
            tutorialImage.sprite = node.GetSprite();
            tutorialText.text = dataParts[1];
            yield return new WaitForSeconds(dialogueDelay);
            yield return new WaitUntil(() => playerInputs.atack);
            NextNode("exit");
        }
        if (dataParts[0] == "End")
        {
            FindStartNode(node.GetGrapgh());
            tutorialPanel.SetActive(false);
            EndDialog?.Invoke();
        }
    }

    private void NextNode(string fieldName)
    {
        if (parser != null)
        {
            StopCoroutine(parser);
            parser = null;
        }
        foreach (NodePort port in graph.current.Ports)
        {
            if (port.fieldName == fieldName)
            {
                graph.current = port.Connection.node as BaseNode;
                break;
            }
        }
        parser = StartCoroutine(ParseNode());
    }
}
