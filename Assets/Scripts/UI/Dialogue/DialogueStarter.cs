using StarterAssets;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private NodeParser dialogueManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
            dialogueManager.StartDialogue();
            Destroy(this);
        }
    }
}
