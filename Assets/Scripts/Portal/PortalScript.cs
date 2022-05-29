using System.Collections;
using UnityEngine;
using StarterAssets;
using System;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private Transform teleportationTarget;
    [SerializeField] 
    bool isTeleported = false;

    public static event Action<bool> PlayerInTrigger;

    private void Start()
    {
        PlayerInTrigger(false);
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
            PlayerInTrigger(true);

            if (input.interact && !isTeleported)
            {
                other.transform.position = teleportationTarget.position;

                if (teleportationTarget.TryGetComponent(out PortalScript portalScript))
                {
                    portalScript.SetTeleportTimer();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController thirdPerson))
        {
            PlayerInTrigger(false);
        }
    }
}
