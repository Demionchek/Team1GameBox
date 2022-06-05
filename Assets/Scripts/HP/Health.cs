using UnityEngine;
using System;
using StarterAssets;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hp;
    [SerializeField] private LayerMask _layerMask;
    private EnemySounds _enemySounds;
    private PlayerSounds _playerSounds;
    private BossSounds _bossSounds;

    public static Action HPChanged;

    public float Hp { get; set; }
    public float FullHP { get; private set; }

    private bool _isPlayer;
    private bool _isBoss;

    private void Start()
    {
        Hp = _hp;
        FullHP = _hp;
        if (TryGetComponent(out PlayerSounds playerSounds))
        {
            _isPlayer = true;
            _playerSounds = playerSounds;
        }
        if (TryGetComponent(out EnemySounds enemySounds))
        {
            _enemySounds = enemySounds;
        }
        if (TryGetComponent(out BossSounds bossSounds))
        {
            _isBoss = true;
            _bossSounds = bossSounds;
        }

    }

    public void RestoreHealth(int amount)
    {
        Hp += amount;
        Hp = Mathf.Min(Hp, _hp);
    }

    public void TakeDamage(int damage, LayerMask mask)
    {
        if (_layerMask == mask)
        {
            Debug.Log($"Damaged {gameObject.name}");
            Hp -= damage;
            CheckDeath();



            if (_isPlayer)
            {
                 HPChanged();
                _playerSounds.PlayDamagedSound();
            }
            else if (_isBoss)
            {
                _bossSounds.PlayDamagedSound();
            }
            else
            {
                _enemySounds.PlayDamagedSound();
            }
        }
    }

    public void Revive()
    {
        Hp = FullHP;
    }

    public void CheckDeath()
    {
        if (Hp < 1)
        {
            if (TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.IsAlive = false;
                _enemySounds.PlayDamagedSound();
            }
            else if (TryGetComponent<YagaController>(out YagaController yagaController))
            {
                yagaController.IsAlive = false;
                _bossSounds.PlayDamagedSound();
            }
            else if (TryGetComponent<PlayerDeath>(out PlayerDeath death))
            {
                death.PlayersDeath();
                _playerSounds.PlayDamagedSound();
            }

            Debug.Log(transform.name + " died");
        }
    }
}
