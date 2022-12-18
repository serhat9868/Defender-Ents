using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed Effect", menuName = "Ability/Effect/Speed Effect", order = 0)]
public class WindEffect : EffectStrategy
{
    [SerializeField] Vector2 windForce;

    public override void StartEffect(AbilityData data, Action finished)
    {
        data.StartCoroutine(SpeedEffect(data, finished));       
    }

    private IEnumerator SpeedEffect(AbilityData data,Action finished)
    {
        foreach (var target in data.GetTargets())
        {
            target.GetComponent<Rigidbody2D>().velocity += windForce;        
        }

        yield return new WaitForSeconds(1f);

        yield return new WaitWhile(() => data.IsAbilityMode());

        foreach (var target in data.GetTargets())
        {
            target.GetComponent<Rigidbody2D>().velocity -= windForce / 2;
        }

        yield return new WaitForSeconds(1.5f);

        foreach (var target in data.GetTargets())
        {
            target.GetComponent<Rigidbody2D>().velocity -= windForce/2;
        }
        Debug.Log("wind removed");
        finished();
    }
}
