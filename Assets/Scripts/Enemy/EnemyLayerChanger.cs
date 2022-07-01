using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLayerChanger : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;

    private int _defaultLayerID;
    private int _xRayEnemyLayerID;

    private const float k_checkDelay = 0.3f;
    private const float k_deathDelay = 2f;
    private const int _idleState = 0;
    private const int _deadState = 8;

    private void Start()
    {
        _defaultLayerID = LayerMask.NameToLayer("Default");
        _xRayEnemyLayerID = LayerMask.NameToLayer("EnemyRenderAbove");
        gameObject.layer = _defaultLayerID;
        StartCoroutine(CheckCurrState(_enemyController));
    }

    private IEnumerator CheckCurrState(EnemyController enemy)
    {
        while(enemy.CurrState == _idleState)
        {
            yield return new WaitForSeconds(k_checkDelay);
        }
        gameObject.layer = _xRayEnemyLayerID;
        while(enemy.CurrState != _deadState)
        {
            yield return new WaitForSeconds(k_checkDelay);
        }
        yield return new WaitForSeconds(k_deathDelay);
        gameObject.layer = _xRayEnemyLayerID;
    }
}
