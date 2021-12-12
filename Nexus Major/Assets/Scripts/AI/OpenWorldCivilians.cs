using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldCivilians : MonoBehaviour
{
    public GameObject player;
    public GameObject companion;
    public bool[] active;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        companion = GameObject.FindGameObjectWithTag("companion");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 200)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                transform.GetChild(i).gameObject.SetActive(true);

            }
        }
    }
}
