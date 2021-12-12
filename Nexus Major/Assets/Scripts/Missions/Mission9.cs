using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Mission9 : MonoBehaviour
{
    public GameObject enemies;
    public GameObject cage;
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

    }

    public void StartMission()
    {
        enemies.SetActive(true);
        cage.SetActive(true);
        finalPlayer.GetComponent<MissionWaypoint>().target = cage.transform;
        MissionSelect(MissionCam);
        companion.GetComponent<CompanionAI>().ongoingmission = true;
        StartCoroutine(EscapeMission(EscapeCam));
    }

    public void MissionComplete()
    {
        companion.GetComponent<CompanionAI>().missionno = 9;
        cage.GetComponent<Animator>().enabled = true;
        missioncompleted.SetActive(true);
        manager.xptext.text = "1500 XP earned!";
        cutscenetrigger.SetActive(true);
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
        missioncompleted.SetActive(false);
        manager.xp = manager.xp + 1500;
        manager.killcount = 0;
        finalPlayer.GetComponent<MissionWaypoint>().target = companion.transform;
        companion.GetComponent<CompanionAI>().ongoingmission = false;
        //shop.SetActive(false);
        enemies.SetActive(false);
        openworldcivilians.SetActive(true);
        openworldenemies.SetActive(false);
        //civilians.SetActive(true);
        manager.xptext.text = "";
        gameObject.SetActive(false);
    }
}
