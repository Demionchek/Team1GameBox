using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;
using System.Collections.Generic;

public class CheckPoint : MonoBehaviour, IUse
{
    [SerializeField] public int _pointNumber;
    [SerializeField] public Transform _spawnPoint;
    [SerializeField] private VisualEffect _visualEffect;
    [SerializeField] private Color _emissionColor;
    [SerializeField] private float _emissionAmount;
    private SavingSystem _savingSystem;
    private List<int> _collectedScrollsIDs = new List<int>();
    private int _passedArena; 
    private bool _isActivated;
    public int PointNumber { get { return _pointNumber; } }
    public Transform SpawnPoint { get { return _spawnPoint; } }

    private void Start()
    {
        _savingSystem = new SavingSystem();
        WorldData worldData = new WorldData();
        _savingSystem.LoadWorldData(ref worldData);
        _passedArena = worldData.PassedArena;
        if (worldData.CheckPoint < PointNumber)
        {
            _isActivated = false;
            _visualEffect.Stop();
        }
        else
        {
            _isActivated = true;
            _visualEffect.Play();
        }
        UnlockItems.Collected += AddScrollIDToList;
        Totem.ArenaNum += AddArenaNum;
    }

    private void AddArenaNum(int num) => _passedArena = num;

    private void AddScrollIDToList(int ID) => _collectedScrollsIDs.Add(ID);

    public void Use(CharacterController controller)
    {
        if (!_isActivated)
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            _savingSystem.SaveWorldData(scene, _pointNumber, _passedArena, _collectedScrollsIDs);
            _savingSystem.SavePlayerData(controller);
            _visualEffect.Play();
            _isActivated = true;
            UnlockItems.Collected -= AddScrollIDToList;
            Totem.ArenaNum -= AddArenaNum;
        }
    }

}
