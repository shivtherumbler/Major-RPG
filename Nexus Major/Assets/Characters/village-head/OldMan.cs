using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldMan : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public GameObject bench;
    public bool sitting;
    public GameObject market;
    public GameObject cutscenecollider;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer;
        bench = GameObject.FindGameObjectWithTag("bench");
    }

    // Update is called once per frame
    void Update()
    {
        if(sitting == false)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 7)
            {
                animator.SetBool("Stand", true);
                sitting = true;
            }
        }
        if(animator.GetBool("Stand") == true)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 2)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
            if(cutscenecollider != null)
            {
                cutscenecollider.SetActive(false);

            }
            agent.SetDestination(player.transform.position);
            player.GetComponent<MissionWaypoint>().target = market.transform;

        }
        //transform.LookAt(player.transform);


        
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), bench.GetComponent<Collider>(), ignore: true);
    }
}
