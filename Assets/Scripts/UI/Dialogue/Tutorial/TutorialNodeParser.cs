using StarterAssets;
using System;
using System.Collections;
using System.Text;
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
    private ThirdPersonController playersController;

    private void Start()
    {
        FindStartNode(graph);
        if (playerInputs.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
            playersController = controller;
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
        graph.TryFindStartNode();

            playersController.CanMove = false;
            playersController.GetComponent<MeleeAtack>().enabled = false;

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
            tutorialText.text = CenterText(dataParts[1]);
            yield return new WaitForSeconds(dialogueDelay);
            yield return new WaitUntil(() => playerInputs.atack);
            NextNode("exit");
        }
        if (dataParts[0] == "End")
        {
            FindStartNode(node.GetGrapgh());
            tutorialPanel.SetActive(false);
            EndDialog?.Invoke();

            playersController.CanMove = true;
            playersController.GetComponent<MeleeAtack>().enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    private string CenterText(string inputLine)
    {
        var stringList = inputLine.Split(' ');
        StringBuilder builder = new StringBuilder();
        foreach(var element in stringList)
        {
                if (element.StartsWith("“"))
                {
                    builder.Append("<color=red>" + element + "</color>" + " ");
                }
                else if(element.EndsWith("."))
                {
                    builder.Append(element + "\n");
                }
                else
                {
                    builder.Append(element+ " ");
                }
        }
        return builder.ToString();
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
