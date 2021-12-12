using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission5Complete : MonoBehaviour
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
        if (other.gameObject == parent.GetComponent<Mission5>().finalPlayer)
        {
            parent.GetComponent<Mission5>().MissionComplete();
        }
    }
}
