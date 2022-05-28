using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask _damageTo;
    [SerializeField] private float hitRadius;
    [SerializeField] private float hitDistance;
    [SerializeField] private int countToDamage;

    public void AttackSphereCast()
    {
        float height = transform.position.y + transform.localScale.y;
        Vector3 rayPos = new Vector3(transform.position.x, height, transform.position.z);
        Ray ray = new Ray(rayPos, transform.forward);
        RaycastHit[] hits = new RaycastHit[countToDamage];
        if (Physics.SphereCastNonAlloc(ray, hitRadius, hits, hitDistance, _damageTo) > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != null && hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage, _damageTo);
#if(UNITY_EDITOR)
                    Debug.Log("hit " + hit.transform.name);
#endif
                }
            }
        }
    }

}
