using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealthManager : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public bool camangle;
    public Animator anim;
    public GameObject gameoverPanel;
    public List<CinemachineFreeLook> cinemachines;
    public CinemachineVirtualCamera CurrentCam;
    public CinemachineFreeLook EscapeCam;
    public int CurrentCamera;
    public int PreviousCamera;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
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
