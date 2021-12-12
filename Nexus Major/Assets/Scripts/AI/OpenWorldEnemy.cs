using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject companion;
    public bool[] active;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        companion = GameObject.FindGameObjectWithTag("companion");
    }

    // Update is called once per frame
    void Update()
    { 
        if (companion.GetComponent<CompanionAI>().ongoingmission == false)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > 200)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponentInChildren<AIHealthSystem>().health = 10;
                    transform.GetChild(i).gameObject.GetComponentInChildren<AIHealthSystem>().anim.SetBool("death", false);
                    transform.GetChild(i).gameObject.GetComponentInChildren<AIHealthSystem>().anim.SetBool("onfire", false);
                    transform.GetChild(i).gameObject.GetComponentInChildren<AIHealthSystem>().anim.SetBool("falltodeath", false);
                    transform.GetChild(i).gameObject.GetComponent<CrowdBot>().enabled = true;
                    transform.GetChild(i).gameObject.SetActive(false);
                    //manager.killcount = 0;
                    active[i] = false;
                }
            }
            else
            {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if(active[i] == false)
                        {
                            transform.GetChild(i).gameObject.SetActive(true);
                            active[i] = true;
                        }

                        //transform.GetChild(i).gameObject.GetComponent<CrowdBot>().ResetAgent();
                    }
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                  transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

