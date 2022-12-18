using RPG.Stats;
using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    BuildingSystem buildingSystem;
    LazyValue<float> healthPoints;
    Coroutine coroutine;
    bool isDamageCoroutineMode = false;
    private void Awake()
    {
        healthPoints = new LazyValue<float>(GetInitialHealth);
        buildingSystem = GameObject.FindGameObjectWithTag("GridMap").GetComponent<BuildingSystem>();
    }

    private void Update()
    {
       // Debug.Log(gameObject.name + " health is : " + healthPoints.value);
    }

    private void Start()
    {
        healthPoints.ForceInit();
    }

    private float GetInitialHealth()
    {
        return GetComponent<BaseStats>().GetStat(Stat.Health);
    }

    public void TakeDamage(GameObject instigator,float damage)
    {
        healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

        if(healthPoints.value > 0 && !isDamageCoroutineMode)
        {
            StartCoroutine(DamageEffect(0.15f,Color.red));
        }
      
        if(healthPoints.value <= 0)
        {
            if (!isPlayer)
            {
             //   AwardExperience(instigator);
            }
            Die();          
        }
    }

    public void TakeDamage(GameObject instigator, float damage,Color damageColor,float damageColorDuration)
    {
        healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
        if (healthPoints.value > 0)
        {
            if (isDamageCoroutineMode)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(DamageEffect(damageColorDuration, damageColor));
        }

        if (healthPoints.value <= 0)
        {
            if (!isPlayer)
            {
                //   AwardExperience(instigator);
            }
            Die();
        }
    }

    public void Heal(float healthEffect)
    {
        healthPoints.value = Mathf.Min(GetInitialHealth(),healthPoints.value + healthEffect);
    }

    private IEnumerator DamageEffect(float colorTime,Color damageColor)
    {
        isDamageCoroutineMode = true;
        GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(colorTime);
        GetComponent<SpriteRenderer>().color = Color.white;
        isDamageCoroutineMode = false;
    }

    private void Die()
    {
        if (isPlayer)
        {
            buildingSystem.RemoveFromDictionary(gameObject);
        }
        Destroy(gameObject);     
    }

    public float GetHealthPoints()
    {
        return healthPoints.value;
    }
}
