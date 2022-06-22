using UnityEngine;

public class UnlockItems : MonoBehaviour
{
    [SerializeField,Range(0,3)] public int ScrollNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<UnlockSystemManager>(out UnlockSystemManager unlockManager))
        {
            unlockManager.TryUnlock(ScrollNum);
            Destroy(gameObject);
        }
    }
}
