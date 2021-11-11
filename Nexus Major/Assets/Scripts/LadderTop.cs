using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : MonoBehaviour
{
    private Animator animator;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            animator = other.GetComponent<Animator>();


            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {

                animator.SetBool("laddertop", true);
                animator.SetBool("climb", false);
                StartCoroutine(LadderClimb(other.transform.position, transform.position, 2f));
                other.GetComponent<Player>().enabled = true;
                
            }
            else
            {
                animator.SetBool("laddertop", false);
                animator.SetBool("ladderdown", false);
                other.GetComponent<Player>().enabled = true;
            }

        }
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
}
