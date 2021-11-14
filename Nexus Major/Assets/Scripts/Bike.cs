using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bike : MonoBehaviour
{
    
        private BikeControl DirtBike;
        public GameObject PlayerOnBike;
        public CinemachineFreeLook PlayerCamera;
        private GameObject Player;
        private bool OnBike;
        private float IKWeight;
        void Start()
        {
            OnBike = false;
            DirtBike = GetComponent<BikeControl>();
        }
        void Update()
        {
            if (OnBike == true && Input.GetKeyDown(KeyCode.G))
            {
                GetOffBike();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Player>())
            {
                //Debug.Log("BIKE");
                if (Input.GetKey(KeyCode.F) && OnBike == false)
                {
                    OnBike = true;

                    Player = other.gameObject;
                    //Player.GetComponent<Player>().minimapcam.transform.parent = gameObject.transform;
                    //Player.GetComponent<PlayerAim>().LookAim.weight = 0;
                    //IKWeight = Player.GetComponent<PlayerAim>().LookAim.weight;
                    Player.SetActive(false);
                    PlayerOnBike.SetActive(true);
                    PlayerCamera.LookAt = transform;
                    PlayerCamera.Follow = transform;
                    //other.gameObject.transform.parent = DirtBike.transform;
                    DirtBike.activeControl = true;
                }
            }
        }
        private void GetOffBike()
        {
            DirtBike.activeControl = false;
            OnBike = false;
            PlayerOnBike.SetActive(false);
            //Player.transform.parent = null;
            PlayerCamera.LookAt = Player.transform;
            PlayerCamera.Follow = Player.transform;
            Player.transform.position = DirtBike.transform.position - DirtBike.transform.right;
            Player.SetActive(true);
        //Player.GetComponent<Player>().minimapcam.transform.parent = Player.transform;
        //Player.GetComponent<PlayerAim>().LookAim.weight = IKWeight;
    }
}

