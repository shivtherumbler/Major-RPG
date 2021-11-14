using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Muzzle;
    public bool S;
    public void Shoot()
    {
        if (S == true)
        {
            GameObject newBullet = (GameObject)Instantiate(Bullet, Muzzle.transform.position, Bullet.transform.localRotation);
            // newBullet.transform.LookAt(Pos);
            newBullet.GetComponent<Rigidbody>().AddForce(1000 * transform.forward);
        }
    }
    public void Reload()
    {
        S = false;
    }

    void Update()
    {
        Shoot();
    }

}
