using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] Image cooldownOverlay;
    [SerializeField] Ability ability;

    GameObject player;
    CooldownStore cooldownStore;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cooldownStore = player.GetComponent<CooldownStore>();
    }

    void Update()
    {
        cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(ability);  
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On pointer " + gameObject.name);
        ability.UseAbility(player);
    }
}
