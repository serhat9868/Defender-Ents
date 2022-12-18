using RPG.Inventories;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEntControl : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float hitDistance = 1f;
    [SerializeField] float fightingRangeX = 20f;
    [SerializeField] float fightingRangeY = 2f;
    [SerializeField] float hitRate = 1f;
    [SerializeField] float maxEnemyPositionX = 32f;

    [Header("Fireball Tolerance")]
    [SerializeField] float xTolerance = 1f;
    [SerializeField] float yTolerance = 1f;

    float time = Mathf.Infinity;

    void Update()
    {
        if (GetComponent<DraggingObject>().IsDraggingMode()) return;
        time += Time.deltaTime;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        if (targets.Length > 0)
        {
            foreach (GameObject enemy in targets)
            {
                float distanceX = gameObject.transform.position.x - enemy.transform.position.x;
                float distanceY = gameObject.transform.position.y - enemy.transform.position.y;
                bool isInFightingRange = Mathf.Abs(distanceY) < fightingRangeY && Mathf.Abs(distanceX) < fightingRangeX;
                bool isEnemyInMap = enemy.transform.position.x < maxEnemyPositionX;
                if (isInFightingRange && isEnemyInMap)
                {
                    Attack();
                }         
            }
        }
    }

    private void Attack()
    {
        if (time > 1 / hitRate)
        {
            Vector2 position = new Vector2(transform.position.x + xTolerance, transform.position.y + yTolerance);
            GameObject instance = Instantiate(projectilePrefab, position, Quaternion.identity);
            instance.GetComponent<Projectile>().SetParent(gameObject);
            time = 0;
        }
    }
}
