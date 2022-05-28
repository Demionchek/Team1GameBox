using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DeathArena : MonoBehaviour
{
    [SerializeField] private GameObject _barier;
    [SerializeField] private EnemySpawner spawner;
    private bool _isActive;
    private SphereCollider _collider;

    private const float k_delay = 1f;
    public int EnemiesAlive { get; set; }

    private void Start()
    {
        _barier.SetActive(false);
        _collider = GetComponent<SphereCollider>();
    }

    private void DeathCounter()
    {
        if (_isActive) EnemiesAlive--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ThirdPersonController controller))
        {
            EnemyController.EnemyDeathAction += DeathCounter;
            spawner.ArenaSummon();
            _isActive = true;
            _barier.SetActive(true);
            StartCoroutine(CheckIfEnemiesAreAlive());
            _collider.enabled = false;
        }
    }

    private IEnumerator CheckIfEnemiesAreAlive()
    {
        yield return new WaitForSeconds(k_delay);
        while (_isActive)
        {
            yield return new WaitForSeconds(k_delay);
            if (EnemiesAlive == 0)
            {
                _isActive = false;
            }
        }
        _barier.SetActive(false);
        EnemyController.EnemyDeathAction -= DeathCounter;
    }
}
