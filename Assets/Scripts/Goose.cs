using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : MonoBehaviour, IUse
{
    [SerializeField] private AudioSource honkSource;
    [SerializeField] private InteractUIScript uIScript;
    [SerializeField] private AudioClip[] honks;

    public void Use(CharacterController controller)
    {
            int r = Random.Range(0, honks.Length);
            honkSource.clip = honks[r];
            honkSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            uIScript.ChangeUIState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            uIScript.ChangeUIState(false);
        }
    }
}
