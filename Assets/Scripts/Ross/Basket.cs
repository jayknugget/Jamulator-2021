using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    FruitInBasketUI fruitInBasketUI;
    OrderGenerator orderGenerator;

    public int fruit1Total;
    public int fruit2Total;
    public int fruit3Total;
    public int fruit4Total;
    public int fruit5Total;

    public int[] fruitInBasket = new int[5];


    private void Awake()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
        fruitInBasketUI = FindObjectOfType<FruitInBasketUI>();

        fruitInBasket[0] = fruit1Total;
        fruitInBasket[1] = fruit2Total;
        fruitInBasket[2] = fruit3Total;
        fruitInBasket[3] = fruit4Total;
        fruitInBasket[4] = fruit5Total;
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
        if(caughtFruit.fruitNum == 1)
        {
            fruit1Total++;
            fruitInBasket[0] = fruit1Total;
        }
        else if(caughtFruit.fruitNum == 2)
        {
            fruit2Total++;
            fruitInBasket[1] = fruit2Total;
        }
        else if(caughtFruit.fruitNum == 3)
        {
            fruit3Total++;
            fruitInBasket[2] = fruit3Total;
        }
        else if(caughtFruit.fruitNum == 4)
        {
            fruit4Total++;
            fruitInBasket[3] = fruit4Total;
        }
        else if(caughtFruit.fruitNum == 5)
        {
            fruit5Total++;
            fruitInBasket[4] = fruit5Total;
        }
        else
        {
            Debug.LogError("Not a valid fruit type!");
        }
        orderGenerator.CheckBasket();
        fruitInBasketUI.UpdateFruitAmountText();
        Destroy(caughtFruit.gameObject);
    }

    public void ClearBasket()
    {
        fruitInBasketUI.ClearFruitAmountText();
        fruit1Total = 0;
        fruit2Total = 0;
        fruit3Total = 0;
        fruit4Total = 0;
        fruit5Total = 0;

        fruitInBasket[0] = fruit1Total;
        fruitInBasket[1] = fruit2Total;
        fruitInBasket[2] = fruit3Total;
        fruitInBasket[3] = fruit4Total;
        fruitInBasket[4] = fruit5Total;
    }
}
