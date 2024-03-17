using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    string path;

    private void Awake() {
        path = Application.persistentDataPath;
    }

    void OnEnable()
    {
        PhaseManager.DiedGameplayPhase += DeleteSaveData;
    }

    void OnDisable()
    {
        PhaseManager.DiedGameplayPhase -= DeleteSaveData;
    }

    public void DeleteSaveData()
    {
        if (Directory.Exists(path))
        {
            // Delete all files in a directory
            string[] files = Directory.GetFiles(path, "*.json");
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
    }
}
