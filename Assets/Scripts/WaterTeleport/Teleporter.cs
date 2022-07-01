using StarterAssets;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _teleportDelay = 0.5f;
    [Header("CheckPoints")]
    [SerializeField] private CheckPoint[] checkPoints;

    private SavingSystem _savingSystem;
    private Health _health;
    private Energy _energy;
    private Inventory _inventory;

    private void Start()
    {
        _savingSystem = new SavingSystem();
        _health = controller.GetComponent<Health>();
        _energy = controller.GetComponent<Energy>();
        _inventory = controller.GetComponent<Inventory>();
        SetPlayerPos();
        SetPlayersData();
        uiManager.CheckEnergyBar();
        uiManager.CheckHpBar();
    }

    private void SetPlayerPos()
    {
        Vector3 spawnPosition = checkPoints[0].SpawnPoint.position;

        WorldData worldData = new WorldData();
        _savingSystem.LoadWorldData(ref worldData);

        foreach (var point in checkPoints)
        {
            if (worldData.CheckPoint == point.PointNumber)
            {
                spawnPosition = point.SpawnPoint.position;
                break;
            }
        }
        controller.enabled = false;
        controller.transform.position = spawnPosition;
        controller.enabled = true;
    }

    private void SetPlayersData()
    {
        PlayerData playerData = new PlayerData();
        _savingSystem.LoadPlayerData(ref playerData);
        _inventory.AddCountOfItem(ItemType.HealthPotion, playerData.HealthPacksData);
        _inventory.AddCountOfItem(ItemType.EnergyPotion, playerData.EnergyPacksData);
        if (playerData.HealthData != 0)
        {
            _health.Hp = playerData.HealthData;
        }
        else
        {
            _health.Hp = _health.FullHP;
        }
        if (playerData.EnergyData != 0)
        {
            _energy.CurrentEnergy = playerData.EnergyData;
        }
        else
        {
            _energy.CurrentEnergy = _energy.MaxEnergy;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<CharacterController>(out CharacterController playerController))
        {
            StartCoroutine(TelerportToSafePosition(playerController));
        }
    }

    private IEnumerator TelerportToSafePosition(CharacterController controller)
    {
        yield return new WaitForSeconds(_teleportDelay);
        if (controller.TryGetComponent(out IDamageable playerHealth))
            playerHealth.TakeDamage((int)_damage, _playerLayer);

        Vector3 spawnPosition = checkPoints[0].SpawnPoint.position;

        WorldData worldData = new WorldData();
        _savingSystem.LoadWorldData(ref worldData);

        foreach (var point in checkPoints)
        {
            if (worldData.CheckPoint == point.PointNumber)
            {
                spawnPosition = point.SpawnPoint.position;
                break;
            }
        }
        controller.enabled = false;
        controller.transform.position = spawnPosition;
        controller.enabled = true;
    }
}
