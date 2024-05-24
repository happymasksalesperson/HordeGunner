using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public static SaveData instance;

    public int highscore;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}