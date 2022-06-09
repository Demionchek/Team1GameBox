using UnityEngine;
using System;
using StarterAssets;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hp;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float lowHPSoundAt = 30f;
    private EnemySounds _enemySounds;
    private PlayerSounds _playerSounds;
    private BossSounds _bossSounds;
    private EnemySocials _enemySocials;

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
            _enemySocials = GetComponent<EnemySocials>();
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


            try
            {
                if (_isPlayer)
                {
                    HPChanged();
                    _playerSounds.PlayDamagedSound();
                    if (Hp < lowHPSoundAt)
                    {
                        _playerSounds.PlayLowHPSound();
                    }
                }
                else if (_isBoss)
                {
                    _bossSounds.PlayDamagedSound();
                }
                else
                {
                    _enemySounds.PlayDamagedSound();
                    _enemySocials.CallNearbyEnemies();
                }
            }
            catch
            {
                Debug.Log("Что-то не то со звуками из скрипта  Health, cs 60 и далее");
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
                _enemySounds.PlayDeathSound();
                _enemySounds.StopStubSound();
            }
            else if (TryGetComponent<YagaController>(out YagaController yagaController))
            {
                yagaController.IsAlive = false;
                _bossSounds.PlayDeathSound();
            }
            else if (TryGetComponent<PlayerDeath>(out PlayerDeath death))
            {
                death.PlayersDeath();
                _playerSounds.PlayDeathSound();
                _playerSounds.StopLowHPSound();
            }

            Debug.Log(transform.name + " died");
        }
    }
}
