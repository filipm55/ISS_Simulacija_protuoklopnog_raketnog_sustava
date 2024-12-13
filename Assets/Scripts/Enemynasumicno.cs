using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemynasumicno : MonoBehaviour
{
    public float patrolRadius = 20f;
    public float patrolTime = 3f;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position; // Ako nema pogodaka, vrati trenutnu poziciju
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("missile"))
        {
            Debug.Log("Enemy hit by missile!");
            Destroy(gameObject);
        }
    }
}