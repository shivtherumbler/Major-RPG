using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health;
    public int mp;
    public int maxmp;
    public int maxhealth;
    public bool camangle;
    public Animator anim;
    public GameObject gameoverPanel;
    public List<CinemachineFreeLook> cinemachines;
    public CinemachineVirtualCamera CurrentCam;
    public CinemachineFreeLook EscapeCam;
    public int CurrentCamera;
    public int PreviousCamera;
    public GameObject healthbar;
    public Slider[] hpmp;
    public Text[] hpmptext;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        healthbar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            anim.SetBool("Death", true);
            MissionSelect(CurrentCam);
            Invoke("GameOver", 4f);
        }

        if(health > maxhealth)
        {
            health = maxhealth;
        }
        if(mp > maxmp)
        {
            mp = maxmp;
        }
        /*else
        {
            if (camangle == true)
            {
                MissionSelect(CurrentCam);
            }
            else
            {
                EscapeMission(EscapeCam);
            }
        }*/
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //camangle = !camangle;
            PreviousCamera = CurrentCamera;
            cinemachines[CurrentCamera].Priority = 0;
            CurrentCamera = (CurrentCamera + 1) % cinemachines.Count;
            cinemachines[CurrentCamera].Priority = 21;
        }

        hpmp[0].maxValue = maxhealth;
        hpmp[1].maxValue = maxmp;
        hpmp[0].value = health;
        hpmp[1].value = mp;
        hpmptext[0].text = health.ToString();
        hpmptext[1].text = mp.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            if(health >= 0)
            {
                health--;
            } 
        }    
    }

    public void MissionSelect(CinemachineVirtualCamera NextCam)
    {
        EscapeCam.Priority = 0;
        NextCam.Priority = 21;
        //CurrentCam = NextCam;
    }

    public void GameOver()
    {

        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
    }

    public void EscapeMission(CinemachineFreeLook NextCam)
    {
        CurrentCam.Priority = 0;
        NextCam.Priority = 21;
        //EscapeCam = NextCam;
    }
}
