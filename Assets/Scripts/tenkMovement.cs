using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class TenkMovement : MonoBehaviour
{
    public float patrolRadius = 20f;
    public float patrolTime = 3f;
    private NavMeshAgent navMeshAgent;
    private TankSpawner tankSpawner;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        tankSpawner = FindObjectOfType<TankSpawner>();
        StartCoroutine(Patrol());
    }

     IEnumerator Patrol()
     {
        while (true)
        {
            Vector3 randomPosition = RandomNavMeshLocation(patrolRadius);
            navMeshAgent.SetDestination(randomPosition);

            // Čekaj dok ne stigne ili ne prođe patrolTime
            yield return new WaitForSeconds(patrolTime);
        }
     }

     Vector3 RandomNavMeshLocation(float radius)
{
    for (int i = 0; i < 5; i++) // Pokušava 5 puta
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
    }
    Debug.LogWarning("Failed to find valid NavMesh point.");
    return transform.position; // ako u 5 pokusaja ne vrati novu lokaciju gdje treba ici vrati trenutnu poziciju
}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("missile"))
        {
            Debug.Log("Tank hit by missile!");
            DestroyTank();
        }
    }
    void DestroyTank()
    {
        // Pokreni respawn prije uništenja
        if (tankSpawner != null)
        {
            tankSpawner.RespawnTank();
        }
        else
        {
            Debug.LogWarning("No TankSpawner found in the scene!");
        }

        Destroy(gameObject); // Uništi trenutni tenk
    }

}
