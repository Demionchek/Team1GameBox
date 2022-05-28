using System.Collections;
using System;
using UnityEngine;
using StarterAssets;

public class YagaController : MonoBehaviour
{
    [SerializeField] public Transform Target;
    [SerializeField] private SpellCaster _kettle;
    [SerializeField] public float AttackDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _markerDelay;
    private ThirdPersonController _controller;

    private const float _lookHeight = 1f;
    private const float _cycleDelay = 0.5f;


    private int _attackCounter;

    private const int _deadState = 0;
    private const int _idleState = 1;
    private const int _attackState = 2;
    private const int _castState = 3;


    private bool _isAttacking;
    private bool _isAttackCooled;

    public static event Action<Vector3, Vector3, float> CreateConeMarker;

    public int CurrentState { get; private set; }
    public float CastDealy { get; set; }
    public bool IsAlive { get; set; }

    private void Start()
    {
        _controller = Target.GetComponent<ThirdPersonController>();
        CurrentState = _idleState;
        _isAttackCooled = true;
        _attackCounter = 0;
        IsAlive = true;
        StartCoroutine(SpellCaster());
    }

    private void Update()
    {
        if (!IsAlive)
        {
            CurrentState = _deadState;
        }

        switch (CurrentState)
        {
            case _idleState:
                Vector3 playerPos = Target.position;
                playerPos.y = _lookHeight;
                transform.LookAt(playerPos);

                if (_isAttackCooled && _attackCounter == 0 && Vector3.Distance(transform.position, playerPos) < AttackDistance)
                {
                    CurrentState = _attackState;
                    _attackCounter++;
                }

                break;
            case _attackState:
                if (!_isAttacking)
                {
                    _isAttacking = true;

                    if (CreateConeMarker != null)
                        CreateConeMarker(transform.position, Target.position, _markerDelay);

                }
                break;
            case _castState:
                Vector3 castPos = _kettle.transform.position;
                castPos.y = _lookHeight;
                transform.LookAt(castPos);
                break;
            case _deadState:
                break;
        }
    }

    public void ChangeState()
    {
        _isAttacking = false;
        if (CurrentState != _attackState)
        {
            StartCoroutine(AttackCooling(_attackCooldown));
        }
        if (IsAlive)
        {
            CurrentState = _idleState;

        }
    }

    private IEnumerator AttackCooling(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        _isAttackCooled = true;
    }

    private IEnumerator SpellCaster()
    {
        yield return new WaitForSeconds(2f);
        while (IsAlive)
        {
            if (_isAttackCooled && !_isAttacking)
            {
                _isAttacking = true;
                _isAttackCooled = false;
                CurrentState = _castState;
                _attackCounter = 0;
                _kettle.GetNewRandomSpell(_controller, _markerDelay);
            }
            yield return new WaitForSeconds(_cycleDelay);
        }
        yield break;
    }


}
