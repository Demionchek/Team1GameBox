using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : EnemyStates
{
    public MoveState(EnemyController enemyController) : base(enemyController)
    {
    }

    public override IEnumerator CurrentState()
    {
        _enemyController.Agent.isStopped = false;
        _enemyController.Agent.SetDestination(_enemyController.Target.position);

        yield break;
    }
}
