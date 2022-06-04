using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System;

public class EnemyController : EnemyStateMachine
{
    [SerializeField] public Transform Target;
    [SerializeField] public EnemiesConfigs EnemiesConfigs;
    [Range(0.1f, 5), SerializeField] public float DOMoveSpeed = 0.7f;
    [Header("EnemyType")]
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _maxSpecialMoveDistance = 7f;
    [SerializeField] private float _maxSpecialMoveHeight = 1f;
    [Header("SpecialAttackAnimation")]
    [Tooltip("Special Animation of your enemy type")]
    [SerializeField] private AnimationClip _specialAttack;
    private CapsuleCollider _capsule;
    private EnemyAnimations _enemyAnimations;

    private enum EnemyType
    {
        Likho,
        CyberGiant,
        Normal
    }
    private int deathActionCounter = 0;
    private int lastState = 10;

    private const int _idleState = 0;
    private const int _moveState = 1;
    private const int _attackState = 2;
    private const int _specialState = 3;
    private const int _chargeState = 4;
    private const int _deadState = 8;

    private const float yPlayerCorrection = 1f;

    private float _stopDistanceCorrection = 0.3f;
    private float _specialChance;
    private float _cooldownTime;

    private bool _isAttaking;
    private bool _isCharging;
    private bool _isSpecialAttacking;
    private bool _isSpecialAttackCooled;

    public bool IsAlive { get; set; }
    public NavMeshAgent Agent { get; set; }
    public Vector3 TmpTarget { get; set; }
    public float TmpSpeed { get; set; }
    public int CurrState { get; set; }
    public bool DoSpecial { get; set; }
    public bool IsSpecialJumping { get; set; }
    public float SpecialAnimLength { get; private set; }
    public void SpecialIsFinished() => _isSpecialAttacking = false;

    public static Action EnemyDeathAction;

    private bool CanDoSpecialDistance(float maxDistance, float maxHeight)
    {
        if (Vector3.Distance(transform.position, Target.position) < maxDistance
           && Math.Abs(transform.position.y - transform.localScale.y - Target.position.y) < maxHeight)
        {
            return true;
        }

        else return false;
    }

    private void Start()
    {
        EnemyPresetsOnType();
    }

    private void EnemyPresetsOnType()
    {
        Agent = GetComponent<NavMeshAgent>();
        _enemyAnimations = GetComponent<EnemyAnimations>();
        switch (_enemyType)
        {
            case EnemyType.Likho:
                Agent.speed = EnemiesConfigs.likhoSpeed;
                Agent.stoppingDistance = EnemiesConfigs.likhoStoppingDistance;
                _specialChance = EnemiesConfigs.likhoSpecialAttackChance;
                _cooldownTime = EnemiesConfigs.likhoSpecialAttackCooldownTime;
                break;
            case EnemyType.CyberGiant:
                Agent.speed = EnemiesConfigs.giantSpeed;
                Agent.stoppingDistance = EnemiesConfigs.giantStoppingDistance;
                _specialChance = EnemiesConfigs.giantSpecialAttackChance;
                _cooldownTime = EnemiesConfigs.giantSpecialAttackCooldownTime;
                break;
            case EnemyType.Normal:
                Agent.speed = EnemiesConfigs.normalSpeed;
                Agent.stoppingDistance = EnemiesConfigs.normalStoppingDistance;
                break;
        }
        _capsule = GetComponent<CapsuleCollider>();
        _stopDistanceCorrection += Agent.stoppingDistance;
        CurrState = _idleState;
        SpecialAnimLength = _specialAttack.length;
        _isSpecialAttackCooled = true;
        _isCharging = false;
        _isSpecialAttacking = false;
        IsAlive = true;
    }

    private void Update()
    {
        if (Target != null)
        {
            EnemyBehaviour();
            //AnimatorUpdater();
        }
    }

    //private void AnimatorUpdater()
    //{
    //    if (CurrState != lastState)
    //    {
    //        Debug.Log($" {CurrState}  {lastState}");
    //        lastState = CurrState;
    //        _enemyAnimations.OnStateChange(CurrState);
    //    }
    //}

    public void Revive()
    {
        IsAlive = true;
        Agent.enabled = true;
        _capsule.enabled = false;
        CurrState = _idleState;
        GetComponent<Health>().Revive();
    }

    private void EnemyBehaviour()
    {

        if (!IsAlive)
        {
            CurrState = _deadState;
            StopAllCoroutines();
            if (deathActionCounter == 0)
            {
                deathActionCounter++;
                EnemyDeathAction?.Invoke();
            }
        }

        switch (_enemyType)
        {
            case EnemyType.Likho:
                LikhoControll();
                break;
            case EnemyType.CyberGiant:
                GiantControll();
                break;
            case EnemyType.Normal:
                NormalControll();
                break;
        }
    }

    private void NormalControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        switch (CurrState)
        {
            case _idleState:
                if (distanceToTarget <= EnemiesConfigs.likhoReactDistance)
                {
                    CurrState = _moveState;
                }
                SetState(new IdleState(this));
                break;
            case _moveState:
                CheckSight(distanceToTarget);
                break;
            case _attackState:
                SetState(new AttackState(this));
                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = _moveState;
                }
                break;
            case _deadState:
                _capsule.enabled = false;
                Agent.enabled = false;
                break;
        }
    }

    private void GiantControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        if (CurrState != _chargeState)
        {
            _isCharging = false;
        }

        switch (CurrState)
        {
            case _idleState:
                if (distanceToTarget <= EnemiesConfigs.giantReactDistance)
                {
                    CurrState = _moveState;
                }
                SetState(new IdleState(this));
                break;
            case _moveState:
                CheckSight(distanceToTarget);
                break;
            case _attackState:
                SetState(new AttackState(this));

                Vector3 lastPlayerPos = Target.position;
                lastPlayerPos.y = transform.position.y;
                transform.LookAt(lastPlayerPos);

                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = _moveState;
                }

                break;
            case _chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;
                    SetState(new GiantChargeState(this));

                }
                break;
            case _specialState:

                SetState(new SpecialRayAttack(this));
                Agent.velocity = Vector3.zero;
                if (!_isSpecialAttacking)
                {

                    CurrState = _moveState;
                }

                break;
            case _deadState:
                _capsule.enabled = false;
                Agent.enabled = false;
                break;
        }
    }

    private void CheckSight(float distanceToTarget)
    {
        if (distanceToTarget <= Agent.stoppingDistance)
        {
            if (IsInSight())
            {
                CurrState = _attackState;
            }
            else
            {
                Rotate();
            }
        }
        else
        {
            SetState(new MoveState(this));
        }
    }

    private void LikhoControll()
    {
        float distanceToTarget = Vector3.Distance(transform.position, Target.position);

        if (CurrState != _chargeState)
        {
            _isCharging = false;
        }



        switch (CurrState)
        {
            case _idleState:
                if (distanceToTarget <= EnemiesConfigs.likhoReactDistance)
                {
                    CurrState = _moveState;
                }
                else
                {
                    SetState(new IdleState(this));
                }
                break;
            case _moveState:
                CheckSight(distanceToTarget);
                break;
            case _attackState:
                SetState(new AttackState(this));
                if (distanceToTarget >= Agent.stoppingDistance && !_isAttaking)
                {
                    CurrState = _moveState;
                }
                break;
            case _chargeState:
                if (!_isCharging)
                {
                    _isCharging = true;
                    SetState(new LikhoChargingState(this));
                }
                break;
            case _specialState:

                SetState(new SpecialJumpAttack(this));

                if (!_isSpecialAttacking && Agent.enabled)
                {
                    Agent.velocity = Vector3.zero;
                    CurrState = _moveState;
                }

                break;
            case _deadState:
                _capsule.enabled = false;
                Agent.enabled = false;
                break;
        }

        if (IsSpecialJumping)
        {
            JumpMove(TmpTarget, TmpSpeed);
        }
    }

    private void Rotate()
    {
        Vector3 lookAt = Target.transform.position;
        lookAt.y = transform.position.y;
        Vector3 lookDir = (lookAt - transform.position).normalized;
        Agent.enabled = false;
        transform.forward = lookDir.normalized;
        Agent.enabled = true;
    }

    public void Agressive() => StartCoroutine(SetToMoveState());

    private IEnumerator SetToMoveState()
    {
        yield return new WaitForSeconds(0.3f);
        CurrState = _moveState;
    }

    public void JumpMove(Vector3 target, float speed)
    {
        transform.DOMove(target, speed);
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _isAttaking = isAttacking;
        if (isAttacking == false)
        {
            Debug.Log("Look At");
            if (IsInSight())
            {
                SpecialAttackChance();
            }
            else
            {
                CurrState = _moveState;
            }
        }
    }

    private bool IsInSight()
    {
        Vector3 lastPlayerPos = Target.position;
        lastPlayerPos.y = transform.position.y;
        Vector3 lookVector = lastPlayerPos - transform.position;
        return Vector3.Dot(transform.forward, lookVector.normalized) >= EnemiesConfigs.sightAngle;
    }

    public void SpecialAttackChance()
    {
        bool canDoSpecial = _isSpecialAttackCooled && UnityEngine.Random.value < _specialChance;
        bool enemyType = _isAttaking == false && _enemyType != EnemyType.Normal;
        bool special = CanDoSpecialDistance(_maxSpecialMoveDistance, _maxSpecialMoveHeight);
        if (canDoSpecial && enemyType && special)
        {
            _isSpecialAttackCooled = false;
            CurrState = _chargeState;
        }
    }

    public void StartSpecialAttack()
    {
        _isSpecialAttacking = true;
        CurrState = _specialState;
        StartCoroutine(SpecialAttackCooling(_cooldownTime));
    }


    private IEnumerator SpecialAttackCooling(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        _isSpecialAttackCooled = true;
    }


}
