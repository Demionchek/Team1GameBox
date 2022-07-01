using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnablingOnMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPacks;

    void Start()
    {
        SavingSystem savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        savingSystem.LoadWorldData(ref worldData);
        for (int i = 0; i < _enemyPacks.Count; i++)
        {
            if (i < worldData.CheckPoint)
            {
                if(_enemyPacks[i] != null)
                    _enemyPacks[i].SetActive(false);
            }
        }
    }
}
