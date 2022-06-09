using StarterAssets;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private NodeParser dialogueManager;
    [SerializeField] private ThirdPersonController controller;
    [SerializeField] private bool isStartingDialog;
    private ThirdPersonController personController;

    private void Start()
    {
        if (isStartingDialog)
        {
            //NodeParser.EndDialog += SetPlayerActive;
            //controller.CanMove = false;
            //controller.GetComponent<MeleeAtack>().enabled = false;
            //personController = controller;
            //dialogueManager.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller1))
        {
            NodeParser.EndDialog += SetPlayerActive;
            controller1.CanMove = false;
            controller1.GetComponent<MeleeAtack>().enabled = false;
            personController = controller1;
            dialogueManager.StartDialogue();
        }
    }

    private void SetPlayerActive()
    {
        personController.CanMove = true;
        personController.GetComponent<MeleeAtack>().enabled = true;
        NodeParser.EndDialog -= SetPlayerActive;
        Destroy(this);
    }
}
