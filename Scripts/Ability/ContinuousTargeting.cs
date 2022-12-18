using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Continuous Targeting", menuName = "Ability/Targeting/Continuous Targeting", order = 0)]
public class ContinuousTargeting : TargetingStrategy
{
    [SerializeField] GameObject effectAreaPrefab;
    GameObject effectArea = null;

    public override void StartTargeting(AbilityData data, Action finished)
    {
        if (effectArea == null)
        {
            effectArea = Instantiate(effectAreaPrefab, effectAreaPrefab.transform.position,Quaternion.identity);
        }

        else
        {
            effectArea.SetActive(true);
        }

        Debug.Log("Continuous Targeting was started");
        finished();
    }
}


