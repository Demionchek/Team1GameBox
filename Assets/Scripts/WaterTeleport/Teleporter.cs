using StarterAssets;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _damage = 10;
    [SerializeField] private Saver _saver;
    [SerializeField] private float _teleportDelay = 0.5f;
    [Header("CheckPoints")]
    [SerializeField] private CheckPoint[] checkPoints;

    private void Start()
    {
        Vector3 spawnPosition = checkPoints[0].SpawnPoint.position;

        foreach (var point in checkPoints)
        {
            if (_saver.CheckPointToSave == point.PointNumber)
            {
                spawnPosition = point.SpawnPoint.position;
                break;
            }
        }
        controller.enabled = false;
        controller.transform.position = spawnPosition;
        controller.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<CharacterController>(out CharacterController playerController))
        {
            StartCoroutine(TelerportToSafePosition(playerController));
        }
    }

    private IEnumerator TelerportToSafePosition(CharacterController controller)
    {
        yield return new WaitForSeconds(_teleportDelay);
        if (controller.TryGetComponent<IDamageable>(out IDamageable playerHealth))
            playerHealth.TakeDamage(_damage, _playerLayer);

        Vector3 spawnPosition = checkPoints[0].SpawnPoint.position;

        foreach (var point in checkPoints)
        {
            if (_saver.CheckPointToSave == point.PointNumber)
            {
                spawnPosition = point.SpawnPoint.position;
                break;
            }
        }
        controller.enabled = false;
        controller.transform.position = spawnPosition;
        controller.enabled = true;
    }
}
