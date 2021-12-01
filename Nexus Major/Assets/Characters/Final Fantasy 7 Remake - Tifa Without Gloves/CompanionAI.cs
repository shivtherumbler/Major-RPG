using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class CompanionAI : MonoBehaviour
{
    public GameObject[] player;
    public Animator animator;
    public bool start;
    public bool small;
    public GameObject house;
    public GameObject cutscenecollider;
    public CinemachineVirtualCamera CurrentCam;
    public CinemachineFreeLook EscapeCam;

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
       
        if(start == true)
        {
            if (Vector3.Distance(transform.position, house.transform.position) > 1)
            {
                if (Vector3.Distance(transform.position, player[0].transform.position) < 10)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    agent.speed = 1f;
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
                if(player[0].activeInHierarchy)
                {
                    transform.LookAt(player[0].transform);
                }
                else if(player[1].activeInHierarchy)
                {
                    transform.LookAt(player[1].transform);
                }
                
                if (Vector3.Distance(transform.position, player[1].transform.position) < 5)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        MissionSelect(CurrentCam);
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        EscapeMission(EscapeCam);
                    }
                }    
            }
            if (cutscenecollider != null)
            {
                cutscenecollider.SetActive(false);

            }
            agent.SetDestination(house.transform.position);
            if(small == true)
            {
                player[0].GetComponent<MissionWaypoint>().target = house.transform;
            }
            
        }

        //transform.LookAt(player.transform);

    }

    public void MissionSelect(CinemachineVirtualCamera NextCam)
    {
        EscapeCam.Priority = 0;
        NextCam.Priority = 21;
        //CurrentCam = NextCam;
    }

    public void EscapeMission(CinemachineFreeLook NextCam)
    {
        CurrentCam.Priority = 0;
        NextCam.Priority = 21;
        //EscapeCam = NextCam;
    }
}
