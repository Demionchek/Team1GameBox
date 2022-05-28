using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerAbilitiesConfigs", menuName = "ScriptableObject/PlayerAbilitiesConfigs", order = 3)]

public class PlayerAbilitiesConfigs : ScriptableObject
{
    public LayerMask enemyLayer;

    [Header("Axe Throw")]
    public float axeThrowDmg;
    public float axeThrowCooldown;

    [Header("Air Atack")]
    public float airAtackDamage;
    public float airAtackCost;
    public float airAtackCooldown;
    public float airAtackRange;

    [Header("Mighty Punch")]
    public int mightyPunchDamage;
    public float mightyPunchCost;
    public float mightyPunchCooldown;
    public float mightyPunchRange;
    public float mightyPunchForce;
}
