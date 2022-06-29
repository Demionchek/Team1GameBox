using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SavingSystem
{
    public void SavePlayerData(CharacterController controller)
    {
        Inventory inventory = controller.GetComponent<Inventory>();
        Health health = controller.GetComponent<Health>();
        Energy energy = controller.GetComponent<Energy>();
        UnlockSystemManager unlockSystem = controller.GetComponent<UnlockSystemManager>();
        int healthPacks = inventory.GetCountOfItem(ItemType.HealthPotion);
        int energyPacks = inventory.GetCountOfItem(ItemType.EnergyPotion);
        int playerHealth = (int)health.Hp;
        int playerEnergy = (int)energy.CurrentEnergy;
        int scrollsCount = unlockSystem.Counter;
        PlayerData playerData = new PlayerData
        {
            HealthData = playerHealth,
            EnergyData = playerEnergy,
            HealthPacksData = healthPacks,
            EnergyPacksData = energyPacks,
            ScrollsCountData = scrollsCount,
        };
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/PlayerSaveData.txt", json);

#if (UNITY_EDITOR)
        Debug.Log("PlayerDataSaved!");
#endif
    }

    public void SaveWorldData()
    {

    }

    public void LoadPlayerData(ref PlayerData playerData)
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSaveData.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/PlayerSaveData.txt");
            playerData = JsonUtility.FromJson<PlayerData>(saveString);
#if (UNITY_EDITOR)
            Debug.Log("PlayerDataLoaded!");
#endif
        }
    }
}

public class WorldData
{
    public int Level;
    public int CheckPoint;
    public int PassedArena;
    public int[] CollectedScrolls;
}


public class PlayerData
{
    public int HealthData;
    public int EnergyData;
    public int HealthPacksData;
    public int EnergyPacksData;
    public int ScrollsCountData;
}

