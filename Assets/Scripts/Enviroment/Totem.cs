using System;
using UnityEngine;

public class Totem : MonoBehaviour, IUse
{
    [SerializeField] public int _arenaNum;
    [SerializeField] private DeathArena _arena;
    [SerializeField] private InteractUIScript _interactUIScript;
    private SavingSystem _savingSystem;
    public bool IsOff { get; private set; }

    private void Start()
    {
        _savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        _savingSystem.LoadWorldData(ref worldData);
        if (worldData.PassedArena >= _arenaNum)
        {
            IsOff = true;
            _arena.OffBarier();
        }
        _interactUIScript.ChangeUIState(false);
    }

    public void ArenaPassed()
    {
        WorldData worldData = new WorldData();
        _savingSystem.LoadWorldData(ref worldData);
        worldData.PassedArena = _arenaNum;
        _savingSystem.SaveWorldData(worldData.Level, worldData.CheckPoint, worldData.PassedArena, worldData.CollectedScrolls);
    }

    public void Use(CharacterController controller)
    {
        if (!IsOff)
        {
            _arena.Activate();
            IsOff = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            if (!IsOff)
            {
                _interactUIScript.ChangeUIState(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            _interactUIScript.ChangeUIState(false);
        }
    }
}
