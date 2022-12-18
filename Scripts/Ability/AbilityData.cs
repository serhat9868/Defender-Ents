using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityData
{
    GameObject user;
    Vector2 targetedPoint;
    IEnumerable<GameObject> targets;
    Ability activeAbility;
    bool isAbilityMode;
    float activeTime;

    public AbilityData(GameObject user)
    {
        this.user = user;
    }

    public IEnumerable<GameObject> GetTargets()
    {
        return targets;
    }

    public void SetTargets(IEnumerable<GameObject> setTarget)
    {
        targets = setTarget;
    }

    public void SetTargetedPoint(Vector2 NewTargetedPoint)
    {
        targetedPoint = NewTargetedPoint;
    }

    public Vector3 GetTargetedPoint()
    {
        return targetedPoint;
    }
    public GameObject GetUser()
    {
        return user;
    }

    public void StartCoroutine(IEnumerator coroutine)
    {
        user.GetComponent<MonoBehaviour>().StartCoroutine(coroutine);
    }
    
    public bool IsAbilityMode()
    {
        return isAbilityMode;
    }

    public void SetAbilityMode(bool state)
    {
        isAbilityMode = state;
    }

    public void SetActiveAbility(Ability ability)
    {
        activeAbility = ability;
    }

    public Ability GetActiveAbility()
    {
        return activeAbility;
    }

    public void ActiveTime(float activeTime)
    {
        this.activeTime = activeTime;
    }

    public float GetActiveTime()
    {
        return activeTime;
    }
}