using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboTracker : MonoBehaviour
{
    public TMP_Text comboText;
    public TMP_Text comboNameText;
    public int combo;
    public float decayTimer;
    public float decayRate = 1.0f;

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

    void UpdateComboNameText()
    {
        if (combo == 0)
        {
            comboText.text = "";
            comboNameText.text = "";
        }
        else
        {
            comboText.text = "Combo: " + combo.ToString();
            string comboNameString = GetComboName(combo).ToString();
            
            // Add exclamation marks for specific combo names
            if (comboNameString == ComboName.SWEET.ToString())
                comboNameString += "!";
            else if (comboNameString == ComboName.SSUBLIME.ToString())
                comboNameString += "!!";
            else if (comboNameString == ComboName.SSSICK.ToString())
                comboNameString += "!!!";
            
            comboNameText.text = comboNameString;
        }
    }

    [ContextMenu("Increase Combo")]
    public void IncreaseCombo()
    {
        combo++;
        decayTimer = 5.0f;
        UpdateComboNameText();
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
                UpdateComboNameText();
            }

            yield return null;
        }
    }
}