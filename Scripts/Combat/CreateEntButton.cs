using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateEntButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI noMoneyText;
    [SerializeField] GameObject crossSymbolPrefab;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Image prefabImage;
    [SerializeField] Image CooldownImage;

    GameObject crossSymbol = null;
    GameObject prefab;
    GameObject entToDrag = null;
    MainPlayerController mainPlayerController;
    BuildingSystem buildingSystem;

    Vector3 currentPosition;
    Vector3 offset;

    int price = 50;
    float timeSinceLastUse = Mathf.Infinity;
    float cooldownTime;
    bool draggingMode = false;
    private void Awake()
    {
        mainPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
        buildingSystem = GameObject.FindGameObjectWithTag("GridMap").GetComponent<BuildingSystem>();
    }
    private void Start()
    {
        noMoneyText.color = Color.red;
        priceText.text = price.ToString();
    }

    public void CharacterConfiguration(GameObject prefab, int price, Image entImage, float cooldownTime)
    {
        this.prefab = prefab;
        this.price = price;
        prefabImage.sprite = entImage.sprite;
        this.cooldownTime = cooldownTime;
    }

    private void Update()
    {
        timeSinceLastUse += Time.deltaTime;

        if (timeSinceLastUse < cooldownTime)
        {
            CooldownImage.fillAmount -= Time.deltaTime / cooldownTime;
            return;
        }
        if (entToDrag == null && crossSymbol != null)
        {
            Destroy(crossSymbol);
            crossSymbol = null;
        }

        if (mainPlayerController.GetLeaf() < price)
        {
            Debug.Log("money is not enough");
            GetComponent<Button>().interactable = false;
            return;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }

        if (entToDrag != null && draggingMode)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (crossSymbol != null)
                {
                    Destroy(crossSymbol);
                    crossSymbol = null;
                }

                if (!buildingSystem.IsInGrid(entToDrag.transform))
                {
                    Destroy(entToDrag);
                    CooldownImage.fillAmount = 0;
                    draggingMode = false;
                    return;
                }

                Vector3 gridPosition = BuildingSystem.current.SnapCoordinateToGrid(currentPosition);

                if (buildingSystem.GetTileDictionary().ContainsKey(gridPosition))
                {
                    Debug.Log("grid is not null");
                    Destroy(entToDrag);
                    CooldownImage.fillAmount = 0;
                    draggingMode = false;
                    return;
                }

                else
                {
                    Debug.Log("Object is dragged");                  
                    buildingSystem.AddToDictionary(gridPosition, entToDrag.name);
                    entToDrag.GetComponent<DraggingObject>().SetDraggingMode(false);
                    entToDrag.transform.position = gridPosition;
                    entToDrag = null;
                    mainPlayerController.SpendLeaf(price);
                    timeSinceLastUse = 0;
                    draggingMode = false;
                    return;
                }
            }
        }

        //WHILE MOUSE HELD DOWN
        if (Input.GetMouseButton(0)  && draggingMode)
        {
            if(entToDrag == null)
            {
                GameObject instance = Instantiate(prefab);
                entToDrag = instance;
                entToDrag.GetComponent<DraggingObject>().SetDraggingMode(true);
            }
    
            CooldownImage.fillAmount = 1;
            if (!buildingSystem.IsInGrid(entToDrag.transform))
            {
                if (crossSymbol == null)
                {
                    crossSymbol = Instantiate(crossSymbolPrefab, entToDrag.transform.position, Quaternion.identity);
                }
                crossSymbol.transform.position = entToDrag.transform.position;
            }

            //CROSS SYMBOL
            else if (crossSymbol != null && buildingSystem.IsInGrid(entToDrag.transform))
            {
                Destroy(crossSymbol);
                crossSymbol = null;
            }

            //DRAGGING ENT
            Vector2 pos = BuildingSystem.GetMouseWorldPosition() + offset;
            currentPosition = pos;
            entToDrag.transform.position = pos;
        }

        //TEXT COROUTINE
        else if (Input.GetMouseButton(0) && mainPlayerController.GetLeaf() < price)
        {
            StartCoroutine(TextCoroutine());
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (mainPlayerController.GetLeaf() < price) return;
        draggingMode = true;
    }

    private IEnumerator TextCoroutine()
    {
        noMoneyText.gameObject.SetActive(true);
        noMoneyText.text = String.Format("No Enough Money");
        yield return new WaitForSeconds(1.5f);
        noMoneyText.gameObject.SetActive(false);
    }
}
