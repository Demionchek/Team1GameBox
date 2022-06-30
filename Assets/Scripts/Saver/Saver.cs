using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Audio;
using UnityEditor;

[Serializable]
class SaveData
{
    public int SavedCheckPoint;
    public int SavedLevel;
    public int SavedHealth;
    public int SavedEnergy;
    public int SavedHealthPacks;
    public int SavedEnergyPacks;
    public int SavedPassedArenas;
    public int SavedCollectedCount;
    public string SavedStringScrolls;
    public int SavedDialogSkip;
}

public class Saver : MonoBehaviour
{
    public int CheckPointToSave { get; private set; }
    public int LevelToSave { get; private set; }
    public int HealthToSave { get; private set; }
    public int EnergyToSave { get; private set; }
    public int HealthPacksToSave { get; private set; }
    public int EnergyPacksToSave { get; private set; }
    public int ArenaNumToSave { get; private set; }
    public int CollectToSave { get; private set; }
    public int FirstScroll { get; private set; }
    public int SecondScroll { get;private set; }
    public int ThirdScroll { get; private set; }
    public int FourthScroll { get; private set; }
    public int DialogToSkip { get; private set; }

    private int[] Scrolls = new int[4];
    private string ScrollsStringToSave;

//#if(UNITY_EDITOR)
//    [MenuItem("Utils/Clear progress")]
//    public static void ClearProgress()
//    {
//        if (File.Exists(Application.persistentDataPath + "/MySavedCheckPoint.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedCheckPoint.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedLevel.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedLevel.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedHealth.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedHealth.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedEnergy.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedEnergy.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedHealthPacks.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedHealthPacks.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedEnergyPacks.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedEnergyPacks.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedArena.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedArena.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedCollectable.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedCollectable.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedScrolls.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedScrolls.dat");
//        }
//        if (File.Exists(Application.persistentDataPath + "/MySavedDialog.dat"))
//        {
//            File.Delete(Application.persistentDataPath + "/MySavedDialog.dat");
//        }
//        Debug.Log("Progress Data Cleared!");
//    }
//#endif

    //private void Awake()
    //{
    //    LoadLevel();
    //    LoadCheckPoint();
    //    LoadHealth();
    //    LoadEnergy();
    //    LoadEnergyPacks();
    //    LoadHealthPacks();
    //    LoadScrollsNums();
    //}

    public void SaveScrolls(int ScrollNum)
    {
        Scrolls[ScrollNum] = 1;
        ScrollsStringToSave = $"{Scrolls[0]}|{Scrolls[1]}|{Scrolls[2]}|{Scrolls[3]}";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedScrolls.dat");
        SaveData data = new SaveData();
        data.SavedStringScrolls = ScrollsStringToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadScrollsNums()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedScrolls.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedScrolls.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            ScrollsStringToSave = data.SavedStringScrolls;
            try
            {
                string[] Numbers = ScrollsStringToSave.Split('|');
                for (int i = 0; i < Scrolls.Length; i++)
                {
                    Scrolls[i] = Int32.Parse(Numbers[i]);
                }
                FirstScroll = Scrolls[0];
                SecondScroll = Scrolls[1];
                ThirdScroll = Scrolls[2];
                FourthScroll = Scrolls[3];
            }
            catch
            {
            }

        }
    }

    public void SaveDialog(int dialog)
    {
        DialogToSkip = dialog;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedDialog.dat");
        SaveData data = new SaveData();
        data.SavedDialogSkip = DialogToSkip;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadDialog()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedDialog.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedDialog.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            DialogToSkip = data.SavedDialogSkip;
        }

    }

    public void SaveArenaNum(int arenaNum)
    {
        ArenaNumToSave = arenaNum;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedArena.dat");
        SaveData data = new SaveData();
        data.SavedPassedArenas = ArenaNumToSave;
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadArenas()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedArena.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedArena.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            ArenaNumToSave = data.SavedPassedArenas;
        }

    }

    public void SaveCollectableNum(int collectable)
    {
        CollectToSave = collectable;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySavedCollectable.dat");
        SaveData data = new SaveData();
        data.SavedCollectedCount = CollectToSave;
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadCollectable()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedCollectable.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySavedCollectable.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            CollectToSave = data.SavedCollectedCount;
        }
        

    }

    public void SaveCheckPoint(int checkPointNumber)
    {
        CheckPointToSave = checkPointNumber;
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

    }

    public void SaveLevel(int levelNumber)
    {
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


