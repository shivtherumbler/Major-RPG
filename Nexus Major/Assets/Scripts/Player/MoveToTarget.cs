using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : MonoBehaviour
{
    public List<Transform> Targets;
    //public NavMeshAgent Agent;
    public Animator anim;
    public Transform closestTarget = null;
    public int enemyno;
    public bool finalplayer;
    public bool[] upgrade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClosestTarget()
    {
        float closestTargetDistance = float.MaxValue;
        for (int i = 0; i < Targets.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, Targets[i].position);

            if(distance < closestTargetDistance)
            {
                closestTargetDistance = distance;
                closestTarget = Targets[i];
                
            }
        }

        if (closestTarget != null)
        {
            if (Vector3.Distance(transform.position, closestTarget.position) > 3)
            {
                if (Input.GetMouseButton(0))
                {
                    //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(closestTarget.transform.position), Time.time);
                    transform.LookAt(new Vector3(closestTarget.position.x, transform.position.y, transform.position.z));
                    anim.applyRootMotion = false;
                    StartCoroutine(GoToEnemy(transform.position, closestTarget.position - transform.forward, 1f));
                    anim.SetBool("Enemy", true);
                    Invoke("RootMotion", 1.25f);

                    //transform.position = closestTarget.position;
                }
            }
        }

        if(Targets.Count == 0)
        {
            closestTarget = null;
        }
        
    }

    /*public void ChooseTarget()
    {
        
        float closestTargetDistance = float.MaxValue;
        NavMeshPath path = null;
        NavMeshPath ShortestPath = null;

        for(int i = 0; i < Targets.Length; i++)
        {
            if(Targets[i] == null)
            {
                continue;
            }
            path = new NavMeshPath();

            if (NavMesh.CalculatePath(transform.position, Targets[i].position, Agent.areaMask, path))
            {
                float distance = Vector3.Distance(transform.position, path.corners[0]);

                for(int j = 1; j < path.corners.Length; j++)
                {
                    distance += Vector3.Distance(path.corners[j - 1], path.corners[j]);
                }

                if(distance < closestTargetDistance)
                {
                    
                   closestTargetDistance = distance;
                   ShortestPath = path;
                   closestTarget = Targets[i];
                    
                }
            }
        }

        if (Vector3.Distance(transform.position, closestTarget.position) > 2)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
                if (ShortestPath != null)
                {
                    anim.SetBool("Enemy", true);
                    Agent.SetPath(ShortestPath);
                    //Agent.SetDestination(closestTarget.position);
                    Agent.speed = 100;

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(closestTarget.transform.position), Time.time);
                }
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("GoToEnemy"))
            {
                transform.position = closestTarget.position;

            }
        }
        else
        {
            anim.SetBool("Enemy", false);
            Agent.speed = 0;
        }

    }*/

    // Update is called once per frame
    void Update()
    {
        ClosestTarget();

        if(finalplayer == true)
        {
            

            enemyno = Random.Range(0, Targets.Count);

            if(upgrade[0] == true)
            {
                if(gameObject.GetComponent<PlayerHealthManager>().mp > 10)
                {
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        anim.SetTrigger("setonfire");
                        if (closestTarget != null)
                        {
                            closestTarget.GetComponentInChildren<AIHealthSystem>().onfire = true;
                            gameObject.GetComponent<PlayerHealthManager>().mp -= 10;
                            gameObject.GetComponent<Player>().audioSource.PlayOneShot(gameObject.GetComponent<Player>().clips[Random.Range(9, 15)]);
                        }
                    }
                }
            }
            if (upgrade[1] == true)
            {
                if (gameObject.GetComponent<PlayerHealthManager>().mp > 49)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        gameObject.GetComponent<Player>().slash[3].SetActive(true);
                        gameObject.GetComponent<Player>().audioSource.PlayOneShot(gameObject.GetComponent<Player>().clips[Random.Range(15,18)]);
                        anim.SetTrigger("FinalAttack");
                        Invoke("SlashOff", 3f);
                        if(Targets != null)
                        {
                            for (int i = 0; i < Targets.Count; i++)
                            {
                                Targets[i].GetComponentInChildren<AIHealthSystem>().health = 0;

                            }
                            gameObject.GetComponent<PlayerHealthManager>().mp -= 50;
                        }

                    }
                }
                    
            }

        }
        
    }

    /*private void OnGUI()
    {
        if(GUI.Button(new Rect(20, 20, 300, 50), "Move To Target"))
        {
            ChooseTarget();
        }
    }*/

    public void RootMotion()
    {
        anim.applyRootMotion = true;
        anim.SetBool("Enemy", false);
    }

    /*IEnumerator GoToEnemy()
    {
        yield return new WaitForSeconds(1f);
        transform.position = Vector3.Lerp(transform.position, closestTarget.position, 1f);

    }*/

    IEnumerator GoToEnemy(Vector3 start, Vector3 end, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            
            transform.position = Vector3.Lerp(new Vector3(start.x, transform.position.y, start.z), new Vector3(end.x, transform.position.y, end.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }

    }

    public void SlashOff()
    {
        gameObject.GetComponent<Player>().slash[3].SetActive(false);
    }

}
