using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "My Ability", menuName = "Ability/New Ability", order = 0)]
public class Ability : ScriptableObject
{
    [Header("Strategy")]
    [SerializeField] TargetingStrategy targetingStrategy;
    [SerializeField] FilterStrategy[] filteringStrategies;
    [SerializeField] EffectStrategy[] effectStrategies;

    [Header("Settings")]
    [SerializeField] CharacterClass characterClass;
    [SerializeField] float cooldownTime = 3f;
    [SerializeField] bool isContinuousAbility = false;

    [Header("Continuous Ability Settings")]
    [SerializeField] float abilityTime;
    public void UseAbility(GameObject user)
    {
        CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
        if (cooldownStore.GetTimeRemaining(this) > 0) return;
        AbilityData data = new AbilityData(user);
        data.SetAbilityMode(true);
        data.SetActiveAbility(this);

        if (isContinuousAbility)
        {
            data.ActiveTime(abilityTime);
            Debug.Log("active time " + data.GetActiveTime());
        }

        targetingStrategy.StartTargeting(data, () =>
        {
            TargetAquired(data);
        });
    }

    private void TargetAquired(AbilityData data)
    {
        CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
        cooldownStore.StartCooldown(this, cooldownTime);
        Debug.Log("time remaining " + cooldownStore.GetTimeRemaining(data.GetActiveAbility()));

        foreach (var filterStrategy in filteringStrategies)
        {
            data.SetTargets(filterStrategy.Filter(data.GetTargets()));
        }

        foreach (var effect in effectStrategies)
        {
            effect.StartEffect(data, EffectFinished);
        }

        return;

        foreach (var target in data.GetTargets())
        {
            Debug.Log("target name "+target.name);
        }
    }

    private void EffectFinished()
    {

    }
}