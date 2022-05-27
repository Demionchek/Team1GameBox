using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Audio;

public class Saver : MonoBehaviour
{
    public int CheckPointToSave { get; private set; }
    
    private void Start()
    {
        
    }

    public void SaveCheckPoint( int checkPointNumber)
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

}
[Serializable]
class SaveData
{
    public int SavedCheckPoint;
}

