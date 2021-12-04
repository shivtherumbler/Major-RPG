using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDestroyed : MonoBehaviour
{
    public GameObject cutscene10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 5)
        {
            cutscene10.SetActive(true);
        }
    }
}
