using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Delayed Click Targeting", menuName = "Ability/Targeting/Delayed Click", order = 0)]
public class DelayedClickTargeting : TargetingStrategy
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float areaAffectRadius;
    [Range(0, 1)]
    [SerializeField] float globalVolume;
    [SerializeField] Transform targetingPrefab;

    Transform targetingPrefabInstance = null;

    public override void StartTargeting(AbilityData data, Action finished)
    {
        MainPlayerController mainPlayerController = data.GetUser().GetComponent<MainPlayerController>();
        mainPlayerController.StartCoroutine(Targeting(data, finished));
    }

    private IEnumerator Targeting(AbilityData data, Action finished)
    {
        if (targetingPrefabInstance == null)
        {
            targetingPrefabInstance = Instantiate(targetingPrefab);
        }
        else
        {
            targetingPrefabInstance.gameObject.SetActive(true);
        }

        targetingPrefabInstance.localScale = new Vector3(areaAffectRadius * 2, areaAffectRadius * 2,1);

          while (true)
          {
            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            Debug.Log("time remaining " + cooldownStore.GetTimeRemaining(data.GetActiveAbility()));
            RaycastHit2D hit = Physics2D.Raycast(MainPlayerController.GetMousePosition(), Vector2.zero, 1000, layerMask);
            BuildingSystem buildingSystem = FindObjectOfType<BuildingSystem>();
            Vector2 gridPosition = buildingSystem.SnapCoordinateToGrid(hit.point);
            targetingPrefabInstance.position = gridPosition;
            if (Input.GetMouseButtonDown(0))
            {
                yield return new WaitWhile(() => Input.GetMouseButton(0));
                targetingPrefabInstance.gameObject.SetActive(false);
                data.SetTargetedPoint(hit.point);
                data.SetTargets(GetGameObjectsInRadius(hit.point));
                finished();
                break;
            }

            yield return null;
          }
    }
    private IEnumerable<GameObject> GetGameObjectsInRadius(Vector2 point)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll
                (point, areaAffectRadius, Vector2.left, 0);

        foreach (var hit in hits)
        {
            yield return hit.collider.gameObject;
        }
    }
}