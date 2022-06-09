using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    [SerializeField] private Text speaker;
    [SerializeField] private Text dialogue;
    [SerializeField] private Image speakerImage;
    [SerializeField] private StarterAssetsInputs playerInputs;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private float dialogueDelay = 0.1f;

    public DialogueGrapgh graph;

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

    public void StartDialogue()
    {
        StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode()
    {
        BaseNode node = graph.current;
        string data = node.GetString();
        string[] dataParts = data.Split('/');
        if (dataParts[0] == "Start")
        {
            dialoguePanel.SetActive(true);
            NextNode("exit");
        }
        if(dataParts[0] == "DialogueNode")
        {
            dialogue.text = dataParts[2];
            yield return new WaitForSeconds(dialogueDelay);
            yield return new WaitUntil(() => playerInputs.atack);
            NextNode("exit");
        }
        if(dataParts[0] == "End")
        {            
            FindStartNode(node.GetGrapgh());
            dialoguePanel.SetActive(false);
            EndDialog?.Invoke();
        }
    }

    public void NextNode(string fieldName)
    {
        if(parser != null)
        {
            StopCoroutine(parser);
            parser = null;
        }
        foreach(NodePort port in graph.current.Ports)
        {
            if(port.fieldName == fieldName)
            {
                graph.current = port.Connection.node as BaseNode;
                break;
            }
        }
        parser = StartCoroutine(ParseNode());
    }
}
