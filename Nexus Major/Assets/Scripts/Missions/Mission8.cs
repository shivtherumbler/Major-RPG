using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Mission8 : MonoBehaviour
{
    public GameObject enemies;
    public GameObject villagers;
    public GameObject finalPlayer;
    public GameObject companion;
    public MissionManager manager;
    public CinemachineVirtualCamera CurrentCam;
    public CinemachineVirtualCamera MissionCam;
    public CinemachineFreeLook EscapeCam;
    public GameObject missionpanel;
    public GameObject missioncompleted;
    public GameObject cutscenetrigger;
    public Text objective;

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
            MissionComplete();
        }
        objective.text = "Free the farmers from the Nexus guards.";
    }

    public void StartMission()
    {

        enemies.SetActive(true);
        villagers.SetActive(true);
        finalPlayer.GetComponent<MissionWaypoint>().target = villagers.transform;
        MissionSelect(MissionCam);
        companion.GetComponent<CompanionAI>().ongoingmission = true;
        StartCoroutine(EscapeMission(EscapeCam));
    }

    public void MissionComplete()
    {
        companion.GetComponent<CompanionAI>().missionno = 8;
        missioncompleted.SetActive(true);
        for (int i = 0; i < villagers.transform.childCount; i++)
        {
            villagers.transform.GetChild(i).GetComponent<AnimationOffset>().anim.SetBool("cheer", true);
        }
        manager.xptext.text = "500 XP earned!";
        cutscenetrigger.SetActive(true);
        Cursor.visible = true;
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
        manager.xp = manager.xp + 500;
        manager.killcount = 0;
        villagers.SetActive(false);
        enemies.SetActive(false);
        finalPlayer.GetComponent<MissionWaypoint>().target = companion.transform;
        companion.GetComponent<CompanionAI>().ongoingmission = false;
        manager.xptext.text = "";
        gameObject.SetActive(false);
        Cursor.visible = false;
    }
}
