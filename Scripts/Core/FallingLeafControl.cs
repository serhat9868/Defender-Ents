using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLeafControl : MonoBehaviour, IRaycastable
{
    [SerializeField] float deaccelarationToTarget = 0.15f;
    [SerializeField] float speedAtStart = 10f;
    [SerializeField] float fallingSpeed;
    [SerializeField] int cashAmount = 25;
    GameObject target;
    MainPlayerController mainPlayerController;
    bool isPickingMode = false;
    bool isClicked = false;
    float speedToTarget;
 

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MoneyText");
        mainPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
        speedToTarget = speedAtStart * Time.deltaTime;
    }

    void Update()
    {
        if (transform.position == target.transform.position)
        {
            Destroy(gameObject);
            return;
        }

        if (isPickingMode)
        {
            if(speedToTarget > speedAtStart * 0.25f)
            {
                speedToTarget -= deaccelarationToTarget * Time.deltaTime;
            }
                    
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speedToTarget);
            return;
        }

        transform.Translate(Vector2.down*fallingSpeed * Time.deltaTime);
    }   


    private void GainMoney()
    {
        isPickingMode = true;
        mainPlayerController.EarnLeaf(cashAmount);
        isClicked = true;
    }

    public CursorType GetCursorType()
    {
        return CursorType.Combat;
    }

    public bool HandleRaycast(MainPlayerController callingController)
    {
        if (isClicked) return false;

        if (Input.GetMouseButtonDown(0))
        {
            GainMoney();
        }
        return true;
    }
}
