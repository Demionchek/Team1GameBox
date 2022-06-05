using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Audio;
using UnityEditor;

public class Saver : MonoBehaviour
{
    public int CheckPointToSave { get; private set; }
    public int LevelToSave { get; private set; }
    public int HealthToSave { get; private set; }
    public int EnergyToSave { get; private set; }
    public int HealthPacksToSave { get; private set; }
    public int EnergyPacksToSave { get; private set; }

    [MenuItem("Utils/Clear progress")]
    public static void ClearProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedCheckPoint.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedCheckPoint.dat");
        }
        if (File.Exists(Application.persistentDataPath + "/MySavedLevel.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedLevel.dat");
        }
        if (File.Exists(Application.persistentDataPath + "/MySavedHealth.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedHealth.dat");
        }
        if (File.Exists(Application.persistentDataPath + "/MySavedEnergy.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedEnergy.dat");
        }
        if (File.Exists(Application.persistentDataPath + "/MySavedHealthPacks.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedHealthPacks.dat");
        }
        if (File.Exists(Application.persistentDataPath + "/MySavedEnergyPacks.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedEnergyPacks.dat");
        }
        Debug.Log("Progress Data Cleared!");
    }

    private void Awake()
    {
        LoadLevel();
        LoadCheckPoint();
        LoadHealth();
        LoadEnergy();
        LoadEnergyPacks();
        LoadHealthPacks();
    }

    public void SaveCheckPoint(int checkPointNumber)
    {
        CheckPointToSave = checkPointNumber;
        Debug.Log($"Saved point = {CheckPointToSave}");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedCheckPoint.dat");
        SaveData data = new SaveData();
        data.SavedCheckPoint = CheckPointToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadCheckPoint()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedCheckPoint.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedCheckPoint.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            CheckPointToSave = data.SavedCheckPoint;
        }
        Debug.Log($"Loaded point = {CheckPointToSave}");
    }

    public void SaveLevel(int levelNumber)
    {
        Debug.Log($"Saved level = {LevelToSave}");
        LevelToSave = levelNumber;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedLevel.dat");
        SaveData data = new SaveData();
        data.SavedLevel = LevelToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadLevel()
    {

        if (File.Exists(Application.persistentDataPath + "/MySavedLevel.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedLevel.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            LevelToSave = data.SavedLevel;
        }
    }

    public void SaveHealth(int health)
    {
        Debug.Log($"Saved Health = {HealthToSave}");
        HealthToSave = health;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedHealth.dat");
        SaveData data = new SaveData();
        data.SavedHealth = HealthToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadHealth()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedHealth.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedHealth.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            HealthToSave = data.SavedHealth;
        }
        if (HealthToSave == 0)
        {
            HealthToSave = 100;
        }
    }

    public void SaveEnergy(int energy)
    {
        Debug.Log($"Saved level = {LevelToSave}");
        EnergyToSave = energy;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedEnergy.dat");
        SaveData data = new SaveData();
        data.SavedEnergy = EnergyToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadEnergy()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedEnergy.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedEnergy.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            EnergyToSave = data.SavedEnergy;
        }

        if (EnergyToSave == 0)
        {
            EnergyToSave = 100;
        }
    }

    public void SaveHealthPacks(int hPacks)
    {
        Debug.Log($"Saved level = {LevelToSave}");
        HealthPacksToSave = hPacks;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedHealthPacks.dat");
        SaveData data = new SaveData();
        data.SavedHealthPacks = HealthPacksToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadHealthPacks()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedHealthPacks.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedHealthPacks.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            HealthPacksToSave = data.SavedHealthPacks;
        }
    }

    public void SaveEnergyPacks(int ePacks)
    {
        Debug.Log($"Saved level = {LevelToSave}");
        EnergyPacksToSave = ePacks;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedEnergyPacks.dat");
        SaveData data = new SaveData();
        data.SavedEnergyPacks = EnergyPacksToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadEnergyPacks()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedEnergyPacks.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedEnergyPacks.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            EnergyPacksToSave = data.SavedEnergyPacks;
        }
    }
}
[Serializable]
class SaveData
{
    public int SavedCheckPoint;
    public int SavedLevel;
    public int SavedHealth;
    public int SavedEnergy;
    public int SavedHealthPacks;
    public int SavedEnergyPacks;
}

