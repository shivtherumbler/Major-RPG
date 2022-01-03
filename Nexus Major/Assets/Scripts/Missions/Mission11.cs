using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Mission11 : MonoBehaviour
{
    public GameObject enemies;
    public GameObject finalboss;
    public GameObject finalPlayer;
    public GameObject companion;
    public MissionManager manager;
    public CinemachineVirtualCamera CurrentCam;
    public CinemachineVirtualCamera MissionCam;
    public CinemachineFreeLook EscapeCam;
    public GameObject missionpanel;
    public GameObject missioncompleted;
    public GameObject cutscenetrigger;
    public GameObject openworldenemies;
    public GameObject openworldcivilians;
    public GameObject campaigncompleted;
    public Text objective;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        finalPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        companion = GameObject.FindGameObjectWithTag("companion");
        manager.killcount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.killcount == enemies.transform.childCount)
        {
            finalboss.SetActive(true);
        }

        if(finalboss.GetComponentInChildren<AIHealthSystem>().health <= 0)
        {
            StartCoroutine(Wait());
        }

        objective.text = " This is the Final Battle! Fight back and take back your village from the Nexus Corp.";
    }

    public void StartMission()
    {

        enemies.SetActive(true);
        //finalboss.SetActive(true);
        finalPlayer.GetComponent<MissionWaypoint>().target = finalboss.transform;
        finalPlayer.GetComponent<MoveToTarget>().Targets = null;
        MissionSelect(MissionCam);
        companion.GetComponent<CompanionAI>().ongoingmission = true;
        StartCoroutine(EscapeMission(EscapeCam));
    }

    public void MissionComplete()
    {
        companion.GetComponent<CompanionAI>().missionno = 0;
        missioncompleted.SetActive(true);
        manager.xptext.text = "2500 XP earned!";
        cutscenetrigger.SetActive(true);
        Cursor.visible = true;
        audio.Play();
    }

    public void MissionSelect(CinemachineVirtualCamera NextCam)
    {
        CurrentCam.Priority = 0;
        NextCam.Priority = 21;
        CurrentCam = NextCam;
    }

    IEnumerator EscapeMission(CinemachineFreeLook NextCam)
    {
        yield return new WaitForSeconds(5f);
        CurrentCam.Priority = 0;
        MissionCam.Priority = 0;
        NextCam.Priority = 21;
        missionpanel.SetActive(false);
        //EscapeCam = NextCam;
    }

    public void collectxp()
    { 
        manager.xp = manager.xp + 2500;
        manager.killcount = 0;
        finalboss.SetActive(false);
        enemies.SetActive(false);
        finalPlayer.GetComponent<MissionWaypoint>().target = companion.transform;
        companion.GetComponent<CompanionAI>().ongoingmission = false;
        openworldcivilians.SetActive(true);
        openworldenemies.SetActive(false);
        manager.xptext.text = "";
        missioncompleted.SetActive(false);
        gameObject.SetActive(false);
        Cursor.visible = false;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        MissionComplete();
    }
}
