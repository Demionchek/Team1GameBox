using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private EnemyController m_EnemyController;
    [SerializeField] private EnemyShooter m_Shooter;
    [SerializeField] private DamageDealer m_DamageDealer;
    [Space(10)]
    [SerializeField] private float _attackAnimationSpeed = 1f;

    private Animator _animator;
    private int _animIDIdle;
    private int _animIDMove;
    private int _animIDAttack;
    private int _animIDSpecial;
    private int _animIDDeath;

    private int _currAnimState;
    private const int _idleState = 0;
    private const int _moveState = 1;
    private const int _attackState = 2;
    private const int _specialState = 3;
    private const int _chargeState = 4;
    private const int _deadState = 8;

    private bool _isDead;

    public void Shoot() 
        => m_Shooter.Shoot(m_EnemyController.Target.position, m_EnemyController.EnemiesConfigs.giantDamage);
    public void SpecialFinished() => m_EnemyController.SpecialIsFinished();
    public void TrySpecial() => m_EnemyController.SpecialAttackChance();
    public void StartAttackEvent() => m_EnemyController.SetIsAttacking(true);
    public void StopAttackEvent() => m_EnemyController.SetIsAttacking(false);
    public void Hit() => m_DamageDealer.AttackSphereCast();

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("AttackSpeed",_attackAnimationSpeed);
        AssignAnimationsIDs();
    }

    private void Update()
    {
        _currAnimState = m_EnemyController.CurrState;
        if (_animator != null)
        {
            StateSwitcher();
        }
    }

    private void StateSwitcher()
    {
        switch (_currAnimState)
        {
            case _idleState:
                _isDead = false;
                IdleState();
                break;
            case _chargeState:
                IdleState();
                break;
            case _moveState:
                MoveState();
                break;
            case _attackState:
                AttackState();
                break;
            case _specialState:
                SpecialState();
                break;
            case _deadState:
                DeathState();
                break;
        }
    }

    private void AssignAnimationsIDs()
    {
        _animIDIdle = Animator.StringToHash("Idle");
        _animIDMove = Animator.StringToHash("Move");
        _animIDAttack = Animator.StringToHash("Attack");
        _animIDSpecial = Animator.StringToHash("Special");
        _animIDDeath = Animator.StringToHash("Death");
    }

    public void IdleState()
    {
        _animator.SetBool(_animIDIdle, true);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecial, false);

    }

    public void AttackState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, true);
        _animator.SetBool(_animIDSpecial, false);

    }

    public void MoveState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, true);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecial, false);

    }

    public void SpecialState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecial, true);

    }

    public void DeathState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDMove, false);
        _animator.SetBool(_animIDAttack, false);
        _animator.SetBool(_animIDSpecial, false);
        if (!_isDead)
        {
            _animator.SetTrigger(_animIDDeath);
            _isDead = true;
        }
    }

}
