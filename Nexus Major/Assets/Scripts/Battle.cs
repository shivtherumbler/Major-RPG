using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<CrowdBot>().anim.SetBool("walk", false);
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<CrowdBot>().enabled = false;
                //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = true;       
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().enabled = true;
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = false;
                
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().anim.SetBool("run", false);
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().anim.SetBool("walk", true);
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().anim.SetBool("attack", false);
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().anim.SetBool("back", false);
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().alert.SetActive(false);
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().enabled = false;
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<LineOfSight>().gun.GetComponent<Weapons>().enabled = false;
                other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets[i].GetComponent<CrowdBot>().enabled = true;

            }
            other.GetComponent<SelectPlayer>().finalPlayer.GetComponent<MoveToTarget>().Targets = null;
            

        }
    }
}
