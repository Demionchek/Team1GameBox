using StarterAssets;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class NodeParser : MonoBehaviour
{
    [SerializeField] private Text dialogue;
    [SerializeField] private StarterAssetsInputs playerInputs;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private float dialogueDelay = 0.5f;

    private bool _next; 

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

    public void Next()
    {
        _next = true;
    }
    public void StartDialogue(DialogueGrapgh dialogueGrapgh)
    {
        graph = dialogueGrapgh;
        //graph.TryFindStartNode();
        FindStartNode(graph);

        playersController.CanMove = false;
        playersController.GetComponent<MeleeAtack>().enabled = false;

        StartCoroutine(ParseNode());
    }

    private IEnumerator ParseNode()
    {
        BaseNode node = graph.current;
        string data = node.GetString();
        string[] dataParts = data.Split('/');
        Time.timeScale = 0f;
        if (dataParts[0] == "Start")
        {
            dialoguePanel.SetActive(true);
            NextNode("exit");
        }
        if(dataParts[0] == "DialogueNode")
        {
            dialogue.text = dataParts[2];
            yield return new WaitForSecondsRealtime(dialogueDelay);
            yield return new WaitUntil(() => playerInputs.atack);
            _next = false;
            NextNode("exit");
        }
        if(dataParts[0] == "End")
        { 
            dialoguePanel.SetActive(false);
            EndDialog?.Invoke();
            Time.timeScale = 1f;
            playersController.CanMove = true;
            playersController.GetComponent<MeleeAtack>().enabled = true;
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
