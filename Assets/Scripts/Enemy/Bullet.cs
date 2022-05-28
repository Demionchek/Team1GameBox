using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private LayerMask _mask;

    public void SetDamageAndMask(int dmg, LayerMask mask)
    {
        _damage = dmg;
        _mask = mask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage,_mask);
            Destroy(gameObject);
        }
    }
}
