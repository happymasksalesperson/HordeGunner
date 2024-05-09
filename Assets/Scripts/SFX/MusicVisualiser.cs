using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MusicVisualiser : MonoBehaviour
{
    public MusicPlayer SFX;

    public Transform musicVisObj;

    public List<Image> column01 = new List<Image>();
    public List<Image> column02 = new List<Image>();
    public List<Image> column03 = new List<Image>();
    public List<Image> column04 = new List<Image>();

    public Color lightGreenColor;
    public Color lightYellowColor;
    public Color lightRedColor;
    public Color darkGreenColor;
    public Color darkYellowColor;
    public Color darkRedColor;
    public Color magicColor;

    public float BPM;
    
    private void OnEnable()
    {
        InitializeColumnColors(column01);
        InitializeColumnColors(column02);
        InitializeColumnColors(column03);
        InitializeColumnColors(column04);

        SFX.AnnounceSongPlay += SetBPM;
    }
    
    private void SetBPM(AudioClip clip, float newBPM)
    {
        BPM = newBPM;
        StopAllCoroutines(); // Stop existing coroutines if any song was already playing
        float beatTime = (60 / BPM);
        StartCoroutine(ControlVisualization(column01, 3, beatTime / 4));
        StartCoroutine(ControlVisualization(column02, 5, beatTime / 2));
        StartCoroutine(ControlVisualization(column03, 7, beatTime / 4));
        StartCoroutine(ControlVisualization(column04, 9, beatTime / 2));
    }
    
    IEnumerator ControlVisualization(List<Image> column, int maxValue, float cycleDuration)
    {
        float elapsed = 0.0f;
        float halfCycle = cycleDuration / 2;
    
        while (true)
        {
            while (elapsed < halfCycle)
            {
                int currentValue = Mathf.FloorToInt(Mathf.Lerp(1, maxValue + 1, elapsed / halfCycle)); // Lerp between 1 and max + 1
                LightUpColumn(column, currentValue); // Update lights
                yield return null;
                elapsed += Time.deltaTime;
            }

            ResetColumnColors(column);
            elapsed = 0.0f;

            // Waiting the second half of the cycle
            yield return new WaitForSeconds(halfCycle);
        }
    }
    
    private void InitializeColumnColors(List<Image> column)
    {
        int third = column.Count / 3;
        int twoThirds = 2 * third;

        // Loop through each image in the column list and assign colors based on image position
        for (int i = 0; i < column.Count; i++)
        {
            if (i < third)
            {
                column[i].color = darkRedColor;
            }
            else if (i < twoThirds)
            {
                column[i].color = darkYellowColor;
            }
            else
            {
                column[i].color = darkGreenColor;
            }
        }
    }

    public int testAmount;

    public int textColumn;

    [ContextMenu("Light Up")]
    public void LightUp()
    {
        LightUpColumn(RetrieveColumn(textColumn), testAmount);
    }

    [ContextMenu("Light Off")]
    public void LightOff()
    {
        InitializeColumnColors(RetrieveColumn(textColumn));
    }

    public void LightUpColumn(List<Image> column, int value)
    {
        int third = column.Count / 3;
        int twoThirds = 2 * third;

        // Loop through each image in the column list and assign colors based on image position, starting from the bottom
        for (int i = 0; i < value; i++)
        {
            int index = column.Count - 1 - i; // Calculate index from the end of the list

            if (index < third)
            {
                column[index].color = lightRedColor;
            }
            else if (index < twoThirds)
            {
                column[index].color = lightYellowColor;
            }
            else
            {
                column[index].color = lightGreenColor;
            }
        }
    }

    private List<Image> RetrieveColumn(int columnNumber)
    {
        switch (columnNumber)
        {
            case 1: return column01;
            case 2: return column02;
            case 3: return column03;
            case 4: return column04;
            default: return null;
        }
    }

    private void ResetColumnColors(List<Image> column)
    {
        InitializeColumnColors(column); // Reinitialize colors to default dark scheme
    }

    void OnDisable()
    {
        SFX.AnnounceSongPlay -= SetBPM;
    }
}