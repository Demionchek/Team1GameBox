using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackMarkersConfigs", menuName = "ScriptableObject/AttackMarkersConfigs", order = 2)]
public class AttackMarkersConfigs : ScriptableObject
{
    [Header("Enemy")]
    public float jumpMarkerSize = 3f;
    public int jumpMarkerDamage = 10;
    public float rayMarkerLength = 6f;
    public float rayMarkerWidth = 2f;
    public int rayMarkerDamage = 10;

    [Header("Boss")]
    public float bossClowdMarkerSize = 3f;
    public float bossClowdMarkerSpeed = 3f;
    public float bossClowdMarkerLifetime = 6f;
    [Range (0,1)] public float bossClowdMarkerSlowing = 0.5f;
    public float bossClowdEffectTimer = 3f;
    [Space(5)]
    public float bossPondMarkerSize = 3f;
    public int bossPondMarkerDamage = 10;
    [Space(5)]
    public float bossRayMarkerLength = 30f;
    public float bossRayMarkerWidth = 4f;
    public int bossRayMarkerDamage = 0;
    [Space(5)]
    public float bossSingleConeMarkerLength = 3f;
    public float bossSingleConeMarkerWidth = 4f;
    public int bossConeMarkerDamage = 10;
    [Space(5)]
    public float bossMultyConeMarkerLength = 3f;
    public float bossMultyConeMarkerWidth = 4f;
    public int bossMultyConeMarkerDamage = 10;

}
