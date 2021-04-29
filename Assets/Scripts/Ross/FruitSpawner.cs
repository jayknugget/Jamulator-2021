using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject [] fruitPrefabsToSpawn;
    public float minimumTimeBetweenDrops;
    public float maximumTimeBetweenDrops;

    private void Start()
    {
        StartCoroutine(FruitSpawningCoroutine());
    }

    private void SpawnAFruit()
    {
        Instantiate(fruitPrefabsToSpawn[Random.Range(0, fruitPrefabsToSpawn.Length)], transform.position, Quaternion.identity);
    }

    private void CallNewDrop()
    {
        StartCoroutine(FruitSpawningCoroutine());
    }

    private IEnumerator FruitSpawningCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minimumTimeBetweenDrops, maximumTimeBetweenDrops));
        SpawnAFruit();
        CallNewDrop();
    }
}
