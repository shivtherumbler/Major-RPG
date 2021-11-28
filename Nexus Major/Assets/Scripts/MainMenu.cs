using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public CinemachineVirtualCamera CurrentCam;
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

