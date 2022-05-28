using UnityEngine;

public class UnlockItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<UnlockSystemManager>(out UnlockSystemManager unlockManager))
        {
            unlockManager.TryUnlock();
            Destroy(gameObject);
        }
    }
}
