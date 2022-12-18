using RPG.Stats;
using RPG.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float fightingRangeX = 4f;
    [SerializeField] float fightingRangeY = 2f;
    [SerializeField] float hitRate = 0.5f;

    LazyValue<float> speed;
    LazyValue<float> damage;

    Coroutine coroutine;
    GameObject target;
    Rigidbody2D rigidbody;
    float time = Mathf.Infinity;
    float firstHitRate;
    bool isCoroutineMode = false;

    private void Awake()
    {
        speed = new LazyValue<float>(GetInitialSpeed);
        rigidbody = GetComponent<Rigidbody2D>();
        damage = new LazyValue<float>(GetInitialDamageValue);
        firstHitRate = hitRate;
    }

    private float GetInitialSpeed()
    {
        return GetComponent<BaseStats>().GetStat(Stat.Speed);
    }

    private float GetInitialHitRate()
    {
        return firstHitRate;
    }

    private float GetInitialDamageValue()
    {
        return GetComponent<BaseStats>().GetStat(Stat.Damage);
    }

    private void Start()
    {
        damage.ForceInit();    
    }

    void Update()
    {
        time += Time.deltaTime;

        GameObject[] ents = GameObject.FindGameObjectsWithTag("Ent");

        if (ents.Length > 0)
        {
            float currentDistance = fightingRangeX;
            foreach (GameObject ent in ents)
            {
                if (ent.GetComponent<DraggingObject>().IsDraggingMode()) continue;
                float distanceX = gameObject.transform.position.x - ent.transform.position.x;
                float distanceY = gameObject.transform.position.y - ent.transform.position.y;
                bool isInFightingRange = Mathf.Abs(distanceY) < fightingRangeY && Mathf.Abs(distanceX) < fightingRangeX;

                if (isInFightingRange)
                {
                    target = ent;
                    Attack();
                    return;
                }
            }
        }
        transform.Translate(Vector2.left * speed.value * Time.deltaTime);
    }

    public void Attack()
    {
        rigidbody.velocity = new Vector2(0, 0);

        if (time > 1 / hitRate)
        {
            target.GetComponent<Health>().TakeDamage(gameObject, damage.value);
            time = 0;
        }
    }

    public IEnumerator FreezeEnemy(float freezingTime, float slowRange)
    {
        Debug.Log("Freeze Enemy started");
        isCoroutineMode = true;
        speed.value = GetInitialSpeed()*slowRange;
        hitRate = firstHitRate * slowRange;
        yield return new WaitForSeconds(freezingTime * 0.8f);        
        speed.value /= 2*slowRange;
        hitRate /= 2*slowRange;
        Debug.Log("velocity " + speed.value);
        yield return new WaitForSeconds(freezingTime* 0.2f);
        speed.value = GetInitialSpeed();
        hitRate = GetInitialHitRate();
        Debug.Log("velocity " + speed.value);
        isCoroutineMode = false;
    }

    public void StartFreezeEnemyCoroutine(float freezingTime,float slowRange,bool isContinuous)
    {
        if (isContinuous && isCoroutineMode) return;

        if (isCoroutineMode && !isContinuous)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(FreezeEnemy(freezingTime, slowRange));
    }
}
   