using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public CinemachineVirtualCamera CurrentCam;
    public GameObject loadingimage;
    public AudioClip[] audioclip;
    public AudioSource audioSource;
    public GameObject slider;
    public void MenuButtons(CinemachineVirtualCamera NextCam)
    {
        CurrentCam.Priority = 0;
        NextCam.Priority = 1;
        CurrentCam = NextCam;
    }

    public void NewGame()
    {
        StartCoroutine(LoadYourAsyncScene("SampleScene"));
    }

    public void SurvivalMode()
    {
        StartCoroutine(LoadYourAsyncScene("Survival Mode"));
    }

    IEnumerator LoadYourAsyncScene(string SceneName)
    {
        loadingimage.SetActive(true);
        slider.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        while (asyncLoad.progress < 1)
        {
            slider.GetComponent<Slider>().value = asyncLoad.progress;
            yield return new WaitForEndOfFrame();
        }
    }

    public void Open(GameObject open)
    {
        open.SetActive(true);
    }
    public void Close(GameObject close)
    {
        close.SetActive(false);
    }    

    public void Quit()
    {
        Application.Quit();
    }

    public void OnMouseEnter()
    {
        audioSource.clip = audioclip[0];
        audioSource.Play();
    }

    public void OnMouseDown()
    {
        audioSource.clip = audioclip[1];
        audioSource.Play();
    }

    
}

