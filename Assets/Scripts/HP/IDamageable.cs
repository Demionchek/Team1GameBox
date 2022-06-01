using UnityEngine;

public interface IDamageable 
{
    public float Hp { get; set; }
    void TakeDamage(int damage, LayerMask mask);
    void CheckDeath();
}
