using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeHealth : MonoBehaviour
{
    public PlayerHealthManager manager;

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
        if (other.tag == "bullet")
        {
            if (manager.health >= 0)
            {
                manager.health--;
            }
        }
    }
}
