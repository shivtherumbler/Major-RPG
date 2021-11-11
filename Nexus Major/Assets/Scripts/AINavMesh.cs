using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    //public GameObject goal;
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(goal.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Player.transform.position);
    }
}
