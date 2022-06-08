using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource _attackSource;
    [SerializeField] private AudioSource _chargingSource;
    [SerializeField] private AudioSource _specialAttackSource;
    [SerializeField] private AudioSource _footstepsSource;
    [SerializeField] private AudioSource _damagedSource;
    [SerializeField] private AudioSource _deathSource;
    [SerializeField] private AudioSource _stubSound;
    [Space (20)]
    
    [SerializeField] private AudioClip[] _attackSounds;
    [SerializeField] private AudioClip[] _chargingSounds;
    [SerializeField] private AudioClip[] _specialAttackSounds;
    [SerializeField] private AudioClip[] _footstepsSounds;
    [SerializeField] private AudioClip[] _damagedSounds;
    [SerializeField] private AudioClip[] _deathSounds;

    public void StopStubSound() => _stubSound.Stop();

    public void PlayAttackSound()
    {
        int r = Random.Range(0, _attackSounds.Length);
        _attackSource.clip = _attackSounds[r];
        _attackSource.Play();
    }

    public void PlaySpecialSound()
    {
        int r = Random.Range(0, _specialAttackSounds.Length);
        _specialAttackSource.clip = _specialAttackSounds[r];
        _specialAttackSource.Play();
    }

    public void PlayChargeSound()
    {
        int r = Random.Range(0, _chargingSounds.Length);
        _chargingSource.clip = _chargingSounds[r];
        _chargingSource.Play();
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
