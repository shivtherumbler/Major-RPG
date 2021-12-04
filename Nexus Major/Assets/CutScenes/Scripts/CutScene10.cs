using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene10 : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject target;
    public GameObject companion;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        playableDirector.Play();
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
            player.GetComponent<MissionWaypoint>().target = target.transform;
            companion.GetComponent<Roshi>().house = target;
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
