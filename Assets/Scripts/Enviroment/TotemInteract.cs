using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemInteract : MonoBehaviour, IUse
{
    [SerializeField] private DeathArena _arena;
    [SerializeField] private PortalUIScript _portalUIScript;

    public void Use(CharacterController controller)
    {
        _arena.Activate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            _portalUIScript.ChangeUIState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            _portalUIScript.ChangeUIState(false);
        }
    }
}
