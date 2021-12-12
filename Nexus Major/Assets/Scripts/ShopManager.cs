using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public MissionManager manager;
    public Text xp;
    public List<Button> buybuttons;
    public List<int> cost;
    public List<bool> itembought;
    public int itemno;
    public GameObject player;
    public GameObject bike;
    public List<GameObject> skills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.xp >= 20)
        {
            buybuttons[0].interactable = true;

            if (manager.xp >= 50)
            {
                buybuttons[1].interactable = true;
            }
        }
        else
        {
            buybuttons[0].interactable = false;
            buybuttons[1].interactable = false;

        }

        for (itemno = 3; itemno < cost.Count; itemno++)
        {
            if(manager.xp >= cost[itemno] && itembought[itemno] == false)
            {
                buybuttons[itemno].interactable = true;
            }

            else
            {
                buybuttons[itemno].interactable = false;
            }
        }

        xp.text = "XP: "+ manager.xp.ToString();
    }

    public void BuyApple()
    {
        manager.xp = manager.xp - 20;
    }

    public void BuyPotion()
    {
        manager.xp = manager.xp - 50;
    }

    public void BuyMythic()
    {
        manager.xp = manager.xp - 200;
        var alphacolor = skills[0].GetComponent<Image>().color;
        alphacolor.a = 1f;
        skills[0].GetComponent<Image>().color = alphacolor;
        itembought[3] = true;
        //buybuttons[3].interactable = false;
        buybuttons[3].GetComponentInChildren<Text>().text = "OWNED";
        player.GetComponent<PlayerHealthManager>().maxmp = 100;
        player.GetComponent<PlayerHealthManager>().mp = 100;
    }

    public void BuySword()
    {
        manager.xp = manager.xp - 300;
        player.GetComponent<Player>().upgradeno[0] = true;  
        itembought[4] = true;
        //buybuttons[4].interactable = false;
        buybuttons[4].GetComponentInChildren<Text>().text = "OWNED";
    }

    public void BuyFire()
    {
        manager.xp = manager.xp - 500;
        player.GetComponent<MoveToTarget>().upgrade[0] = true;
        var alphacolor = skills[1].GetComponent<Image>().color;
        alphacolor.a = 1f;
        skills[1].GetComponent<Image>().color = alphacolor;
        itembought[5] = true;
        //buybuttons[5].interactable = false;
        buybuttons[5].GetComponentInChildren<Text>().text = "OWNED";
    }

    public void BuyBlade()
    {
        manager.xp = manager.xp - 750;
        player.GetComponent<Player>().upgradeno[1] = true;
        itembought[6] = true;
        //buybuttons[6].interactable = false;
        buybuttons[6].GetComponentInChildren<Text>().text = "OWNED";
    }

    public void BuyRing()
    {
        manager.xp = manager.xp - 1000;
        player.GetComponent<PlayerHealthManager>().maxhealth = 1000;
        player.GetComponent<PlayerHealthManager>().health = 1000;
        var alphacolor = skills[2].GetComponent<Image>().color;
        alphacolor.a = 1f;
        skills[2].GetComponent<Image>().color = alphacolor;
        itembought[7] = true;
        //buybuttons[7].interactable = false;
        buybuttons[7].GetComponentInChildren<Text>().text = "OWNED";
    }

    public void BuyFinal()
    {
        manager.xp = manager.xp - 1200;
        player.GetComponent<MoveToTarget>().upgrade[1] = true;
        var alphacolor = skills[3].GetComponent<Image>().color;
        alphacolor.a = 1f;
        skills[3].GetComponent<Image>().color = alphacolor;
        itembought[8] = true;
        //buybuttons[8].interactable = false;
        buybuttons[8].GetComponentInChildren<Text>().text = "OWNED";
    }

    public void BuyBike()
    {
        manager.xp = manager.xp - 1500;
        bike.SetActive(true);
        itembought[9] = true;
        //buybuttons[9].interactable = false;
        buybuttons[9].GetComponentInChildren<Text>().text = "OWNED";
    }
}
