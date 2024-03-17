using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour
{

    public void Save(int data, string fileEnding)
    {
        String path = Path.Join(Application.persistentDataPath,fileEnding);
        NumberSave numberSave = new NumberSave();
        numberSave.number = data;
        string json = JsonUtility.ToJson(numberSave);
        System.IO.File.WriteAllText(path, json);
    }

    public int Load(string fileEnding)
    {
        String path = Path.Join(Application.persistentDataPath,fileEnding);
        NumberSave numberSave = new NumberSave();
        //If there is no save file that exists, create one
        if (!File.Exists(path))
        {
            return -1;
        }
        string json = System.IO.File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, numberSave);
        return numberSave.number;
    }

    [System.Serializable]
    public class NumberSave
    {
        public int number;
    }
}
