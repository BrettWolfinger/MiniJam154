using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpgradeSaver : MonoBehaviour
{
    public void SaveUpgrade(Dictionary<string,int> data, string fileEnding)
    {
        String path = Path.Join(Application.persistentDataPath,fileEnding);
        UpgradeSave upgradeSave = new UpgradeSave();
        foreach(string key in data.Keys)
        {
            upgradeSave.stats.Add(key);
            upgradeSave.upgradeLevels.Add(data[key]);
        }
        string json = JsonUtility.ToJson(upgradeSave);
        System.IO.File.WriteAllText(path, json);
    }

    public Dictionary<string,int> LoadUpgrade(string fileEnding)
    {
        //initalize necessary containers
        String path = Path.Join(Application.persistentDataPath,fileEnding);
        Dictionary<string,int> dict = new Dictionary<string, int>();
        UpgradeSave upgradeSave = new UpgradeSave();

        //If there is no save file that exists, create one
        if (!File.Exists(path))
        {
            return null;
        }

        //Read and parse data
        string json = System.IO.File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, upgradeSave);
        for(int i = 0; i < upgradeSave.stats.Count; i++)
        {
            dict.Add(upgradeSave.stats[i],upgradeSave.upgradeLevels[i]); 
        }

        //return
        return dict;
    }

    [System.Serializable]
    public class UpgradeSave
    {
        public List<string> stats = new List<string>();
        public List<int> upgradeLevels = new List<int>();
    }
}
