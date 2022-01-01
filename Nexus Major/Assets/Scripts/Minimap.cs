using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public GameObject playerselect;

    private void Update()
    {
        if(playerselect.GetComponent<SelectPlayer>().youngPlayer.activeInHierarchy)
        {
            player = playerselect.GetComponent<SelectPlayer>().youngPlayer.transform;
        }
        else
        {
            player = playerselect.GetComponent<SelectPlayer>().finalPlayer.transform;
        }
        
    }

    private void LateUpdate()
    {
        Vector3 newpos = player.position;
        newpos.y = transform.position.y;
        transform.position = newpos;
        
    }
}
