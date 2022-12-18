using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectArea : MonoBehaviour
{
    float effectTime = 5f;

    void Update()
    {
        Debug.Log("windeffect is working");
        effectTime -= Time.deltaTime;

        if(effectTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().StartFreezeEnemyCoroutine(effectTime, 0.5f,true);
        }
    }   
}
