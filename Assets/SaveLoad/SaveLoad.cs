using System.IO;
using UnityEngine;

public class SaveLoad
{
    public static void SaveGame(string fileName, SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        string filePath = Application.persistentDataPath + "/" + fileName+".json";
        File.WriteAllText(filePath, json);
    }

    public static SaveData LoadGame(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName +".json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogErrorFormat("SaveLoad, LoadGame(), file {0} does not exist in Application.persistentDataPath", fileName);
            return null;
        }
    }
}