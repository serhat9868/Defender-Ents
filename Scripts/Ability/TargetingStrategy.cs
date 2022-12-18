using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Targeting Strategy", menuName = "Ability/Targeting/New Targeting Strategy", order = 0)]
public abstract class TargetingStrategy : ScriptableObject
{
    public abstract void StartTargeting(AbilityData data, Action finished);
}
