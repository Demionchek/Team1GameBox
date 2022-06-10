using StarterAssets;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private NodeParser dialogueManager;
<<<<<<< Updated upstream
=======
    [SerializeField] private bool isStartingDialog;
    [SerializeField] private DialogueGrapgh dialogueToStart;
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
>>>>>>> Stashed changes

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
<<<<<<< Updated upstream
            dialogueManager.StartDialogue();
            Destroy(this);
=======
            NodeParser.EndDialog += SetPlayerActive;
            controller1.CanMove = false;
            controller1.GetComponent<MeleeAtack>().enabled = false;
            personController = controller1;
            dialogueManager.StartDialogue(dialogueToStart);
>>>>>>> Stashed changes
        }
    }
}
