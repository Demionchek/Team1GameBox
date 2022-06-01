using UnityEngine;

public interface IDamageable 
{
    public int Hp { get; set; }
    void TakeDamage(int damage, LayerMask mask);
    void CheckDeath();
}
