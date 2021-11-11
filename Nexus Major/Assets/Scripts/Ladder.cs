using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Animator animator;
    public bool down;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (animator.GetBool("climb") == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                animator.SetBool("climbup", true);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("climbdown", true);
            }
            else
            {
                animator.SetBool("climbup", false);
                animator.SetBool("climbdown", false);
            }
        }
    }*/

    IEnumerator RotatePlayer(Quaternion start, Quaternion end, float Duration)
    {
        float t = 0f;
        end = new Quaternion(0, end.y, 0, end.w);
        while (t < Duration)
        {
            transform.localRotation = Quaternion.Slerp(start, end, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        //Turn = transform.eulerAngles.y;
    }

    IEnumerator LadderClimb(Vector3 start, Vector3 end, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(new Vector3(start.x, transform.position.y, start.z), new Vector3(end.x, transform.position.y, end.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        animator.applyRootMotion = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ladder")
        {
            animator.applyRootMotion = false;
            animator.SetBool("climb", true);
            animator.SetBool("Move", false);
            StartCoroutine(RotatePlayer(transform.rotation, other.transform.rotation, 0.3f));
            StartCoroutine(LadderClimb(transform.position, other.transform.position, 0.3f));
            gameObject.GetComponent<Player>().enabled = false;
        }

        if(other.tag == "comedown")
        {
            animator.applyRootMotion = false;
            animator.SetBool("ladderdown", true);
            animator.SetBool("climb", false);
            animator.SetBool("climbup", false);
            StartCoroutine(RotatePlayer(transform.rotation, other.transform.rotation, 0.3f));
            StartCoroutine(LadderClimb(transform.position, other.transform.position - transform.up, 0.3f));
            gameObject.GetComponent<Player>().enabled = false;
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ladder")
        {
            animator.SetBool("climb", false);
            gameObject.GetComponent<Player>().enabled = true;
        }

        if (other.tag == "laddertop")
        {
            animator.SetBool("climb", false);
            animator.SetBool("laddertop", false);
        }
    }*/

    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ladderbottom")
        {

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("offladder", true);
                animator.SetBool("climb", false);
                gameObject.GetComponent<Player>().enabled = true;
            }
            else
            {
                animator.SetBool("offladder", false);
            }
        }

        if (other.tag == "laddertop")
        {*/
    /*if(down == false)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            animator.SetBool("laddertop", true);
            animator.SetBool("climb", false);
            StartCoroutine(LadderClimb(transform.position, other.transform.position, 2f));
            gameObject.GetComponent<Player>().enabled = true;
            Invoke("DownOn", 2f);
        }
        else
        {
            animator.SetBool("laddertop", false);
            animator.SetBool("ladderdown", false);
            gameObject.GetComponent<Player>().enabled = true;
        }
    }*/

    /*else if(down == true)
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

            animator.SetBool("ladderdown", true);
            animator.SetBool("climb", false);
            animator.SetBool("climbup", false);
            StartCoroutine(LadderClimb(other.transform.position, other.transform.position - transform.forward, 2f));
            gameObject.GetComponent<Player>().enabled = false;
            Invoke("DownOff", 2f);
        }
        else
        {
            animator.SetBool("laddertop", false);
            animator.SetBool("ladderdown", false);
            gameObject.GetComponent<Player>().enabled = false;
        }
    }*/

    /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
    {

        animator.SetBool("laddertop", true);
        animator.SetBool("climb", false);
        StartCoroutine(LadderClimb(transform.position, other.transform.position, 2f));
        gameObject.GetComponent<Player>().enabled = true;
        //Invoke("DownOn", 2f);
    }

    else
    {
        animator.SetBool("laddertop", false);
        animator.SetBool("ladderdown", false);
    }
}
}*/

    public void DownOn()
    {
        down = true;
    }

    public void DownOff()
    {
        down = false;
    }    

}
