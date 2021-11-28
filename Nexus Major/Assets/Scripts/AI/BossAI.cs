using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform Player;
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

    void Start()
    {
        //mRenderer = GetComponent<MeshRenderer>();
        GetComponent<Animator>().SetFloat("Offset", Random.Range(0.0f, 1.0f));
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
                Invoke("Alert", 1f);
                gun.GetComponent<Weapons>().S = false;
                gun.GetComponent<Weapons>().enabled = false;


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
                gun.GetComponent<Weapons>().enabled = true;
                gun.GetComponent<Weapons>().S = true;
                //Invoke("GameOver", 2f);
            }
        }
        else
        {
            State = "Idle";
            //stateVis.color = IdleColor;
            stateVis.color = Color.green;
            agent.speed = 0;
            Speed = 0;
            anim.SetBool("attack", false);
            anim.SetBool("run", false);
            anim.SetBool("back", false);
            agent.SetDestination(transform.position);
            alert.SetActive(false);
            gun.GetComponent<Weapons>().S = false;
            gun.GetComponent<Weapons>().enabled = false;

        }
        if (State == "Running")
        {
            this.transform.Translate(0, 0, Time.deltaTime * Speed);

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
            Debug.Log(Chest);
            Chest.LookAt(Player.transform.position);
            Chest.rotation = Chest.rotation * Quaternion.Euler(Offset);
        }

    }

    public void GameOver()
    {

        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
    }
}
