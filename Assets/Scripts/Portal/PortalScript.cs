using System.Collections;
using UnityEngine;
using StarterAssets;
using System;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private Transform _teleportationTarget;
    [SerializeField] private bool _isNextLevel;
    [SerializeField] private int _nextLevelNum;
    private bool _isTeleported = false;
    private int counter;

    private const float kTime = 1f;

    public static event Action Teleported;
    public static event Action<int> LoadNextLevel;

    private void Start()
    {
        PortalScript.Teleported += SetTeleportTimer;
        counter = 0;
    }

    public void SetTeleportTimer()
    {
        _isTeleported = true;
        StartCoroutine(TeleportTimerCorutine());
    } 

    private IEnumerator TeleportTimerCorutine()
    {
        yield return new WaitForSeconds(kTime);
        counter = 0;
        _isTeleported = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.TryGetComponent(out ThirdPersonController thirdPerson))
        {

            if (_input.interact && !_isTeleported && !_isNextLevel && counter == 0 )
            {
                other.transform.position = _teleportationTarget.position;
                counter++;
                Teleported?.Invoke();
            }

            if (_input.interact && !_isTeleported && _isNextLevel)
            {
                LoadNextLevel?.Invoke(_nextLevelNum);
            }
        }
    }

    private void OnDestroy()
    {
        PortalScript.Teleported -= SetTeleportTimer;
    }
}
