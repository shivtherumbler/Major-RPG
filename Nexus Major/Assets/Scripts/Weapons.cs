using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Muzzle;
    private bool CanFire;
    public float FireRate = 0.1f;
    public bool S;

    private void Start()
    {
        CanFire = true;
    }
    public void Shoot()
    {
        if (CanFire == true)
        {
            CanFire = false;
            StartCoroutine(Fire());
        }

        /*if (S == true)
        {
            GameObject newBullet = (GameObject)Instantiate(Bullet, Muzzle.transform.position, transform.rotation);
            // newBullet.transform.LookAt(Pos);
            newBullet.GetComponent<Rigidbody>().AddForce(1000 * transform.forward);
        }*/
    }
    public void Reload()
    {
        S = false;
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    IEnumerator Fire()
    {
        GameObject newBullet = (GameObject)Instantiate(Bullet, Muzzle.transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(1000 * transform.forward);
        yield return new WaitForSeconds(FireRate);
        CanFire = true;
    }

}
