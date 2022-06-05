using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LikhoChargingState : EnemyStates
{
    public LikhoChargingState(EnemyController enemyController) : base(enemyController)
    {
    }

    public static event Action<Vector3, float> CreateMarker;

    private const float yCorrection = 0.01f;

    public override IEnumerator CurrentState()
    {
        Vector3 lastPlayerPos = _enemyController.Target.position;
        float lookHeight = _enemyController.transform.position.y;
        Vector3 markerTarget = new Vector3(lastPlayerPos.x, lookHeight, lastPlayerPos.z);
        float deleteTime = _enemyController.EnemiesConfigs.likhoSpecialAttackDelay +
                            _enemyController.SpecialAnimLength;
        CreateMarker(markerTarget, deleteTime);
        _enemyController.Agent.enabled = false;
        //markerTarget.y = lookHeight;
        _enemyController.transform.LookAt(markerTarget);
        _enemyController.TmpTarget = markerTarget;
        yield return new WaitForSeconds(_enemyController.EnemiesConfigs.likhoSpecialAttackDelay);
        _enemyController.transform.DOMove(markerTarget, _enemyController.DOMoveSpeed);
        _enemyController.StartSpecialAttack();
    }
}
