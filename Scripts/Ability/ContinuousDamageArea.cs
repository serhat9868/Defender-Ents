using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContinuousDamageArea : MonoBehaviour
{
    Dictionary<GameObject, bool> enemyToHitDictionary =new Dictionary<GameObject, bool>();
    [SerializeField] float effectTime;
    [SerializeField] float damage = 25f;
    [SerializeField] float hitRate;
    float refreshingTime = 1.4f;
    float timeToHitRate;
    void Update()
    {
        timeToHitRate += Time.deltaTime;
        effectTime -= Time.deltaTime;

        if (effectTime < 0)
        {
            Destroy(gameObject);
        }

        //DAMAGE CONFIG
        if (timeToHitRate > hitRate + 0.05f)
        {
            foreach (var enemy in enemyToHitDictionary.Keys.ToList())
            {
                enemyToHitDictionary[enemy] = false;
            }
            timeToHitRate = hitRate;
            return;
        }

        foreach (var enemy in enemyToHitDictionary.Keys.ToList())
        {
            if (timeToHitRate >= hitRate && enemyToHitDictionary[enemy] == false && enemy !=null)
            {
                enemy.GetComponent<Health>().TakeDamage(GameObject.FindGameObjectWithTag("Player"), damage);
                enemyToHitDictionary[enemy] = true;
                timeToHitRate = 0;
            }
        }     
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!enemyToHitDictionary.ContainsKey(collision.gameObject) && collision.gameObject.CompareTag("Enemy"))
        {
            enemyToHitDictionary.Add(collision.gameObject, false);
        }        
    }
}


