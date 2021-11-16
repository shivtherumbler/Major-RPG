using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public InventoryChannel inventorychannel;
    public InventorySystem.InventoryItem sword;
    public InventorySystem.InventoryItem healthpotion;
    public bool swordactive;
    public GameObject weapon;

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
            inventorychannel.RaiseLootItem(healthpotion);
            Destroy(other.gameObject, 0.1f);

        }
    }
}
