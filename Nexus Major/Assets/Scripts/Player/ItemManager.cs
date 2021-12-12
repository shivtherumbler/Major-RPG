using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public InventoryChannel inventorychannel;
    public InventorySystem.InventoryItem sword;
    public InventorySystem.InventoryItem magicpotion;
    public bool swordactive;
    public GameObject weapon;
    public MissionManager manager;

    // Start is called before the first frame update
    void Start()
    {
        swordactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(swordactive == false)
        {
            if(weapon.activeInHierarchy)
            {
                inventorychannel.RaiseLootItem(sword);
                swordactive = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health")
        {
            inventorychannel.RaiseLootItem(magicpotion);
            Destroy(other.gameObject, 0.1f);

        }
        if(other.gameObject.tag == "crystal")
        {

            gameObject.GetComponent<PlayerHealthManager>().mp += 2;
            Destroy(other.gameObject, 0.1f);
        }
        if(other.gameObject.tag == "plus")
        {
            gameObject.GetComponent<PlayerHealthManager>().health += 20;
            Destroy(other.gameObject, 0.1f);
        }
        if(other.gameObject.tag == "star")
        {
            manager.xp += 5;
            Destroy(other.gameObject, 0.1f);
        }
        if (other.gameObject.tag == "chest")
        {
            gameObject.GetComponent<PlayerHealthManager>().mp += 5;
            gameObject.GetComponent<PlayerHealthManager>().health += 50;
            manager.xp += 50;
            Destroy(other.gameObject, 0.1f);
        }
    }
}
