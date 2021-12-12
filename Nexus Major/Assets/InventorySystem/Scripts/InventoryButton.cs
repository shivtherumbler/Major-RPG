using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public GameObject itemname;
    public GameObject finalPlayer;
    public InventoryChannel inventorychannel;
    public List<InventorySystem.InventoryItem> items;
    public GameObject amountleft;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(UseItem);
    }

    // Update is called once per frame
    void Update()
    {
        if(finalPlayer == null)
        {
            finalPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<SelectPlayer>().finalPlayer;
        }     

        if(amountleft.GetComponent<Text>().text == "0")
        {
            gameObject.GetComponent<InventorySlotUIController>().DestroySlot();
        }
    }

    public void UseItem()
    {
        if(itemname.GetComponent<Text>().text == "Apple")
        {
            finalPlayer.GetComponent<PlayerHealthManager>().health += 50;
            inventorychannel.UseLootItem(items[0]);
        }
        else if (itemname.GetComponent<Text>().text == "Magic Potion")
        {
            inventorychannel.UseLootItem(items[1]);
            //finalPlayer.GetComponent<PlayerHealthManager>().health += 50;
        }

    }
}
