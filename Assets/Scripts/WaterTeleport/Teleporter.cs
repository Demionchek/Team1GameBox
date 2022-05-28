using StarterAssets;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform safePosition;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<ThirdPersonController>(out ThirdPersonController playerController))
        {
            StartCoroutine(TelerportToSafePosition(playerController.transform));
        }
    }

    private IEnumerator TelerportToSafePosition(Transform player)
    {
        yield return new WaitForSeconds(1f);
        if (player.TryGetComponent<IDamageable>(out IDamageable playerHealth))
            playerHealth.TakeDamage(damage, playerLayer);
        player.position = safePosition.position;
    }
}
