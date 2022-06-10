using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundsEvents : MonoBehaviour
{
    [SerializeField] private AudioSource mainTheme;
    [SerializeField] private AudioSource arenaTheme;
    [SerializeField] private AudioSource harbourTheme;
    [SerializeField, Range(0,5)] private float changeSpeed = 1f;

    private bool inHarbour;
    private void Start()
    {
        DeathArena.ArenaActivated += SetActiveMusic;
        mainTheme.Play();
        if (arenaTheme != null)
        {
            arenaTheme.volume = 0f;
        }
    }

    private void SetActiveMusic(bool isArena)
    {
        if (isArena)
        {
            arenaTheme.Play();
            arenaTheme.DOFade(1,changeSpeed);
            mainTheme.DOFade(0, changeSpeed);
        }
        else
        {
            arenaTheme.DOFade(0, changeSpeed);
            mainTheme.DOFade(1, changeSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            if (!inHarbour)
            {
                inHarbour = true;
                harbourTheme.Play();
                harbourTheme.DOFade(1, changeSpeed);
                mainTheme.DOFade(0, changeSpeed);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            inHarbour = false;
            harbourTheme.DOFade(0, changeSpeed);
            mainTheme.DOFade(1, changeSpeed);
        }
    }

    private void OnDestroy()
    {
        DeathArena.ArenaActivated -= SetActiveMusic;
    }
}
