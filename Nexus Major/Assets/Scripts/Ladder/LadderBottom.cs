using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBottom : MonoBehaviour
{
    private Animator animator;
    public GameObject ladder;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "character")
        {
            animator = other.GetComponent<Animator>();
            

                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    animator.SetBool("offladder", true);
                    animator.SetBool("climb", false);
                    other.GetComponent<Player>().enabled = true;
                }
                else
                {
                    animator.SetBool("offladder", false);
                }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ladder.SetActive(true);
    }
}
