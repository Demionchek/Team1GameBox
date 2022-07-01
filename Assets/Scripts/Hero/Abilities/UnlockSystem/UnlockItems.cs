using UnityEngine;
using System;

public class UnlockItems : MonoBehaviour
{
    [SerializeField] public int ItemID;
    public static event Action<int> Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<UnlockSystemManager>(out UnlockSystemManager unlockManager))
        {
            unlockManager.TryUnlock();
            Collected?.Invoke(ItemID);
            Destroy(gameObject);
        }
    }
}
