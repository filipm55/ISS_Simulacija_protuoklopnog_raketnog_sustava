using UnityEngine;
using System.Collections.Generic;

public class TankSpawner : MonoBehaviour
{
    public GameObject tankPrefab; 
    public List<Transform> spawnPoints; 

    public Transform ActiveTankList; 

    void Start()
    {
        if (spawnPoints.Count == 0)
        {
            foreach (GameObject spawnPoint in GameObject.FindGameObjectsWithTag("SpawnPoint"))
            {
                spawnPoints.Add(spawnPoint.transform);
            }
        }
    }

    public void RespawnTank()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available!");
            return;
        }
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject spawnedTank = Instantiate(tankPrefab, randomSpawn.position, randomSpawn.rotation);
        spawnedTank.transform.parent = ActiveTankList; 
        Debug.Log("Tank respawned" + randomSpawn.position);
    }
}