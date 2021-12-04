using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthSystem : MonoBehaviour
{
    public int health;
    public GameObject parent;
    public Animator anim;
    //public GameObject companion;
    public MissionManager manager;
    public bool killed;
    // Start is called before the first frame update
    void Start()
    {
        anim = parent.GetComponent<Animator>();
        //companion = GameObject.FindGameObjectWithTag("companion");
    }

    // Update is called once per frame
    void Update()
    {
        if(health <=0)
        {
            anim.SetBool("death", true);
            parent.GetComponent<LineOfSight>().gun.GetComponent<Weapons>().enabled = false;
            parent.GetComponent<CrowdBot>().enabled = false;
            parent.GetComponent<LineOfSight>().enabled = false;
            parent.GetComponent<Weapons>().enabled = false;
            Invoke("SetactiveFalse", 10);
        }

        if (anim.GetBool("death") == true)
        {
            anim.SetBool("heavyhit", false);
            anim.SetBool("lighthit", false);
            parent.GetComponent<LineOfSight>().finalPlayer.GetComponent<MoveToTarget>().Targets.Remove(parent.transform);
            parent.GetComponent<LineOfSight>().finalPlayer.GetComponent<Player>().animator.SetBool("Battle", false);
            
            if(killed == false)
            {
                /*if (companion.GetComponent<CompanionAI>().missionno == 0)
                {
                    companion.GetComponent<CompanionAI>().missions[0].GetComponent<Mission1>().killcount++;
                    killed = true;
                }
                else if (companion.GetComponent<CompanionAI>().missionno == 1)
                {
                    companion.GetComponent<CompanionAI>().missions[1].GetComponent<Mission1>().killcount++;
                    killed = true;
                }*/
                manager.killcount++;
                killed = true;
            }
            
            //parent.GetComponent<LineOfSight>().finalPlayer.GetComponent<MoveToTarget>().enabled = false;

            //for (int i = 0; i < transform.childCount; i++)
            //{

                //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = false;

                parent.GetComponent<LineOfSight>().anim.SetBool("run", false);
                parent.GetComponent<LineOfSight>().anim.SetBool("walk", true);
                parent.GetComponent<LineOfSight>().anim.SetBool("attack", false);
                parent.GetComponent<LineOfSight>().anim.SetBool("back", false);
                parent.GetComponent<LineOfSight>().alert.SetActive(false);
                parent.GetComponent<LineOfSight>().enabled = false;
                parent.GetComponent<LineOfSight>().gun.GetComponent<Weapons>().enabled = false;
                parent.GetComponent<CrowdBot>().enabled = true;

            //}
            //parent.GetComponent<LineOfSight>().finalPlayer.GetComponent<MoveToTarget>().Targets = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "sword")
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                health--;
                anim.SetBool("lighthit", true);
            }

            else if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftShift))
            {
                health = health - 3;
                anim.SetBool("heavyhit", true);
            }
            else
            {
                anim.SetBool("heavyhit", false);
                anim.SetBool("lighthit", false);
            }
        }
    }

    public void SetactiveFalse()
    {
        //parent.GetComponent<LineOfSight>().finalPlayer.GetComponent<Player>().weapon.SetActive(false);
        parent.SetActive(false);
    }

    public void hitfalse()
    {
        anim.SetBool("heavyhit", false);
        anim.SetBool("lighthit", false);
    }
}
