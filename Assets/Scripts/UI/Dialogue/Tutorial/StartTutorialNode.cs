using StarterAssets;
using UnityEngine;

public class StartTutorialNode : MonoBehaviour
{
    [SerializeField] private TutorialNodeParser tutorialManager;
    [SerializeField] private DialogueGrapgh tutorialDialogue;

    private ThirdPersonController personController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
            tutorialDialogue.TryFindStartNode();
            personController = controller;
            TutorialNodeParser.EndDialog += SetPlayerActive;
            SetPlayerInactive();
            tutorialManager.StartNode(tutorialDialogue);
        }
    }

    private void SetPlayerInactive()
    {
            personController.CanMove = false;
            personController.GetComponent<MeleeAtack>().enabled = false;
    }

    private void SetPlayerActive()
    {
        personController.CanMove = true;
        personController.GetComponent<MeleeAtack>().enabled = true;
        TutorialNodeParser.EndDialog -= SetPlayerActive;
        Destroy(this);
    }
}
