using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinimap
{
}

public enum MinimapType
{
    Player,
    Objective,
    Nipper
}

public class MinimapComponent : MonoBehaviour, IMinimap
{
    public MinimapType myType;
    
    public Color nipperColor;
    public Color objectiveColor;
    public Color playerColor;

    public SpriteRenderer rend;

    public void OnEnable()
    {
        Color myColor = nipperColor;
        switch (myType)
        {
            case (MinimapType.Nipper):
                myColor = nipperColor;
                break;
            case (MinimapType.Objective):
                myColor = objectiveColor;
                break;
            case(MinimapType.Player):
                myColor = playerColor;
                break;
            default:
                Debug.LogWarning("Unrecognized MinimapType");
                break;
        }

        rend.color = myColor;
    }
}