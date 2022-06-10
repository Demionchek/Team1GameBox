using StarterAssets;
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

    private DialogueGrapgh graph;

    private Coroutine parser;
    private ThirdPersonController playersController;

    private void Start()
    {
        if(playerInputs.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
            playersController = controller;
        }
    }

    public void StartDialogue(DialogueGrapgh dialogueGrapgh)
    {
        graph = dialogueGrapgh;
        graph.TryFindStartNode();
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

            playersController.CanMove = false;
            playersController.GetComponent<MeleeAtack>().enabled = false;

            NextNode("exit");
        }
        if(dataParts[0] == "DialogueNode")
        {
            dialogue.text = dataParts[2];
            yield return new WaitForSeconds(4f);
            NextNode("exit");
        }
        if(dataParts[0] == "End")
        { 
            dialoguePanel.SetActive(false);
<<<<<<< Updated upstream
=======
            EndDialog?.Invoke();
            playersController.CanMove = true;
            playersController.GetComponent<MeleeAtack>().enabled = true;
>>>>>>> Stashed changes
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
