using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    public int popupIndex;
    public float waitTime = 2f;
    public GameObject player;
    public bool ButtonSelected;
    public Button mapButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < popUps.Length; i++)
        {
            if(i == popupIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popupIndex == 0)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == mapButton)
            {
                popupIndex++;

            }
        }
        else if(popupIndex == 1)
        {

        }
            
    }
}

