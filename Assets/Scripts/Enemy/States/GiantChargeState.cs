using System.Collections;
using UnityEngine;
using System;

public class GiantChargeState : EnemyStates
{
    public GiantChargeState(EnemyController enemyController) : base(enemyController)
    {
    }

    public static event Action<Vector3, Vector3, float> CreateMarker;

    private const float yMarkerCorrection = 0.03f;
    private const float yPlayerCorrection = 1f;

    public override IEnumerator CurrentState()
    {
        Vector3 lastPlayerPos = _enemyController.Target.position;
        Vector3 markerTarget = new Vector3(lastPlayerPos.x, lastPlayerPos.y + yPlayerCorrection, lastPlayerPos.z);
        float deleteTime = _enemyController.EnemiesConfigs.giantSpecialAttackDelay +
                            _enemyController.SpecialAnimLength;
        markerTarget.y += yMarkerCorrection;
        CreateMarker(_enemyController.transform.position, markerTarget, deleteTime);
        _enemyController.Agent.SetDestination(_enemyController.Target.position);
        yield return new WaitForSeconds(_enemyController.EnemiesConfigs.giantSpecialAttackDelay);
        _enemyController.StartSpecialAttack();
    }
}