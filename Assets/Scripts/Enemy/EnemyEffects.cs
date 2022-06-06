using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyEffects : MonoBehaviour
{
    [SerializeField] private VisualEffect _rayEffect;

    public void PlayRayeffect()
    {
        _rayEffect.Play();
    }
}
