using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAbility : MonoBehaviour
{
    [SerializeField] GameObject spawningTarget;
    [SerializeField] GameObject AbilityButton;
  //  [SerializeField] Ability ability;
    [SerializeField] int price = 25;

    MainPlayerController mainPlayerController;
    bool isTaken = false;

    private void Awake()
    {
        mainPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenAbility);
    }

    private void OpenAbility()
    {
        if (isTaken) return;

        if (mainPlayerController.GetLeaf() < price)
        {
            //No enough money textCoroutine
            return;
        }
        
        mainPlayerController.SpendLeaf(price);
        Instantiate(AbilityButton, spawningTarget.transform);
        isTaken = true;
    }
}
