using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveAI : MonoBehaviour
{
    public Transform Player;
    public GameObject finalPlayer;
    public GameObject alert;
    public SpriteRenderer stateVis;
    public float visDistance;
    public float visAngle;
    public string State = "Idle";
    public float rotationSpeed;
    public float shootDist;
    //private MeshRenderer mRenderer;
    public Color RunningColor;
    public Color ShootingColor;
    public Color IdleColor;
    public float Speed;
    public NavMeshAgent agent;
    public Animator anim;
    public GameObject gun;
    public Vector3 Offset;
    public GameObject gameoverPanel;
    public bool boss;
    public int waittime;
    public float distance;
    public WaveSpawnner waveSpawnner;

    void Start()
    {
        //mRenderer = GetComponent<MeshRenderer>();
        GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
        finalPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        Player = finalPlayer.transform;
        Player.GetComponent<MoveToTarget>().Targets.Add(transform);
        waveSpawnner = GameObject.FindGameObjectWithTag("wavemanager").GetComponent<WaveSpawnner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.position);
        agent.SetDestination(Player.position - gameObject.transform.forward);
        Vector3 direction = Player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (direction.magnitude < visDistance && angle < visAngle)
        {
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            if (direction.magnitude > shootDist)
            {
                alert.SetActive(true);
                State = "Running";
                //stateVis.color = RunningColor;
                stateVis.color = Color.yellow;
                agent.speed = 0;
                Speed = 1;
                anim.SetBool("run", true);
                anim.SetBool("attack", false);
                anim.SetBool("back", false);
                Invoke("OffAlert", 1f);
                gun.GetComponent<Weapons>().S = false;
                gun.GetComponent<Weapons>().enabled = false;


                finalPlayer.GetComponent<Player>().animator.SetBool("Battle", false);
                //finalPlayer.GetComponent<Player>().weapon.SetActive(false);
                //finalPlayer.GetComponent<MoveToTarget>().enabled = false;
                //finalPlayer.GetComponent<MoveToTarget>().Targets = new Transform[transform.childCount];

                //for (int i = 0; i < transform.childCount; i++)
                //{

                //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = false;

                //gameObject.GetComponent<LineOfSight>().anim.SetBool("run", false);
                //gameObject.GetComponent<LineOfSight>().anim.SetBool("walk", true);
                //gameObject.GetComponent<LineOfSight>().anim.SetBool("attack", false);
                //gameObject.GetComponent<LineOfSight>().anim.SetBool("back", false);
                //gameObject.GetComponent<LineOfSight>().alert.SetActive(false);
                //gameObject.GetComponent<LineOfSight>().enabled = false;
                gameObject.GetComponent<WaveAI>().gun.GetComponent<Weapons>().enabled = false;
                //gameObject.GetComponent<CrowdBot>().enabled = true;

                //}
                //finalPlayer.GetComponent<MoveToTarget>().Targets = null;
            }
            else
            {

                State = "Shooting";
                //stateVis.color = ShootingColor;
                stateVis.color = Color.red;
                agent.speed = 0;
                Speed = 0;
                anim.SetBool("attack", true);
                anim.SetBool("run", false);
                anim.SetBool("back", false);
                alert.SetActive(false);
                if (boss == false)
                {
                    gun.transform.localPosition = new Vector3(-0.6932409f, -0.1737845f, -0.05664165f);
                    gun.transform.localRotation = Quaternion.Euler(-103.221f, 280.21f, -190.331f);
                }
                gun.GetComponent<Weapons>().enabled = true;
                gun.GetComponent<Weapons>().S = true;

                finalPlayer.GetComponent<Player>().animator.SetBool("Battle", true);
                finalPlayer.GetComponent<Player>().weapon[finalPlayer.GetComponent<Player>().weaponno].SetActive(true);
                //finalPlayer.GetComponent<MoveToTarget>().enabled = true;
                //finalPlayer.GetComponent<MoveToTarget>().Targets = new Transform[transform.childCount];

                //for (int i = 0; i < transform.childCount; i++)
                //{
                //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = true;       
                //} 

            }
        }
        else
        {
            alert.SetActive(true);
            State = "Running";
            //stateVis.color = RunningColor;
            stateVis.color = Color.yellow;
            agent.speed = 0;
            Speed = 1;
            anim.SetBool("run", true);
            anim.SetBool("attack", false);
            anim.SetBool("back", false);
            Invoke("OffAlert", 1f);
            gun.GetComponent<Weapons>().S = false;
            gun.GetComponent<Weapons>().enabled = false;


            finalPlayer.GetComponent<Player>().animator.SetBool("Battle", false);
            //finalPlayer.GetComponent<Player>().weapon.SetActive(false);
            //finalPlayer.GetComponent<MoveToTarget>().enabled = false;
            //finalPlayer.GetComponent<MoveToTarget>().Targets = new Transform[transform.childCount];

            //for (int i = 0; i < transform.childCount; i++)
            //{

            //other.GetComponent<MoveToTarget>().Targets[i].GetComponent<AINavMesh>().enabled = false;

            //gameObject.GetComponent<LineOfSight>().anim.SetBool("run", false);
            //gameObject.GetComponent<LineOfSight>().anim.SetBool("walk", true);
            //gameObject.GetComponent<LineOfSight>().anim.SetBool("attack", false);
            //gameObject.GetComponent<LineOfSight>().anim.SetBool("back", false);
            //gameObject.GetComponent<LineOfSight>().alert.SetActive(false);
            //gameObject.GetComponent<LineOfSight>().enabled = false;
            gameObject.GetComponent<WaveAI>().gun.GetComponent<Weapons>().enabled = false;
            //gameObject.GetComponent<CrowdBot>().enabled = true;

            //}
            //finalPlayer.GetComponent<MoveToTarget>().Targets = null;
        }
        if (State == "Running")
        {
            this.transform.Translate(0, 0, Time.deltaTime * Speed);

        }

        distance = Vector3.Distance(transform.position, Player.position);

        if (distance < 5)
        {
            waittime = 6;
            //StartCoroutine(GoBack());
            Invoke("BackOff", waittime);
        }
        else
        {
            waittime = 0;
            //StopCoroutine(GoBack());
            CancelInvoke();
            anim.SetBool("back", false);

        }

    }

    public void OffAlert()
    {
        alert.SetActive(false);
    }

    private void LateUpdate()
    {
        if (gameObject.GetComponent<Weapons>().S == true)
        {
            Transform Chest = anim.GetBoneTransform(HumanBodyBones.Spine);
            //Debug.Log(Chest);
            Chest.LookAt(Player.transform.position);
            Chest.rotation = Chest.rotation * Quaternion.Euler(Offset);
        }

    }

    public void GameOver()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
    }

    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(waittime);
        anim.SetBool("back", true);
    }

    public void BackOff()
    {
        anim.SetBool("back", true);

    }

}
