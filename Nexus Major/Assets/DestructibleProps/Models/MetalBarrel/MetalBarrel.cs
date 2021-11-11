﻿using UnityEngine;
using System.Collections;

public class MetalBarrel : MonoBehaviour {
	public GameObject[] Chunks; //parts of the broken object.
	[Range(0.0F, 30.0F)]
	public float BurningTime = 3;
	[Range(1,100)]
	public int Health = 100;
	public float ExplosionForce = 200; //force added to every chunk of the broken object.
	public float ChunksRotation = 20; //rotation force added to every chunk when it explodes.
	public bool BreakByClick = true;
	public bool DestroyAfterTime = true; //if true, then chunks will be destroyed after time.
	public float time = 5; //time before chunks will be destroyed from the scene.
	public GameObject ExpLight;
	public GameObject FireFX;
	public bool AutoDestroy = true; //if true, then object will be automatically break after after "AutoDestTime" since game start.
	public float AutoDestTime = 2; //Auto destruction time (counts from game start).

	void Start () {
		if(AutoDestroy){
			Invoke("Crushing", AutoDestTime);
		}
		FireFX.SetActive(false);
		if(GetComponent<AudioSource>()){
			GetComponent<AudioSource>().pitch = Random.Range (1, 1.3f);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.GetComponent<BulletsDamage>()){
		Health -= other.gameObject.GetComponent<BulletsDamage>().Damage;
			if(Health <= 0 && !FireFX.activeInHierarchy){
				FireFX.SetActive(true);
				Invoke("Crushing", BurningTime);
			}
		}
		else if(other.gameObject.tag == "BarrelChunk" && !FireFX.activeInHierarchy){
			Health = 0;
			FireFX.SetActive(true);
			Invoke("Crushing", BurningTime);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "sword")
		{
			Crushing();
		}
	}

    void FixedUpdate(){
		if(ExpLight && ExpLight.GetComponent<Light>().intensity >0 && !GetComponent<Renderer>().enabled){
			ExpLight.GetComponent<Light>().intensity -= 0.3f;
		}
	}

	void OnMouseDown(){
		if(BreakByClick){
			Health = 0;
			FireFX.SetActive(true);
			Invoke("Crushing", BurningTime);
			BreakByClick = false;
		}
		}

	void Crushing(){
		FireFX.SetActive(false);
		GetComponent<Renderer>().enabled = false;
		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		foreach(GameObject chunk in Chunks){
			chunk.SetActive(true);
			chunk.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * -ExplosionForce);
			chunk.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * -ChunksRotation*Random.Range(-5f, 5f));
			chunk.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * -ChunksRotation*Random.Range(-5f, 5f));
		}
		if(DestroyAfterTime){
			Invoke("DestructObject", time);
		}
	}

	void DestructObject(){
		Destroy(gameObject);
	}

}
