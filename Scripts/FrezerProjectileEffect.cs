using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrezerProjectileEffect : MonoBehaviour
{
    [SerializeField] float freezingTime;

    [Range(0,1)]
    [SerializeField] float slowRange;

    float time;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<EnemyController>() != null)
        {
            collision.gameObject.GetComponent<EnemyController>().StartFreezeEnemyCoroutine(freezingTime, slowRange,false);
        }
    }

    public float GetFreezingTime()
    {
        return freezingTime;
    }
}
