using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    FruitInBasketUI fruitInBasketUI;
    OrderGenerator orderGenerator;

    public int[] fruitInBasket = new int[5];


    private void Awake()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
        fruitInBasketUI = FindObjectOfType<FruitInBasketUI>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fruit")
        {
            Fruit newFruit = other.gameObject.GetComponent<Fruit>();
            ProcessFruit(newFruit);
        }
    }

    private void ProcessFruit(Fruit caughtFruit)
    {
        fruitInBasket[(int)caughtFruit.fruitType]++;
        print(caughtFruit.fruitType);
        fruitInBasketUI.UpdateFruitAmountText();
        orderGenerator.CheckBasket();
        Destroy(caughtFruit.gameObject);
    }

    public void ClearBasket()
    {
        fruitInBasketUI.ClearFruitAmountText();
        fruitInBasket = new int[5];
    }
}
