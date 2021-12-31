using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    public static bool GameIsPaused = false;
    public GameObject minimap;
    public GameObject mappanel;
    public GameObject inventorypanel;
    public GameObject skillsPanel;
    public GameObject settingsPanel;
    public GameObject shoppanel;
    public GameObject graphicspanel;
    public GameObject controlPanel;
    public GameObject creditsPanel;
    public GameObject targetpos;
    public GameObject[] inventorylist;
    public GameObject minimapcam;
    public CinemachineFreeLook[] CurrentCam;

    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();

            }
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer.activeInHierarchy)
        {
            targetpos.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer.GetComponent<MissionWaypoint>().target.position;
            YoungSelect(CurrentCam[1]);

        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.activeInHierarchy)
        {
            targetpos.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.GetComponent<MissionWaypoint>().target.position;
            FinalSelect(CurrentCam[0]);
        }

        if(shoppanel.activeInHierarchy)
        {
            PausePanel.SetActive(false);
        }
        else if(PausePanel.activeInHierarchy)
        {
            shoppanel.SetActive(false);
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.GetComponent<Player>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer.GetComponent<YoungPlayer>().enabled = true;
        minimapcam.SetActive(false);
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        minimap.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.GetComponent<Player>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer.GetComponent<YoungPlayer>().enabled = true;
        minimapcam.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        minimap.SetActive(false);
        //System.IO.File.Delete(Application.persistentDataPath + "/SaveFile.es3");
        StartCoroutine(LoadYourAsyncScene("SampleScene"));

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        //ES3.DeleteFile();
        //System.IO.File.Delete(Application.persistentDataPath + "/SaveFile.es3");
        StartCoroutine(LoadYourAsyncScene("MainMenu"));
    }

    public void Map()
    {
        minimapcam.SetActive(true);
        mappanel.SetActive(true);
        inventorypanel.SetActive(false);
        skillsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        graphicspanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void Inventory()
    {
        minimapcam.SetActive(false);
        inventorypanel.SetActive(true);
        mappanel.SetActive(false);
        skillsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        graphicspanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void CheckItems()
    {
        inventorylist[0].SetActive(true);
    }

    public void Skills()
    {
        skillsPanel.SetActive(true);
        mappanel.SetActive(false);
        minimapcam.SetActive(false);
        inventorypanel.SetActive(false);
        settingsPanel.SetActive(false);
        graphicspanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void SettingsOpen()
    {
        settingsPanel.SetActive(true);
        skillsPanel.SetActive(false);
        mappanel.SetActive(false);
        minimapcam.SetActive(false);
        inventorypanel.SetActive(false);
        graphicspanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ControlsOpen()
    {
        controlPanel.SetActive(true);
        settingsPanel.SetActive(false);
        skillsPanel.SetActive(false);
        mappanel.SetActive(false);
        minimapcam.SetActive(false);
        inventorypanel.SetActive(false);
        graphicspanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ShopClose()
    {
        shoppanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void YoungSelect(CinemachineFreeLook NextCam)
    {
        CurrentCam[0].Priority = 0;
        NextCam.Priority = 20;
        //CurrentCam = NextCam;
    }

    public void FinalSelect(CinemachineFreeLook NextCam)
    {
        CurrentCam[1].Priority = 0;
        NextCam.Priority = 20;
        //EscapeCam = NextCam;
    }

    public void GraphicsTabOpen()
    {
        graphicspanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void GraphicsTabClose()
    {
        graphicspanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CreditsTabOpen()
    {
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void CreditsTabClose()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Open(GameObject open)
    {
        open.SetActive(true);
    }

    IEnumerator LoadYourAsyncScene(string SceneName)
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        while (asyncLoad.progress < 1)
        {
            //slider.GetComponent<Slider>().value = asyncLoad.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
