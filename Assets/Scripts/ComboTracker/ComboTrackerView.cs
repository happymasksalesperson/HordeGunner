using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboTrackerView : MonoBehaviour
{
    public TMP_Text comboText;
    public TMP_Text comboNameText;

    public Slider comboDecaySlider;

    public ComboTracker cT;

    void OnEnable()
    {
        cT = GetComponentInParent<ComboTracker>();
        cT.AnnounceComboStrings += UpdateComboNameText;
        cT.AnnounceComboTimer += UpdateComboTimer;
    }

    private void UpdateComboTimer(float decayTimer)
    {
        float clampedValue = Mathf.Clamp(decayTimer, 0f, 1f);
        comboDecaySlider.value = clampedValue;
    }

    void UpdateComboNameText(string comboName, int combo)
    {
        if (combo == 0)
        {
            comboText.text = "";
            comboNameText.text = "";
        }
        else
        {
            comboText.text = combo.ToString();
            
            // Add exclamation marks for specific combo names
            if (comboName == ComboTracker.ComboName.SWEET.ToString())
                comboName += "!";
            else if (comboName == ComboTracker.ComboName.SSUBLIME.ToString())
                comboName += "!!";
            else if (comboName == ComboTracker.ComboName.SSSICK.ToString())
                comboName += "!!!";
            
            comboNameText.text = comboName;
        }
    }

    private void OnDisable()
    {
        cT.AnnounceComboStrings -= UpdateComboNameText;
        cT.AnnounceComboTimer -= UpdateComboTimer;
    }
}
