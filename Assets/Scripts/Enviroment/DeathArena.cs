using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathArena : MonoBehaviour
{
    [SerializeField] private GameObject _barier;
    [SerializeField] private GameObject _forestWall; 
    [SerializeField] private EnemySpawner spawner;
    private bool _isActive;

    private const float k_delay = 1f;
    public int EnemiesAlive { get; set; }

    public static event Action<bool> ArenaActivated;

    private void Start()
    {
        _barier.SetActive(false);
    }

    public void OffBarier() => _forestWall.SetActive(false);

    private void DeathCounter()
    {
        if (_isActive) EnemiesAlive--;
    }

    public void Activate()
    {
        EnemyController.EnemyDeathAction += DeathCounter;
        ArenaActivated?.Invoke(true);
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
        ArenaActivated?.Invoke(false);
        _barier.SetActive(false);
        EnemiesAlive = 0;
        EnemyController.EnemyDeathAction -= DeathCounter;
    }
}
