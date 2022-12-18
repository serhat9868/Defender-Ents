using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoController : MonoBehaviour
{
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] Vector2 jumpingForce;
    float targetXPoint;
    Rigidbody2D rigidbody;
    Vector2 zeroVelocity;
    bool isForced = false;
    float startingY;
    float timeSinceForce = 0;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        targetXPoint = Random.Range(minX, maxX);
        startingY = transform.position.y;
        zeroVelocity = new Vector2(0, 0);
    }

    void Update()
    {
        Debug.Log("enemycontroller is " + GetComponent<EnemyController>().enabled);

        if (transform.position.y <= startingY && isForced && timeSinceForce > 0.2f)
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            isForced = false;
            return;
        }
        if (isForced)
        {
            timeSinceForce += Time.deltaTime;
        }
        if (Mathf.Abs(transform.position.x - targetXPoint) <= 5 && !isForced)
        {
            Debug.Log("isforceApplied to " + gameObject.name);
            GetComponent<EnemyController>().enabled = false;
            rigidbody.gravityScale = 4;
            rigidbody.AddForce(jumpingForce, ForceMode2D.Impulse);
            isForced = true;
        }
    }
}
