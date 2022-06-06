using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{

    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private LayerMask[] _ignoreLayers;
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
        }
        if (_needDestroy)
        {
            foreach (var layer in _ignoreLayers)
            {
                if (other.gameObject.layer == layer)
                {
                    break;
                }
                Destroy(gameObject);
            }
        }
    }
}
