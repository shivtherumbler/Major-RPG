using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roshi : MonoBehaviour
{
    public GameObject[] player;
    public Animator animator;
    public GameObject house;
    public GameObject stick;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {

        player[0] = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer;
        player[1] = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, house.transform.position) > 1)
        {
            if (Vector3.Distance(transform.position, player[0].transform.position) < 10)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                agent.speed = 0.2f;
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Idle", true);
                agent.speed = 0;
            }

        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);
            agent.speed = 0;
            if (player[0].activeInHierarchy)
            {
                transform.LookAt(player[0].transform);
            }
            else if (player[1].activeInHierarchy)
            {
                transform.LookAt(player[1].transform);
            }
        }
        agent.SetDestination(house.transform.position);
        player[0].GetComponent<MissionWaypoint>().target = house.transform;

        if(animator.GetBool("Walk") == true)
        {
            stick.SetActive(true);
        }
        else
        {
            stick.SetActive(false);
        }
    }
}
