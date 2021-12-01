using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutScene6 : MonoBehaviour
{
    public GameObject player;
    public PlayableDirector playableDirector;
    public GameObject target;
    public GameObject companion;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().youngPlayer;
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
            companion.GetComponent<CompanionAI>().start = true;
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

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player)
        {
            playableDirector.Play();

        }
    }
}
