using System;
using UnityEngine;

public class Totem : MonoBehaviour, IUse
{
    [SerializeField] public int _arenaNum;
    [SerializeField] private DeathArena _arena;
    [SerializeField] private InteractUIScript _interactUIScript;
    private SavingSystem _savingSystem;
    private bool _isOff;

    public static event Action<int> ArenaNum;

    private void Start()
    {
        _savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        _savingSystem.LoadWorldData(ref worldData);
        if (worldData.PassedArena >= _arenaNum)
        {
            _isOff = true;
            _arena.OffBarier();
        }
        _interactUIScript.ChangeUIState(false);
    }

    public void ArenaPassed() => ArenaNum(_arenaNum);

    public void Use(CharacterController controller)
    {
        if (!_isOff)
        {
            _arena.Activate();
            _isOff = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            _interactUIScript.ChangeUIState(true);
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
