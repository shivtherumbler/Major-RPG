using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderManager : MonoBehaviour
{
    public bool OnLadder;
    public GameObject top;
    private Animator PlayerAnimator;
    private GameObject Player;
    public GameObject down;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerAnimator = other.gameObject.GetComponent<Animator>();
            PlayerAnimator.SetBool("climbup", false);
            PlayerAnimator.SetBool("laddertop", false);
            top.SetActive(false);
            down.SetActive(true);
            gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerAnimator = other.gameObject.GetComponent<Animator>();
            Player = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //PlayerAnimator = other.gameObject.GetComponent<Animator>();
            //other.gameObject.GetComponent<Player>().enabled = false;
            //PlayerAnimator.applyRootMotion = false;
            //PlayerAnimator.SetBool("climb", true);
            //PlayerAnimator.SetBool("Move", false);
            //StartCoroutine(RotatePlayer(other.transform.rotation, transform.rotation, 0.3f));
            //StartCoroutine(LadderClimb(other.transform.position, transform.position, 0.3f));
            
            top.SetActive(true);
            down.SetActive(false);
        }
    }


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
        PlayerAnimator.applyRootMotion = true;

    }

    private void Update()
    {
        if(PlayerAnimator != null)
        {
            if (PlayerAnimator.GetBool("climb") == true)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    PlayerAnimator.SetBool("climbup", true);
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    PlayerAnimator.SetBool("climbdown", true);
                }
                else
                {
                    PlayerAnimator.SetBool("climbup", false);
                    PlayerAnimator.SetBool("climbdown", false);
                }
            }
        }
        
    }
}