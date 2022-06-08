using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySocials : MonoBehaviour
{
    [SerializeField] private float _callRadius = 7f;
    [SerializeField] private LayerMask _enemyLayer; 
    [SerializeField] private int _countToFind = 30;

    public void CallNearbyEnemies()
    {
        float height = transform.position.y + transform.localScale.y;
        Vector3 rayPos = new Vector3(transform.position.x, height, transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward.normalized);
        RaycastHit[] hits = new RaycastHit[_countToFind];
        if (Physics.SphereCastNonAlloc(ray, _callRadius, hits, _callRadius, _enemyLayer) > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != null && hit.transform.TryGetComponent<EnemyController>(out EnemyController controller))
                {
                    try
                    {
                        controller.Agressive();
#if (UNITY_EDITOR)
                        Debug.Log("Called " + hit.transform.name);
#endif
                    }
                    catch
                    {
                        Debug.Log("EnemySocials script cs 25");
                    }
                }
            }
        }
    }
}
