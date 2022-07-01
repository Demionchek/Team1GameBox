using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class SavingSystem
{
#if(UNITY_EDITOR)
    [MenuItem("Utils/Clear progress")]
#endif
    public static void ClearProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSaveData.txt"))
        {
            File.Delete(Application.persistentDataPath + "/PlayerSaveData.txt");
        }
        if (File.Exists(Application.persistentDataPath + "/WorldSaveData.txt"))
        {
            File.Delete(Application.persistentDataPath + "/WorldSaveData.txt");
        }
#if(UNITY_EDITOR)
        Debug.Log("DataCleared!");
#endif
    }

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

    public void SaveWorldData(int level, int checkPoint, int passedArena, List<int> collectedScrollsID)
    {
        WorldData worldData = new WorldData
        {
            Level = level,
            CheckPoint = checkPoint,
            PassedArena = passedArena,
            CollectedScrolls = collectedScrollsID
        };
        string json = JsonUtility.ToJson(worldData);
        File.WriteAllText(Application.persistentDataPath + "/WorldSaveData.txt", json);
#if (UNITY_EDITOR)
        Debug.Log("WorldDataSaved!");
#endif
    }

    public void LoadWorldData(ref WorldData worldData)
    {
        if (File.Exists(Application.persistentDataPath + "/WorldSaveData.txt"))
        {
            string savedString = File.ReadAllText(Application.persistentDataPath + "/WorldSaveData.txt");
            worldData = JsonUtility.FromJson<WorldData>(savedString);
#if (UNITY_EDITOR)
            Debug.Log("WorldDataLoaded!");
#endif
        }
    }

    public void LoadPlayerData(ref PlayerData playerData)
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSaveData.txt"))
        {
            string savedString = File.ReadAllText(Application.persistentDataPath + "/PlayerSaveData.txt");
            playerData = JsonUtility.FromJson<PlayerData>(savedString);
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
    public List<int> CollectedScrolls;
}


public class PlayerData
{
    public int HealthData;
    public int EnergyData;
    public int HealthPacksData;
    public int EnergyPacksData;
    public int ScrollsCountData;
}

