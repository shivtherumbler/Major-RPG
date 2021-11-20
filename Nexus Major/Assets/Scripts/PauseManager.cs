using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public GameObject targetpos;
    public GameObject[] inventorylist;
    public GameObject minimapcam;

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

        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.activeInHierarchy)
        {
            targetpos.transform.position = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.GetComponent<MissionWaypoint>().target.position;

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
        SceneManager.LoadScene("SampleScene");

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        //ES3.DeleteFile();
        //System.IO.File.Delete(Application.persistentDataPath + "/SaveFile.es3");
        SceneManager.LoadScene("MainMenu");
    }

    public void Map()
    {
        minimapcam.SetActive(true);
        mappanel.SetActive(true);
        inventorypanel.SetActive(false);
        skillsPanel.SetActive(false);
        settingsPanel.SetActive(false);

    }

    public void Inventory()
    {
        minimapcam.SetActive(false);
        inventorypanel.SetActive(true);
        mappanel.SetActive(false);
        skillsPanel.SetActive(false);
        settingsPanel.SetActive(false);

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
    }

    public void SettingsOpen()
    {
        settingsPanel.SetActive(true);
        skillsPanel.SetActive(false);
        mappanel.SetActive(false);
        minimapcam.SetActive(false);
        inventorypanel.SetActive(false);
    }

    public void ShopClose()
    {
        shoppanel.SetActive(false);
        Time.timeScale = 1;
    }

}
