using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Control")]
    [SerializeField] Vector2 velocity;
    [SerializeField] float lifetime = 3.5f;
    [SerializeField] float fireballScale = 1f;
    [SerializeField] float hitToleranceY = 3f;
    [SerializeField] float growthRate = 0.6f;

    [Header("Damage Settings")]
    [SerializeField] float damage = 25f;
    [SerializeField] float ColorDuration;

    [Tooltip("This option provides unique Damage Effect.")]
    [SerializeField] bool isFreezer = false;

    Vector2 zeroVelocity = new Vector2(0, 0);
    GameObject parent = null;
    float time = 0;
    float xScale = 0;
    float yScale = 0;

    private void Start()
    {
        transform.localScale = new Vector2(0, 0);

       // damage = parent.GetComponent<BaseStats>().GetStat(Stat.Damage);
    }

    void Update()
    {
        if (parent == null)
        {
            Destroy(gameObject);
        }

        if (time >= lifetime)
        {
            Destroy(gameObject);
        }

        time += Time.deltaTime;

        if (xScale > fireballScale && yScale > fireballScale)
        {
            GetComponent<CircleCollider2D>().enabled = true;
            transform.Translate(velocity * Time.deltaTime);
            return;
        }

        GetComponent<CircleCollider2D>().enabled = false;
        xScale += Time.deltaTime * growthRate;
        yScale += Time.deltaTime * growthRate;
        transform.localScale = new Vector2(xScale, yScale);
        transform.Translate(zeroVelocity);
    }

    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //To do not interact with game object that is not in same horizontal grid line with this projectile
            if (Mathf.Abs(collision.gameObject.transform.position.y - gameObject.transform.position.y) > hitToleranceY)
            {
                return;
            }

            GameObject mainPlayer = GameObject.FindGameObjectWithTag("Player");

            if (isFreezer)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(mainPlayer, damage, Color.cyan, ColorDuration);
            }
            else
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(mainPlayer, damage);
            }

            Destroy(gameObject);
        }
    }
}
