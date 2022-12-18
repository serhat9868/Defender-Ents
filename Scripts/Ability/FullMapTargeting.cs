using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Full Map Targeting", menuName = "Ability/Targeting/Full Map", order = 0)]
public class FullMapTargeting : TargetingStrategy
{
    [SerializeField] float areaHeight;
    [SerializeField] float areaWidth;
    public override void StartTargeting(AbilityData data, Action finished)
    {
        MainPlayerController mainPlayerController = data.GetUser().GetComponent<MainPlayerController>();
        mainPlayerController.StartCoroutine(Targeting(data, finished));
    }

    private IEnumerator Targeting(AbilityData data, Action finished)
    {
        CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
        while (cooldownStore.GetTimeRemaining(data.GetActiveAbility()) == 0)
        {
            RaycastHit2D hit = Physics2D.BoxCast(Vector2.zero, new Vector2(areaWidth, areaHeight), 0, Vector2.zero);
            data.SetTargetedPoint(hit.point);
            data.SetTargets(GetGameobjectsInMap(hit.point));
            if (Input.GetMouseButtonDown(0))
            {
                finished();
                break;
            }
            yield return null;
        }

    }
    private IEnumerable<GameObject> GetGameobjectsInMap(Vector2 point)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(point, new Vector2(areaWidth, areaHeight), 0, Vector2.zero);

        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.gameObject == null);

            yield return hit.collider.gameObject;
        }
    }
}
