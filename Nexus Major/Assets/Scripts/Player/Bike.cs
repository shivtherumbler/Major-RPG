using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bike : MonoBehaviour
{
    
        private BikeControl DirtBike;
        public GameObject PlayerOnBike;
        public CinemachineFreeLook[] PlayerCamera;
        private GameObject Player;
        private bool OnBike;
        private float IKWeight;
        public GameObject sound;

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
            if (other.gameObject.tag == "character" && other.gameObject.GetComponent<Player>())
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
                for(int i = 0; i< PlayerCamera.Length; i++)
                {
                    PlayerCamera[i].LookAt = transform;
                    PlayerCamera[i].Follow = transform;
                }
                    
                    other.gameObject.transform.parent = DirtBike.transform;
                    DirtBike.activeControl = true;
                    sound.SetActive(true);
                }
            }
        }
        private void GetOffBike()
        {
            DirtBike.activeControl = false;
            OnBike = false;
            PlayerOnBike.SetActive(false);
        //Player.transform.parent = null;
        for (int i = 0; i < PlayerCamera.Length; i++)
        {
            PlayerCamera[i].LookAt = Player.transform;
            PlayerCamera[i].Follow = Player.transform;
        }
        Player.transform.position = DirtBike.transform.position - DirtBike.transform.right;
            Player.SetActive(true);
            sound.SetActive(false);
        //Player.GetComponent<Player>().minimapcam.transform.parent = Player.transform;
        //Player.GetComponent<PlayerAim>().LookAim.weight = IKWeight;
    }
}

