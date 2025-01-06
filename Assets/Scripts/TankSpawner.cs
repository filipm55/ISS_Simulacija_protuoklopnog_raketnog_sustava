using UnityEngine;
using System.Collections.Generic;

public class TankSpawner : MonoBehaviour
{
    public GameObject tankPrefab; // Prefab tenka
    public List<Transform> spawnPoints; // Lista spawn točaka

    public Transform ActiveTankList; // Roditeljski objekt za tenkove

    void Start()
    {
        // Automatski trazi spawnpointove s tagom SpawnPoint
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

        // Odaberi nasumičnu spawn točku
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject spawnedTank = Instantiate(tankPrefab, randomSpawn.position, randomSpawn.rotation);
        spawnedTank.transform.parent = ActiveTankList; // Stavljam sve tankove u jedan folder
        Debug.Log("Tank respawned" + randomSpawn.position);
    }
}