using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    FruitInBasketUI fruitInBasketUI;
    OrderGenerator orderGenerator;
    PlayerPenalties playerPenalties;

    public GameObject explosionFX;

    public int[] fruitInBasket = new int[5];


    private void Awake()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
        fruitInBasketUI = FindObjectOfType<FruitInBasketUI>();
        playerPenalties = FindObjectOfType<PlayerPenalties>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fruit")
        {
            Fruit newFruit = other.gameObject.GetComponent<Fruit>();
            ProcessFruit(newFruit);
        }
        else if(other.tag == "Hazard")
        {
            orderGenerator.currentPlayerPenalties = 3;
            playerPenalties.UpdatePenaltyIconUI();
            orderGenerator.CheckBasket();
            GameObject activeExplosionFX = Instantiate(explosionFX, other.transform) as GameObject;
            activeExplosionFX.transform.SetParent(null);
            Destroy(activeExplosionFX, 1f);
            Destroy(other.gameObject);
        }
    }

    private void ProcessFruit(Fruit caughtFruit)
    {
        fruitInBasket[(int)caughtFruit.fruitType]++;

        if (caughtFruit.fruitType == orderGenerator.nextFruitOnTicket)
        {
            orderGenerator.SetNextFruitOnOrder();
            Debug.Log("This is the correct fruit");
        }
        else
        {
            orderGenerator.currentPlayerPenalties++;
            playerPenalties.UpdatePenaltyIconUI();
            Debug.Log("Penalties: " + orderGenerator.currentPlayerPenalties);
        }

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
