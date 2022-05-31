using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnablingOnMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPacks;
    [SerializeField] private Saver _saver;

    void Start()
    {
        for (int i = 0; i < _enemyPacks.Count; i++)
        {
            if (i < _saver.CheckPointToSave)
            {
                _enemyPacks[i].SetActive(false);
            }
        }
    }
}
