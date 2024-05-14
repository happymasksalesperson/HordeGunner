using UnityEngine;

public class MusicVisualiser2 : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject bassVisualizerPrefab;
    public GameObject percussionVisualizerPrefab;
    public GameObject melodyVisualizerPrefab;
    public int bassBandSize = 16;
    public int percussionBandSize = 32;
    public int melodyBandSize = 16;
    public float visualizerScale = 5f;
    public float spacing = 1f;
    public float categorySpacing = 5f; // Space between categories

    private GameObject[] bassVisualizers;
    private GameObject[] percussionVisualizers;
    private GameObject[] melodyVisualizers;
    private float[] spectrumData;

    public AudioClip audioClip;

    void Start()
    {
        // Initialize visualizers
        int totalBands = bassBandSize + percussionBandSize + melodyBandSize;
        spectrumData = new float[totalBands];

        // Calculate total length of visualizer line
        float totalLength = (bassBandSize + percussionBandSize + melodyBandSize - 1) * spacing + categorySpacing * 2;

        // Create visualizer objects for bass bands
        bassVisualizers = CreateVisualizers(bassVisualizerPrefab, bassBandSize, -totalLength / 2f + categorySpacing);

        // Create visualizer objects for percussion bands
        percussionVisualizers = CreateVisualizers(percussionVisualizerPrefab, percussionBandSize, -totalLength / 2f + categorySpacing + bassBandSize * spacing + categorySpacing);

        // Create visualizer objects for melody bands
        melodyVisualizers = CreateVisualizers(melodyVisualizerPrefab, melodyBandSize, -totalLength / 2f + categorySpacing + (bassBandSize + percussionBandSize) * spacing + categorySpacing);

        // Assign the AudioClip to the AudioSource
        audioSource.clip = audioClip;

        // Play the audio clip
        audioSource.Play();
    }

    void Update()
    {
        // Get spectrum data from audio source
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Update bass visualizers based on spectrum data
        UpdateVisualizers(bassVisualizers, 0, bassBandSize);

        // Update percussion visualizers based on spectrum data
        UpdateVisualizers(percussionVisualizers, bassBandSize, bassBandSize + percussionBandSize);

        // Update melody visualizers based on spectrum data
        UpdateVisualizers(melodyVisualizers, bassBandSize + percussionBandSize, spectrumData.Length);
    }

    GameObject[] CreateVisualizers(GameObject visualizerPrefab, int bandSize, float startX)
    {
        GameObject[] visualizers = new GameObject[bandSize];
        for (int i = 0; i < bandSize; i++)
        {
            GameObject visualizer = Instantiate(visualizerPrefab, transform);
            visualizer.transform.position = new Vector3(startX + i * spacing, 0, 0);
            visualizers[i] = visualizer;
        }
        return visualizers;
    }

    [ContextMenu("Update Visualiser")]
    void UpdateVisualizers(GameObject[] visualizers, int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            // Update visualizer scale based on spectrum data
            Vector3 newScale = visualizers[i - startIndex].transform.localScale;
            newScale.y = Mathf.Clamp(spectrumData[i] * visualizerScale, 0.1f, 10f);
            visualizers[i - startIndex].transform.localScale = newScale;
        }
    }
}
