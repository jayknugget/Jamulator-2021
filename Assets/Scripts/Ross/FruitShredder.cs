using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitShredder : MonoBehaviour
{
    public float fruitShreddingBuffer;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fruit" || other.tag == "Hazard")
        {
            StartCoroutine(FruitShredderCoroutine(other.gameObject));
        }
    }

    private IEnumerator FruitShredderCoroutine(GameObject collision)
    {
        yield return new WaitForSeconds(fruitShreddingBuffer);
        Destroy(collision);
    }
}
