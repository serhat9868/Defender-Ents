using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Health Effect", menuName = "Ability/Effect/Health Effect", order = 0)]
public class HealthEffect : EffectStrategy
{
    [SerializeField] float healthEffect;
    public override void StartEffect(AbilityData data, Action finished)
    {
        foreach(var target in data.GetTargets())
        {
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth)
            {
                Debug.Log("targethealth name : "+targetHealth.gameObject.name);
                if (healthEffect < 0)
                {
                    targetHealth.TakeDamage(data.GetUser(), -healthEffect);              
                }
                else
                {
                    targetHealth.Heal(healthEffect);
                }
            }                    
        }
        finished();
    }
}
