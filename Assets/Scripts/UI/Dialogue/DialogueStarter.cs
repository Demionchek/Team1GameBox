using StarterAssets;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private NodeParser dialogueManager;
    [SerializeField] private bool isFinal;
    [SerializeField] private DialogueGrapgh dialogueToStart;
    [SerializeField] private ThirdPersonController personController;
    [SerializeField] private FadeInOut inOut;
    [SerializeField] private Health yaga;
    [SerializeField] private GameObject yagaUI;
    [SerializeField] private EnemySpawner spawner; 

    private void Start()
    {
        if (yaga != null)
        {
            StartCoroutine(CheckYagaHealth());
        }
    }

    private IEnumerator CheckYagaHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (yaga.Hp < 1)
            {
                StartFinal();
                break;
            }

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
            dialogueManager.StartDialogue(dialogueToStart);
        }
    }

    public void StartFinal()
    {
        NodeParser.EndDialog += SetPlayerActive;
        if (inOut != null)
        {
            inOut.FadeIn(1f);
            yagaUI.SetActive(false);
            spawner.OffAllEnemies();
        }
        personController.CanMove = false;
        personController.GetComponent<MeleeAtack>().enabled = false;
        dialogueManager.StartDialogue(dialogueToStart);
    }

    private void SetPlayerActive()
    {
        personController.CanMove = true;
        personController.GetComponent<MeleeAtack>().enabled = true;
        NodeParser.EndDialog -= SetPlayerActive;
        if (isFinal && inOut != null)
        {
            inOut.FadeInAndLoadScene(0);
        }
        Destroy(this);
    }
}
