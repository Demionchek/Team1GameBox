using System.Collections;
using UnityEngine;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private Transform teleportationTarget;
    [SerializeField] private bool nextLevel;
    [SerializeField] private int nextSceneNum;
    bool isTeleported = false;

    public static event Action<bool> PlayerInTrigger;

    private void Start()
    {
        PlayerInTrigger?.Invoke(false);
    }

    public void SetTeleportTimer()
    {
        isTeleported = true;
        StartCoroutine(TeleportTimerCorutine());
    } 

    private IEnumerator TeleportTimerCorutine()
    {
        yield return new WaitForSeconds(1f);
        isTeleported = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.TryGetComponent(out ThirdPersonController thirdPerson))
        {
            PlayerInTrigger?.Invoke(true);

            if (input.interact && !isTeleported && !nextLevel)
            {
                other.transform.position = teleportationTarget.position;

                if (teleportationTarget.TryGetComponent(out PortalScript portalScript))
                {
                    portalScript.SetTeleportTimer();
                }
            }

            if (input.interact && !isTeleported && nextLevel)
            {
                SceneManager.LoadScene(nextSceneNum);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController thirdPerson))
        {
            PlayerInTrigger?.Invoke(false);
        }
    }
}
