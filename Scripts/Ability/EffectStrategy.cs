using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect Strategy", menuName = "Ability/Effect/New Effect Strategy", order = 0)]
public abstract class EffectStrategy : ScriptableObject
{
    public abstract void StartEffect(AbilityData data, Action finished);
}
