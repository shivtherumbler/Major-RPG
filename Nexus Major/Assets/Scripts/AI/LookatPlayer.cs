using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        Vector3 aim = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Clamp(transform.rotation.eulerAngles.z,0, 20));

        //Chest.LookAt(Player.transform.position);
        transform.LookAt(aim);
        transform.rotation = transform.rotation * Quaternion.Euler(Offset);
        
    }
}
