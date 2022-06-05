using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTimer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float _timerDelay;
    private void Start()
    {
        audioSource.Stop();
        StartCoroutine(PlayTimer());
    }
    private IEnumerator PlayTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timerDelay);
            audioSource.Play();
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
