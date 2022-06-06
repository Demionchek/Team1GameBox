using StarterAssets;
using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _damage = 10;
    [SerializeField] private Saver _saver;
    [SerializeField] private float _teleportDelay = 0.5f;
    [Header("CheckPoints")]
    [SerializeField] private CheckPoint[] checkPoints;

    private Health _health;
    private Energy _energy;
    private Inventory _inventory;

    private void Start()
    {
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

        foreach (var point in checkPoints)
        {
            if (_saver.CheckPointToSave == point.PointNumber)
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
        _saver.LoadEnergy();
        _saver.LoadHealth();
        _saver.LoadHealthPacks();
        _saver.LoadEnergyPacks();
        _inventory.AddCountOfItem(ItemType.HealthPotion, _saver.HealthPacksToSave);
        _inventory.AddCountOfItem(ItemType.EnergyPotion, _saver.EnergyPacksToSave);
        _health.Hp = _saver.HealthToSave;
        _energy.CurrentEnergy = _saver.EnergyToSave;
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

        foreach (var point in checkPoints)
        {
            if (_saver.CheckPointToSave == point.PointNumber)
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
