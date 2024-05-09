using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<NPCSpawner> spawners = new List<NPCSpawner>();

    public int interval;
    
    public void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        foreach (NPCSpawner spwn in spawners)
        {
            spwn.SpawnNPC();
        }

        yield return new WaitForSeconds(interval);
        StartCoroutine(Spawn());
    }
}
