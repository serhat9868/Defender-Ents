using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Effect", menuName = "Ability/Effect/Color Effect", order = 0)]
public class ColorEffect : EffectStrategy
{
    public override void StartEffect(AbilityData data, Action finished)
    {
        foreach(var target in data.GetTargets())
        {
            target.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("red target is  " + target.gameObject.name);
        }
    }
}
