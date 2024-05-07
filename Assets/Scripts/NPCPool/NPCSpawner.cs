using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCPrefab;

    public ComboTracker comboTracker;

    public Transform spawnPos;

    public Transform targetPoint;

    public List<GameObject> spawnedNPCs = new List<GameObject>();

    public List<GameObject> deadNPCS = new List<GameObject>();
    
    [ContextMenu("Spawn NPC")]
    public void SpawnNPC()
    {
        if (spawnedNPCs.Count == 0 || deadNPCS.Count == 0)
        {
            GameObject newNPC = Instantiate(NPCPrefab, spawnPos.position, spawnPos.rotation);
            spawnedNPCs.Add(newNPC);

            NipperSensor sensor = newNPC.GetComponent<NipperSensor>();
            sensor.target = targetPoint;

            HealthComponent HP = newNPC.GetComponent<HealthComponent>();
            HP.AnnounceGameObject += AddToDeathPool;
            
            //where to unsubscribe?
            HP.AnnounceDeath += comboTracker.IncreaseCombo;
            HP.Resurrect();
        }
        else if (deadNPCS.Count > 0)
        {
            int randomIndex = Random.Range(0, deadNPCS.Count);
            GameObject NPC = deadNPCS[randomIndex];
            deadNPCS.Remove(NPC);
            NPC.SetActive(true);
            NPC.transform.position = spawnPos.transform.position;
            NPC.transform.rotation = spawnPos.transform.rotation;
            NipperSensor sensor = NPC.GetComponent<NipperSensor>();
            sensor.target = targetPoint;
            HealthComponent HP = NPC.GetComponent<HealthComponent>();
            HP.Resurrect();
            
        }
    }

    public void AddToDeathPool(GameObject obj)
    {
        if (!deadNPCS.Contains(obj))
        {
            deadNPCS.Add(obj);
        }
    }
}
