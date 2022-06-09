using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemInteract : MonoBehaviour, IUse
{
    [SerializeField, Range(1, 2)] private int _arenaNum;
    [SerializeField] private DeathArena _arena;
    [SerializeField] private Saver _saver;
    [SerializeField] private PortalUIScript _portalUIScript;
    private bool _isOff;

    private void Start()
    {
        _saver.LoadArenas();
        if (_saver.ArenaNumToSave >= _arenaNum)
        {
            _isOff = true;
            _arena.OffBarier();
        }
    }

    public void Use(CharacterController controller)
    {
        if (!_isOff)
        {
            _arena.Activate();
            _saver.SaveArenaNum(_arenaNum);
            _isOff = true;
        }
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
