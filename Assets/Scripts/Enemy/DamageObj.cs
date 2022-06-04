using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{

    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _mask;
    private bool _needDestroy = false;

    public void SetDamageAndMask(int dmg, LayerMask mask)
    {
        _damage = dmg;
        _mask = mask;
        _needDestroy = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            var damageable = controller.GetComponent<IDamageable>();
            damageable.TakeDamage(_damage, _mask);
            if (_needDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
