using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboTracker : MonoBehaviour
{
    public int combo;
    public float decayTimer;
    public float decayRate = 1.0f;

    public event Action<string, int> AnnounceComboStrings;

    public event Action<float> AnnounceComboTimer;

    public enum ComboName
    {
        Decent,
        Cool,
        Badass,
        Awesome,
        SWEET,
        SSUBLIME,
        SSSICK
    }

    void Start()
    {
        combo = 0;
        decayTimer = 0;
        StartCoroutine(ComboDecay());
    }

    string GetComboName(int comboValue)
    {
        ComboName comboName = ComboName.Decent;

        if (comboValue >= 1 && comboValue <= 5)
            comboName = ComboName.Decent;
        else if (comboValue >= 6 && comboValue <= 10)
            comboName = ComboName.Cool;
        else if (comboValue >= 11 && comboValue <= 15)
            comboName = ComboName.Badass;
        else if (comboValue >= 16 && comboValue <= 20)
            comboName = ComboName.Awesome;
        else if (comboValue >= 21 && comboValue <= 25)
            comboName = ComboName.SWEET;
        else if (comboValue >= 26 && comboValue <= 30)
            comboName = ComboName.SSUBLIME;
        else if (comboValue > 30)
            comboName = ComboName.SSSICK;

        return comboName.ToString();
    }

    [ContextMenu("Increase Combo")]
    public void IncreaseCombo()
    {
        combo++;
        decayTimer = 5.0f;
        
        AnnounceComboStrings?.Invoke(GetComboName(combo), combo);
    }

    public IEnumerator ComboDecay()
    {
        while (true)
        {
            decayTimer -= Time.deltaTime * decayRate;
            if (decayTimer <= 0)
            {
                combo = 0;
                decayTimer = 0;
                AnnounceComboStrings?.Invoke(GetComboName(combo), combo);
            }
            
            AnnounceComboTimer.Invoke(decayTimer);
            
            yield return null;
        }
    }
}