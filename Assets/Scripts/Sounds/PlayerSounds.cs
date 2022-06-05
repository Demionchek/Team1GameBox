using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource footSteps;
    [Space(5)]
    [SerializeField] private AudioSource jumpSound;
    [Space(5)]
    [SerializeField] private AudioSource groundedSound;
    [Space(5)]
    [SerializeField] private AudioSource swingAttack;
    [Space(5)]
    [SerializeField] private AudioSource kickSound;
    [Space(5)]
    [SerializeField] private AudioSource throwSound;
    [Space(5)]
    [SerializeField] private AudioSource dashSound;
    [Space(5)]
    [SerializeField] private AudioSource airAttackSound;
    [Space(5)]
    [SerializeField] private AudioSource takeDamageSound;
    [Space(5)]
    [SerializeField] private AudioSource deathSound;
    [Space(5)]
    [SerializeField] private AudioSource axeAppearSound;
    [Space(5)]
    [SerializeField] private AudioSource lowHPSound;
    [Space(5)]
    [SerializeField] private AudioSource airCDSound;
    [Space(5)]
    [SerializeField] private AudioSource kickCDSound;
    [Space(5)]
    [SerializeField] private AudioSource throwCDSound;
    [Space(5)]
    [Header("Clips Arrays")]
    [Space(5)]
    [SerializeField] private AudioClip[] stepsArray;
    [Space(5)]
    [SerializeField] private AudioClip[] jumpArray;
    [Space(5)]
    [SerializeField] private AudioClip[] swingsArray;
    [Space(5)]
    [SerializeField] private AudioClip[] throwArray;
    [Space(5)]
    [SerializeField] private AudioClip[] axeAppearArray;
    [Space(5)]
    [SerializeField] private AudioClip[] takeDamageArray;
    [Space(5)]
    [SerializeField] private AudioClip[] dashArray;
    [Space(5)]
    [SerializeField] private AudioClip[] groundedArray;
    [Space(5)]
    [SerializeField] private AudioClip[] damagedArray;

    public void KickCDSound()
    {
        kickCDSound.Play();
    }

    public void ThrowCDSound()
    {
        throwCDSound.Play();
    }

    public void AirAttackCDSound()
    {
        airCDSound.Play();
    }

    public void PlayLowHPSound()
    {
        lowHPSound.Play();
    }
    public void PlayAirAttackSound()
    {
        airAttackSound.Play();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }

    public void PlayKickSound()
    {
        kickSound.Play();
    }

    public void PlayDamagedSound()
    {
        int r = Random.Range(0, damagedArray.Length);
        if (r! > damagedArray.Length)
        {

            takeDamageSound.clip = damagedArray[r];
            takeDamageSound.Play();
        }
    }

    public void PlayThrowSound()
    {
        int r = Random.Range(0, throwArray.Length);
        throwSound.clip = throwArray[r];
        throwSound.Play();
    }
    public void PlayAxeAppearSound()
    {
        int r = Random.Range(0, axeAppearArray.Length);
        axeAppearSound.clip = axeAppearArray[r];
        axeAppearSound.Play();
    }
    public void PlayDashSound()
    {
        if (dashSound.isPlaying == false)
        {
            int r = Random.Range(0, dashArray.Length);
            dashSound.clip = dashArray[r];
            dashSound.Play();
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            int r = Random.Range(0, groundedArray.Length);
            groundedSound.clip = groundedArray[r];
            groundedSound.Play();
        }
    }

    public void SwingSound(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.3f)
        {
            int r = Random.Range(0, swingsArray.Length);
            swingAttack.clip = swingsArray[r];
            swingAttack.Play();
        }
    }

    public void JumpSound(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.3f)
        {
            int r = Random.Range(0, jumpArray.Length);
            jumpSound.clip = jumpArray[r];
            jumpSound.Play();
        }
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (stepsArray.Length > 0)
            {
                int r = Random.Range(0, stepsArray.Length);
                footSteps.clip = stepsArray[r];
                footSteps.Play();
            }
        }
    }
}
