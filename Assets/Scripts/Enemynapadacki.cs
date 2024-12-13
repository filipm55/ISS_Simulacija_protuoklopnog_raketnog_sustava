using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemynapadacki : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
       {    
           navMeshAgent.SetDestination(player.position);
       }
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