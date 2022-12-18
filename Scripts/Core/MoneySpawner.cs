using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab1;
    [SerializeField] float startingPointY;
    [SerializeField] float spawningRate;
    [SerializeField] float spawningTolerance = 2f;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    float currentSpawnTime;
    float time = Mathf.Infinity;
    bool isTimeRolled = false;
    void Update()   
    {
        time += Time.deltaTime;
        Spawn();
        RandomTime();
    }

    private void RandomTime()
    {
        if(time > spawningRate - spawningTolerance && !isTimeRolled)
        {
            currentSpawnTime = UnityEngine.Random.Range(spawningRate - spawningTolerance, spawningRate + spawningTolerance);
            isTimeRolled = true;
        }
    }

    void Spawn()
    {
        if(time > currentSpawnTime && isTimeRolled)
        {
            float startingPointX = UnityEngine.Random.Range(minX, maxX);
            Vector2 spawningPosition = new Vector2(startingPointX, startingPointY);
            Instantiate(prefab1, spawningPosition, Quaternion.identity);
            time = 0;
            isTimeRolled = false;
        }
    }
}
