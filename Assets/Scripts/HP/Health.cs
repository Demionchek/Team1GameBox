using UnityEngine;
using System;
using StarterAssets;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hp;
    [SerializeField] private LayerMask _layerMask;

    public static Action HPChanged;

    public float Hp { get; set; }
    public float FullHP { get; private set; }

    private bool _isPlayer;

    private void Start()
    {
        Hp = _hp;
        FullHP = _hp;
        if (TryGetComponent<ThirdPersonController>(out ThirdPersonController contriller))
        {
            _isPlayer = true;
        }
    }

    public void RestoreHealth(float amount)
    {
        Hp += amount;
        Hp = Mathf.Min(Hp, _hp);
    }

    public void TakeDamage(float damage, LayerMask mask)
    {
        if (_layerMask == mask)
        {
            Debug.Log($"Damaged {gameObject.name}");
            Hp -= damage;
            CheckDeath();

            if (_isPlayer)
            {
                 HPChanged();
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
            }
            else if (TryGetComponent<YagaController>(out YagaController yagaController))
            {
                yagaController.IsAlive = false;
            }
            else if (TryGetComponent<ThirdPersonController>(out ThirdPersonController thirdPersonController))
            {
                
            }

            Debug.Log(transform.name + " died");
        }
    }
}
