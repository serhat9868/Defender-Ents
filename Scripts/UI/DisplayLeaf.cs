using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayLeaf : MonoBehaviour
{
    MainPlayerController mainPlayerController;
    float leafAmount;
    private void Awake()
    {
        mainPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
        leafAmount = mainPlayerController.GetLeaf();
    }
    void Update()
    {      
        GetComponent<TextMeshProUGUI>().text =
            String.Format("{0:0}", leafAmount); 

        if (leafAmount != mainPlayerController.GetLeaf())
        {
            leafAmount += Time.deltaTime*15f;
            if(leafAmount >= mainPlayerController.GetLeaf())
            {
                leafAmount = mainPlayerController.GetLeaf();
            }
        }
    }
}
