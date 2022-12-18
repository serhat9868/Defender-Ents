using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinControl : MonoBehaviour
{
    [SerializeField] float distanceToleranceToSpawn = 3f;
    [SerializeField] GameObject PowerfulPumpkinPrefab = null;
    [SerializeField] float timeToGrowing = 10f;
    Rigidbody2D rigidbody;
    PumpkinControl lowerPumpkin;
    PumpkinControl upperPumpkin;
    float defaultTimeToGrowing;
    Coroutine coroutine = null;
    bool isGrowth = false;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        defaultTimeToGrowing = timeToGrowing;
    }

    void Update()
    {
        if(PowerfulPumpkinPrefab != null)
        {
            PumpkinControlToGrow();
        }
    }

    private void PumpkinControlToGrow()
    {
        PumpkinControl[] pumpkins = FindObjectsOfType<PumpkinControl>();

        //PUMPKIN CONTROL
        if (pumpkins.Length > 0)
        {
            foreach (var pumpkin in pumpkins)
            {
                float distance = Vector2.Distance(gameObject.transform.position,pumpkin.transform.position);
                float distanceY = gameObject.transform.position.y - pumpkin.transform.position.y;

                if (distance < distanceToleranceToSpawn && distanceY < 0)
                {
                    lowerPumpkin = pumpkin;
                }
                if (distance < distanceToleranceToSpawn && distanceY > 0)
                {
                    upperPumpkin = pumpkin;
                }

                if (upperPumpkin && lowerPumpkin)
                {
                    timeToGrowing -= Time.deltaTime;
                    if (timeToGrowing <= 0 && coroutine == null)
                    {
                        coroutine = StartCoroutine(SpawnPowerfulPumpkin());
                    }
                }
                else
                {
                    timeToGrowing = defaultTimeToGrowing;
                }
            }
        }
    }

    private IEnumerator SpawnPowerfulPumpkin()
    {
        isGrowth = true;
        while (true)
        {
            upperPumpkin.gameObject.transform.position =
          Vector3.MoveTowards(upperPumpkin.gameObject.transform.position, transform.position, 0.1f);

            lowerPumpkin.gameObject.transform.position =
               Vector3.MoveTowards(lowerPumpkin.gameObject.transform.position, transform.position, 0.1f);

            if (transform.position == lowerPumpkin.transform.position && transform.position == upperPumpkin.transform.position)
            {
                Instantiate(PowerfulPumpkinPrefab, transform.position, Quaternion.identity);
                Destroy(upperPumpkin.gameObject);
                Destroy(lowerPumpkin.gameObject);
                Destroy(gameObject);
                break;
            }
            yield return null;
        }   
    }   
}
