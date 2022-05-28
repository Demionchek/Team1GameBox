using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YagaAnimations : MonoBehaviour
{

    [SerializeField] private YagaController _controller;
    [SerializeField] private float _castSpeed = 0.7f;

    private Animator _animator;
    private int _animIDIdle;
    private int _animIDCast;
    private int _animIDConeAttack;
    private int _animIDDeath;

    private int _currAnimState;
    private const int _deadState = 0;
    private const int _idleState = 1;
    private const int _attackState = 2;
    private const int _castState = 3;

    private bool _isDead;
    public void StateFinished() => _controller.ChangeState();

    void Start()
    {
        _animator = GetComponent<Animator>();

        AssignAnimationsIDs();
        _animator.SetFloat("CastSpeed", _castSpeed);
        _isDead = false;
    }

    private void Update()
    {
        _currAnimState = _controller.CurrentState;

        StateSwitcher();

    }


    private void StateSwitcher()
    {
        switch (_currAnimState)
        {
            case _idleState:
                IdleState();
                break;
            case _castState:
                CastState();
                break;
            case _attackState:
                ConeAttackState();
                break;
            case _deadState:
                DeadState();
                break;
        }
    }

    private void AssignAnimationsIDs()
    {
        _animIDIdle = Animator.StringToHash("Idle");
        _animIDCast = Animator.StringToHash("Cast");
        _animIDConeAttack = Animator.StringToHash("ConeAttack");
        _animIDDeath = Animator.StringToHash("Death");
    }

    public void IdleState()
    {
        _animator.SetBool(_animIDIdle, true);
        _animator.SetBool(_animIDCast, false);
        _animator.SetBool(_animIDConeAttack, false);
    }

    public void ConeAttackState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDCast, false);
        _animator.SetBool(_animIDConeAttack, true);

    }

    public void CastState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDCast, true);
        _animator.SetBool(_animIDConeAttack, false);

    }
    public void DeadState()
    {
        _animator.SetBool(_animIDIdle, false);
        _animator.SetBool(_animIDCast, false);
        _animator.SetBool(_animIDConeAttack, false);
        if (!_isDead)
        {
            _animator.SetTrigger(_animIDDeath);
            _isDead = true;
        }
    }
}
