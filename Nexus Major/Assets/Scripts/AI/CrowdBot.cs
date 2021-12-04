using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CrowdBot : MonoBehaviour
{
    public GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent agent;
    public Animator anim;
    private float sppedMult;
    private float detectionRadius = 25;
    private float fleeRadius = 15;
    public GameObject player;
    public RaycastHit hitinfo;
    public GameObject gameoverPanel;
    public bool addedinlist;

    void Start()
    {
        //goalLocations = GameObject.FindGameObjectsWithTag("goal");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = this.GetComponent<Animator>();
        GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
        ResetAgent();
    }

    private void ResetAgent()
    {
        //anim.SetFloat("wOffset", Random.Range(0,1));
        //anim.SetTrigger("isWalking");
        float sppedMult = Random.Range(0.35f, 1.5f);
        //anim.SetFloat("speedMult", sppedMult);
        agent.speed *= sppedMult;
        agent.angularSpeed = 120;
        agent.ResetPath();
        if (goalLocations.Length > 1)
        {
            anim.SetBool("walk", true);

        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);

        }
        else
        {
            anim.SetBool("walk", true);
        }

        agent.speed = 0.1f;

        if (Physics.Raycast(transform.position + transform.up, player.transform.position - transform.position, out hitinfo, 10))
        {
            Debug.Log(hitinfo.transform.gameObject.name);
            if (hitinfo.transform.gameObject == player)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 10)
                {
                    anim.SetBool("walk", false);
                    player.GetComponent<Player>().animator.SetBool("Battle", true);
                    player.GetComponent<Player>().weapon.SetActive(true);
                    player.GetComponent<MoveToTarget>().enabled = true;
                    if(addedinlist == false)
                    {
                        player.GetComponent<MoveToTarget>().Targets.Add(transform);
                        addedinlist = true;
                    }
                    
                    gameObject.GetComponent<LineOfSight>().enabled = true;
                }

            }
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("walk", true);
            anim.SetBool("attack", false);
            player.GetComponent<Player>().animator.SetBool("Battle", false);
            //player.GetComponent<Player>().weapon.SetActive(false);
            gameObject.GetComponent<LineOfSight>().alert.SetActive(false);
            gameObject.GetComponent<LineOfSight>().enabled = false;
            gameObject.GetComponent<LineOfSight>().gun.GetComponent<Weapons>().enabled = false;
            gameObject.GetComponent<LineOfSight>().enabled = false;
            player.GetComponent<MoveToTarget>().Targets.Remove(transform);
            addedinlist = false;
            //player.GetComponent<MoveToTarget>().enabled = false;
        }
    }
    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);

                /*if (gameObject.name == "liam")
                {
                    print("corners are : " + path.corners.Length);
                }*/

                //anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    public void Gameover()
    {
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
    }
}

