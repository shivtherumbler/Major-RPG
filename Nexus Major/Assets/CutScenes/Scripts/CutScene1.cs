using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene1 : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(player.GetComponent<SelectPlayer>().youngPlayer.activeInHierarchy)
        {
            playableDirector.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (playableDirector == aDirector)
        {
            //Destroy(gameObject, 1f);
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}
