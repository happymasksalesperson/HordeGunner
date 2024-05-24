using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{
    public List<GameObject> spawners;
    public float radius;
    public float waitTime = 1.0f;

    private Coroutine spawnCoroutine;

    private bool spawning = false;

    void OnEnable()
    {
        ArrangeSpawnersInCircle();
    }

    void ArrangeSpawnersInCircle()
    {
        int numSpawners = spawners.Count;

        float angleStep = 360f / numSpawners;

        for (int i = 0; i < numSpawners; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            Vector3 spawnerPosition = new Vector3(x, 0f, z);

            spawners[i].transform.localPosition = spawnerPosition;
        }
    }
    
    public void StartSpawnCoroutine()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);

        spawning = true;
        spawnCoroutine = StartCoroutine(SpawnRepeatedly());
    }

    public void StopSpawn()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        
        spawning = false;

        foreach (GameObject spawner in spawners)
        {
            NPCSpawner newSpawner = spawner.GetComponent<NPCSpawner>();
            newSpawner.GameOver();
        }
    }

    IEnumerator SpawnRepeatedly()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(waitTime);
            Spawn();
        }
    }
    
    void Spawn()
    {
        foreach (GameObject spawner in spawners)
        {
            NPCSpawner newSpawner = spawner.GetComponent<NPCSpawner>();
            newSpawner.SpawnNPC();
        }
    }
}