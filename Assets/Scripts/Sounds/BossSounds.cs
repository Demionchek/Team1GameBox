using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _hutChargingSource;
    [SerializeField] private AudioSource _rocketsSource;
    [SerializeField] private AudioSource _specialAttackSource;
    [SerializeField] private AudioSource _footstepsSource;
    [SerializeField] private AudioSource _damagedSource;
    [SerializeField] private AudioSource _deathSource;

    [Space(20)]

    [SerializeField] private AudioClip[] _hutChargeSounds;
    [SerializeField] private AudioClip[] _rocketsSounds;
    [SerializeField] private AudioClip[] _specialAttackSounds;
    [SerializeField] private AudioClip[] _footstepsSounds;
    [SerializeField] private AudioClip[] _damagedSounds;
    [SerializeField] private AudioClip[] _deathSounds;

    public void PlayHutChargingSound()
    {
        int r = Random.Range(0, _hutChargeSounds.Length);
        _hutChargingSource.clip = _hutChargeSounds[r];
        _hutChargingSource.Play();
    }

    public void PlaySpecialSound()
    {
        int r = Random.Range(0, _specialAttackSounds.Length);
        _specialAttackSource.clip = _specialAttackSounds[r];
        _specialAttackSource.Play();
    }

    public void PlayChargeSound()
    {
        int r = Random.Range(0, _rocketsSounds.Length);
        _rocketsSource.clip = _rocketsSounds[r];
        _rocketsSource.Play();
    }

    public void PlayFootstepsSound()
    {
        int r = Random.Range(0, _footstepsSounds.Length);
        _footstepsSource.clip = _footstepsSounds[r];
        _footstepsSource.Play();
    }

    public void PlayDamagedSound()
    {
        int r = Random.Range(0, _damagedSounds.Length);
        _damagedSource.clip = _damagedSounds[r];
        _damagedSource.Play();
    }

    public void PlayDeathSound()
    {
        int r = Random.Range(0, _deathSounds.Length);
        _deathSource.clip = _deathSounds[r];
        _deathSource.Play();
    }
}
