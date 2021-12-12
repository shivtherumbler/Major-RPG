using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission9Complete : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == parent.GetComponent<Mission9>().finalPlayer)
        {
            parent.GetComponent<Mission9>().MissionComplete();
        }
    }
}
