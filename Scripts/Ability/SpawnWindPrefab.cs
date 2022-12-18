using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Wind", menuName = "Ability/Effect/Spawn Wind", order = 0)]
public class SpawnWindPrefab : EffectStrategy
{
    [Header("Boundary")]
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;

    [Header("Wind Settings")]
    [SerializeField] GameObject windPrefab;
    [SerializeField] float lifetime;
    [SerializeField] float spawningRate = 0.1f;
    [SerializeField] int windAmount = 250;

    float time;
    public override void StartEffect(AbilityData data, Action finished)
    {
       data.StartCoroutine(SpawnWind(data,finished));
    }

    private IEnumerator SpawnWind(AbilityData data,Action finished)
    {
        data.SetAbilityMode(true);
        for (int i = 0; i < windPrefab.transform.childCount; i++)
        {
            int randomWind = UnityEngine.Random.Range(0, windPrefab.transform.childCount);
            float randomX = UnityEngine.Random.Range(minX, maxX);
            float randomY = UnityEngine.Random.Range(minY, maxY);
            Vector2 randomPosition = new Vector2(randomX, randomY);
            Instantiate(windPrefab.transform.GetChild(randomWind),randomPosition,Quaternion.identity);
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < windAmount; i++)
        {
            int randomWind = UnityEngine.Random.Range(0, windPrefab.transform.childCount);
            float randomX = UnityEngine.Random.Range(minX, maxX);
            float randomY = UnityEngine.Random.Range(minY, maxY);
            Vector2 randomPosition = new Vector2(randomX, randomY);
            Instantiate(windPrefab.transform.GetChild(randomWind), randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawningRate);
        }
        data.SetAbilityMode(false);
    }
}