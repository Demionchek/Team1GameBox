using StarterAssets;
using UnityEngine;

public class StartTutorialNode : MonoBehaviour
{
    [SerializeField] private TutorialNodeParser tutorialManager;
    [SerializeField] private DialogueGrapgh tutorialDialogue;

    private void OnCollisionEnter(Collision collision)
    {
        if(TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
            tutorialManager.StartNode(tutorialDialogue);
        }
    }
}
