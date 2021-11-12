using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoungPlayer : MonoBehaviour
{
    public Animator animator;

    private CharacterController characterController;
    public float RotationSpeed = 240.0f;
    private Vector3 moveDir = Vector3.zero;
    public float Speed = 2.0f;
    public GameObject Cam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        if (v < 0)
            v = 0;

        transform.Rotate(0, h * RotationSpeed * Time.deltaTime, 0);

        if (characterController.isGrounded)
        {
            bool move = (v > 0 || h != 0);
            float velocity = h * h + v * v;


            if (v > 0 || h != 0)
            {

                animator.SetBool("Move", true);
                //animator.SetBool("Back", false);
                if (Input.GetAxis("Fire3") > 0 && animator.GetFloat("Walk") < 1)
                {

                    animator.SetFloat("Walk", animator.GetFloat("Walk") + 0.01f);

                }
                if (Input.GetAxis("Fire3") == 0 && animator.GetFloat("Walk") > 0)
                {

                    animator.SetFloat("Walk", animator.GetFloat("Walk") - 0.01f);

                }

            }
            else
            {
                if (animator.GetFloat("Walk") > 0)
                {
                    animator.SetFloat("Walk", animator.GetFloat("Walk") - 0.01f);

                }
                else
                {
                    animator.SetBool("Move", false);
                }


            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {

                animator.SetBool("Move", true);
                //animator.SetBool("Back", true);
                //transform.localRotation = Quaternion.Slerp();

                StartCoroutine(RotatePlayer(transform.localRotation, Quaternion.LookRotation(-Cam.transform.forward), 0.5f));

                //Vector3 rot = transform.localRotation.eulerAngles;
                //rot = new Vector3(rot.x, rot.y + 180, rot.z);
                //transform.localRotation = Quaternion.Euler(rot);

            }

            moveDir = Vector3.forward * v;

            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Speed;

        }

        if (jump > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
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

    public void JumpForward()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            animator.applyRootMotion = false;
            StartCoroutine(JumpMove(transform.position, transform.position + transform.forward, 0.3f));
            Invoke("RootMotion", 0.2f);

        }
    }

    public void RootMotion()
    {
        animator.applyRootMotion = true;

    }

    IEnumerator JumpMove(Vector3 start, Vector3 end, float Duration)
    {
        float t = 0f;
        while (t < Duration && (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump normal") || animator.GetCurrentAnimatorStateInfo(0).IsName("Jump combat")))
        {
            transform.position = Vector3.Lerp(new Vector3(start.x, transform.position.y, start.z), new Vector3(end.x, transform.position.y, end.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }

    }
}
