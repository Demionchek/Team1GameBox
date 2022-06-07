using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DeathArena : MonoBehaviour
{
    [SerializeField] private GameObject _barier;
    [SerializeField] private GameObject _forestWall; 
    [SerializeField] private EnemySpawner spawner;
    private bool _isActive;

    private const float k_delay = 1f;
    public int EnemiesAlive { get; set; }

    private void Start()
    {
        _barier.SetActive(false);
    }

    private void DeathCounter()
    {
        if (_isActive) EnemiesAlive--;
    }

    public void Activate()
    {
        EnemyController.EnemyDeathAction += DeathCounter;
        spawner.ArenaSummon();
        _isActive = true;
        _barier.SetActive(true);
        StartCoroutine(CheckIfEnemiesAreAlive());
    }

    private IEnumerator CheckIfEnemiesAreAlive()
    {
        yield return new WaitForSeconds(k_delay);
        while (_isActive)
        {
            yield return new WaitForSeconds(k_delay);
            if (EnemiesAlive <= 0)
            {
                _isActive = false;
                _forestWall.SetActive(false);
            }
        }
        _barier.SetActive(false);
        EnemyController.EnemyDeathAction -= DeathCounter;
    }
}
