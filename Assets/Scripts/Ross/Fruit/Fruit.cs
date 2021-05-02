using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType{
    Apple, Banana, Mango, Orange, Peach, Rotten
}

public class Fruit : MonoBehaviour
{
    public float baseValue;
    public FruitType fruitType;
    void Update()
    {
        RotateFruit();
    }

    void RotateFruit()
    {
        transform.Rotate( -0.5f, 0.5f, -0.5f);
    }
}
