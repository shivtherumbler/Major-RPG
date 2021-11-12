using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Animator animator;
    public GameObject Cam;
    public float DirectionDampTime = 0.25f;
    public bool ApplyGravity = true;
    //public bool isAttacking = false;
    public bool canRecieveInput;
    public bool inputRecieved;
    public static Player instance;
    public GameObject weapon;
    public GameObject slash;

    private CharacterController characterController;

    public float Speed = 2.0f;

    public float RotationSpeed = 240.0f;

    private Vector3 moveDir = Vector3.zero;

    /*private void Awake()
    {
        instance = this;
    }*/

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        Attack();
        instance = this;
    }


// Update is called once per frame
/*void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(vertical > 0)
        {
            velocity += Time.deltaTime;
            animator.SetFloat("Walk", velocity);

        }
        if(vertical == 0)
        {
            velocity -= Time.deltaTime;
            animator.SetFloat("Walk", velocity);
        }
        if(animator.GetFloat("Walk") < 1)
        {

        }

    }*/

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

            //animator.SetBool("Walk", move);
            /*if (Input.GetAxis("Fire3") == 0)
            {

                animator.SetFloat("Walk", velocity);

            }*/

            //animator.SetFloat("Direction", h, DirectionDampTime, Time.deltaTime);


            moveDir = Vector3.forward * v;

            moveDir = transform.TransformDirection(moveDir);
            moveDir *= Speed;

        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("Evade", true);
            animator.SetFloat("EvadeDir", 3f);
        }
        else if (h > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("Evade", true);
            animator.SetFloat("EvadeDir", 1f);
        }
        else if(h < 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("Evade", true);
            animator.SetFloat("EvadeDir",2f);
        }
        
        else if (v > 0 && Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("Evade", true);
            animator.SetFloat("EvadeDir", 3f);
        }
        else
        {
            animator.SetBool("Evade", false);

        }

        if(jump > 0)
        {
            animator.SetBool("Jump", true);   
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        if (animator.GetBool("Battle") == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                animator.SetTrigger("StrongAttack");
                
                weapon.SetActive(true);
                Invoke("WeaponOff", 5f);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetTrigger("RunAttack");
                
                weapon.SetActive(true);
                Invoke("WeaponOff", 5f);
            }

        }

        /*if(animator.GetBool("climb") == true)
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
        }*/

        //characterController.Move(moveDir * Time.deltaTime);

        /*if(Input.GetButtonDown("Fire1"))
        {
            JumpToEnemy();
        }*/
    }

    /*private void JumpToEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject bestEnemy = null;
        float bestEnemyScore = -1;
        foreach(GameObject enemy in enemies)
        {
            float enemyScore = CalculateEnemyViability(enemy.transform);
            if(enemyScore > bestEnemyScore)
            {
                bestEnemyScore = enemyScore;
                bestEnemy = enemy;
            }
        }
        if(bestEnemy != null)
        {
            DefineTrajectory(bestEnemy.transform);
        }
    }

    private float CalculateEnemyViability(Transform enemy)
    {
        float dot = Vector3.Dot(transform.forward, (enemy.position - transform.position).normalized);
        float distance = (enemy.position - transform.position).magnitude;
        return dot + 1 / distance;
    }
    private void DefineTrajectory(Transform target)
    {
        Vector3 s = target.position + target.forward * 0.5f - transform.position;
        Vector3 u = MotionEquationSolver.SolveForInitialVelocity(s, Physics.gravity, 0.5f);
        if(GetComponent<CharacterController>() != null)
        {
            GetComponent<CharacterController>().attachedRigidbody.velocity = u;
        }
        else if(GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = u;
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

    public void Attack()
    {
        /*if(Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
        }*/
        if (Input.GetButtonDown("Fire1"))
        {
            if (animator.GetBool("Battle") == false)
            {

                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    animator.SetTrigger("StrongAttack");
                    weapon.SetActive(true);
                    Invoke("WeaponOff", 5f);
                }
                else if(!Input.GetKey(KeyCode.LeftAlt))
                {
                    animator.SetTrigger("Attack");
                    weapon.SetActive(true);
                    Invoke("WeaponOff", 5f);
                }

            }

            if (canRecieveInput)
            {
                inputRecieved = true;
                canRecieveInput = false;
            }
            else
            {
                return;
            }
        }
        
    }

    public void InputManager()
    {
        if(!canRecieveInput)
        {
            canRecieveInput = true;
        }
        else
        {
            canRecieveInput = false;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ladder")
        {
            animator.SetBool("climb", true);
            StartCoroutine(RotatePlayer(transform.rotation, other.transform.rotation, 1f));
            StartCoroutine(LadderClimb(transform.position, other.transform.position, 1f));
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Battle")
        {
            animator.SetBool("Battle", true);
            weapon.SetActive(true);
            //gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<MoveToTarget>().enabled = true;

        }

        /*if(other.tag == "ladderbottom")
        {

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("offladder", true);
            }
            else
            {
                animator.SetBool("offladder", false);
            }
        }*/


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Battle")
        {
            animator.SetBool("Battle", false);
            weapon.SetActive(false);
            //gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<MoveToTarget>().enabled = false;

        }
        /*if (other.tag == "ladder")
        {
            animator.SetBool("climb", false);
        }*/
    }
    
    public void SlashOn()
    {
        slash.SetActive(true);
    }

    public void SlashOff()
    {
        slash.SetActive(false);
    }

    public void JumpForward()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            animator.applyRootMotion = false;
            StartCoroutine(JumpMove(transform.position, transform.position + transform.forward , 0.3f));
            Invoke("RootMotion", 0.2f);

        }
    }

    public void WeaponOff()
    {
        weapon.SetActive(false);
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

    /*IEnumerator LadderClimb(Vector3 start, Vector3 end, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(new Vector3(start.x, transform.position.y, start.z), new Vector3(end.x, transform.position.y, end.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }

    }*/
}




