using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemInteract : MonoBehaviour, IUse
{
    [SerializeField, Range(1, 2)] public int _arenaNum;
    [SerializeField] private DeathArena _arena;
    [SerializeField] public Saver _pSaver;
    [SerializeField] private PortalUIScript _portalUIScript;
    private bool _isOff;

    private void Start()
    {
        _pSaver.LoadArenas();
        if (_pSaver.ArenaNumToSave >= _arenaNum)
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
            _pSaver.SaveArenaNum(_arenaNum);
            _isOff = true;
        }
    }

    public void SaveArena() => _pSaver.SaveArenaNum(_arenaNum);

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
