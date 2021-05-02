using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject [] fruitPrefabsToSpawn;
    public float minimumTimeBetweenDrops;
    public float maximumTimeBetweenDrops;
    [SerializeField] private AnimationCurve probabilities;
    private void Start()
    {
        StartCoroutine(FruitSpawningCoroutine());
    }

    private void SpawnAFruit()
    {
        int fruitToSpawn = (int)((fruitPrefabsToSpawn.Length - 1) * probabilities.Evaluate(Random.value));
        print(fruitToSpawn);
        Instantiate(fruitPrefabsToSpawn[fruitToSpawn], transform.position, Quaternion.identity);
    }


    private IEnumerator FruitSpawningCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minimumTimeBetweenDrops, maximumTimeBetweenDrops));
            SpawnAFruit();
        }
    }
}
