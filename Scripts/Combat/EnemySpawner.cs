using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Vector3 SpawnPosition;
    float yLocation;

    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING,
        FINISH
    }

    [System.Serializable]
    public class Wawe
    {
        public string waweName;
        public Enemies[] enemies;
        public float spawnRate;
        public float waweCountdown;
    }

    [System.Serializable]
    public class Enemies
    {
        public CombatTarget enemyType;
        public int enemyAmount;
    }

    [Header("Wawe Settings")]
    public Wawe[] wawes;
    public string[] waweText;
    private int nextWawe = 0;

    public SpawnState state = SpawnState.COUNTING;

    [Header("Transform")]
    public float startingPointAtX;

    [Header("Text")]
    public TextMeshProUGUI textToEdit;
    public float textTime = 4f;

    private float maxY;
    private float minY;
    private float searchCountdown = 1f;
    private bool isTextCoroutineStartable = true;

    private void Start()
    {
       GameObject grid = GameObject.FindGameObjectWithTag("GridMap");
       maxY = grid.GetComponent<BuildingSystem>().GetMaxY();
       minY = grid.GetComponent<BuildingSystem>().GetMinY();
    }
    void Update()
    {
      //  Debug.Log("Wawe Countdown : " + wawes[nextWawe].waweCountdown);

        if (state == SpawnState.FINISH)
        {
            StartCoroutine(FinishSpawner());
            return;
        }

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                StartCoroutine(WaweCompleted());
            }
            else
            {
                return;
            }
        }

        if (wawes[nextWawe].waweCountdown < 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWawe(wawes[nextWawe]));
                isTextCoroutineStartable = true;
            }
        }
        else
        {
            wawes[nextWawe].waweCountdown -= Time.deltaTime;
            if (isTextCoroutineStartable)
            {
                StartCoroutine(ShowWaweText(nextWawe));
            }

            //    textToEdit.text = String.Format("Time To New Wawe Is {0:0.00} Seconds !", waweCountdown);
            //    textToEdit.gameObject.SetActive(true);

        }
    }

    private IEnumerator FinishSpawner()
    {
        textToEdit.color = Color.green;
        textToEdit.text = "All Wawes are fully completed!";
        yield return new WaitForSeconds(2f);
        textToEdit.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator ShowWaweText(int nextWawe)
    {
        isTextCoroutineStartable = false;
        textToEdit.text = string.Format("{0}", waweText[nextWawe]);
        textToEdit.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        textToEdit.text = string.Format("{0} WAWE IS COMING", wawes[nextWawe].waweName);
        yield return new WaitForSeconds(2);
        textToEdit.gameObject.SetActive(false);
    }

    IEnumerator SpawnWawe(Wawe currentWawe)
    {
      //  Debug.Log("Spawning Wawe : " + currentWawe.waweName);
        state = SpawnState.SPAWNING;

        int totalEnemy = 0;

        foreach (Enemies enemy in currentWawe.enemies)
        {
            totalEnemy += enemy.enemyAmount;
        }

        for (int i = 0; i < totalEnemy; i++)
        {
            int randomEnemy = UnityEngine.Random.Range(0, currentWawe.enemies.Length);
            Enemies enemy = currentWawe.enemies[randomEnemy];
            if (enemy.enemyAmount == 0)
            {
                i--;
                continue;
            }
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(currentWawe.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Enemies enemy)
    {
        yLocation = UnityEngine.Random.Range(minY, maxY);
        SpawnPosition = new Vector3(startingPointAtX, yLocation);
        Vector3 newPos = BuildingSystem.current.SnapCoordinateToGrid(SpawnPosition);
        Instantiate(enemy.enemyType, newPos, transform.rotation);
        enemy.enemyAmount--;
    }

    IEnumerator WaweCompleted()
    {
        Debug.Log("Wawe Completed ! ");

        state = SpawnState.COUNTING;

        if (nextWawe + 1 > wawes.Length - 1)
        {
            state = SpawnState.FINISH;
            Debug.Log("All Wawes Complete :) ");

        }
        else
        {
            nextWawe++;
        }

        yield return null;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

}
