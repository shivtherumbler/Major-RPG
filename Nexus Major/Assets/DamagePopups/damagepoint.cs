using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damagepoint : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("character");
        }

        if(player.GetComponent<Player>().weaponno == 0)
        {
            this.GetComponent<TextMeshPro>().text = "-8";
        }
        else if(player.GetComponent<Player>().weaponno == 1)
        {
            this.GetComponent<TextMeshPro>().text = "-12";
        }
        else if(player.GetComponent<Player>().weaponno == 2)
        {
            this.GetComponent<TextMeshPro>().text = "-16";
        }
    }
}
