using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<MoveToTarget>().Targets = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                other.GetComponent<MoveToTarget>().Targets[i] = transform.GetChild(i);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<StealthEnemy>().anim.SetBool("walk", false);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<StealthEnemy>().enabled = false;
                //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = true;       
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().enabled = true;

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

                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().anim.SetBool("run", false);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().anim.SetBool("walk", true);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().anim.SetBool("attack", false);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().anim.SetBool("back", false);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().alert.SetActive(false);
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().enabled = false;
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<PlayerDetection>().gun.GetComponent<Weapons>().enabled = false;
                other.GetComponent<MoveToTarget>().Targets[i].GetComponent<StealthEnemy>().enabled = true;

            }
            other.GetComponent<MoveToTarget>().Targets = null;


        }
    }
}
