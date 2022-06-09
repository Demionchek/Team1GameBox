using UnityEngine;

public class UnlockItems : MonoBehaviour
{
    [SerializeField,Range(0,3)] private int _scrollNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<UnlockSystemManager>(out UnlockSystemManager unlockManager))
        {
            unlockManager.TryUnlock(_scrollNum);
            Destroy(gameObject);
        }
    }
}
