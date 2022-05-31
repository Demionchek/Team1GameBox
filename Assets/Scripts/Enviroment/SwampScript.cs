using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class SwampScript : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _damageDelay = 0.5f;
    [SerializeField] private float _speedDecreaser = 0.5f;
    private IDamageable _damageable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
            controller.SprintSpeed *= _speedDecreaser;
            if (_damageable == null)
            {
                _damageable = other.GetComponent<IDamageable>();
            }
            StartCoroutine(SlowDamage());
        }
    }

    private IEnumerator SlowDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(_damageDelay);
            _damageable.TakeDamage(_damage, _playerLayer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ThirdPersonController>(out ThirdPersonController controller))
        {
            StopAllCoroutines();
            controller.SprintSpeed /= _speedDecreaser;
        }
    }
}
