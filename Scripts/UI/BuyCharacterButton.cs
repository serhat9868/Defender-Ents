using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyCharacterButton : MonoBehaviour
{

    [SerializeField] GameObject characterToBuy;
    [SerializeField] GameObject creatingEntButton;
    [SerializeField] Image entImage;
    [SerializeField] GameObject spawningTarget;
    [SerializeField] int price;
    [SerializeField] Image imageToShow;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] float cooldownTime = 6f;
    MainPlayerController mainPlayerController;

    float size;
    private void Start()
    {
        priceText.text = String.Format("LEAF : " +price);
       // size = textGameobject.fontSize;
        mainPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
        GetComponent<Button>().onClick.AddListener(BuyCharacter);
    }

    private void BuyCharacter()
    {
        GameObject instance = Instantiate(creatingEntButton, spawningTarget.transform);
        instance.GetComponent<CreateEntButton>().CharacterConfiguration(characterToBuy, price,entImage,cooldownTime);
    }

    //private IEnumerator TextCoroutine()
    //{
    //    textGameobject.color = Color.red;
    //    textGameobject.text = String.Format("No Enough Money! "+gameObject.name);      
    //    for(int i = 0; i < 4; i++)
    //    {
    //        textGameobject.fontSize *= 0.5f;
    //        yield return new WaitForSeconds(0.5f);
    //        textGameobject.fontSize *= 2f;
    //        yield return new WaitForSeconds(0.5f);
    //    }
       
    //    textGameobject.color = Color.white;
    //    yield return new WaitForSeconds(1f);
    //    textGameobject.text = "";
    //}

    void Update()
    {
    
    }
}
