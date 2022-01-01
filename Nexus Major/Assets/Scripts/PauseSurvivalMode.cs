using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSurvivalMode : MonoBehaviour
{
    public GameObject PausePanel;
    public static bool GameIsPaused = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
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
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.GetComponent<Player>().enabled = true;
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer.GetComponent<Player>().enabled = false;
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        //System.IO.File.Delete(Application.persistentDataPath + "/SaveFile.es3");
        SceneManager.LoadScene("Survival Mode");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        //ES3.DeleteFile();
        //System.IO.File.Delete(Application.persistentDataPath + "/SaveFile.es3");
        SceneManager.LoadScene("MainMenu");
    }
}
