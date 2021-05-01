using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    public GameObject[] hazardPrefabsToSpawn;
    public float minimumTimeBetweenDrops;
    public float maximumTimeBetweenDrops;

    private void Start()
    {
        StartCoroutine(FruitSpawningCoroutine());
    }

    private void SpawnAHazard()
    {
        Instantiate(hazardPrefabsToSpawn[Random.Range(0, hazardPrefabsToSpawn.Length)], transform.position, Quaternion.identity);
    }

    private void CallNewDrop()
    {
        StartCoroutine(FruitSpawningCoroutine());
    }

    private IEnumerator FruitSpawningCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minimumTimeBetweenDrops, maximumTimeBetweenDrops));
        SpawnAHazard();
        CallNewDrop();
    }
}
