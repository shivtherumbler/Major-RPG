using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0.5f , transform.localPosition.z);
        }
        
    }
}
