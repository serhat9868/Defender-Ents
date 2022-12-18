using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tag Filter", menuName = "Ability/Filters/Tag", order = 0)]
public class TagFilter : FilterStrategy
{
    [SerializeField] string tagToFilter;
    public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
    {
        foreach (var gameobject in objectsToFilter)
        {
            if (gameobject.CompareTag(tagToFilter))
            {
                yield return gameobject;
            }
        }
    }
}