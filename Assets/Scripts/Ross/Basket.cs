using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    FruitInBasketUI fruitInBasketUI;
    OrderGenerator orderGenerator;
    PlayerPenalties playerPenalties;

    public GameObject explosionFX;
    public GameObject successFX;

    public int[] fruitInBasket = new int[5];

    // audio
    public AudioSource source;
    public AudioClip fruit, stamp, rotten, order;


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
            source.PlayOneShot(rotten);
            orderGenerator.currentPlayerPenalties = 3;
            playerPenalties.UpdateAllPenaltyIconUI();
            StartCoroutine(orderGenerator.CheckBasket());
            GameObject activeExplosionFX = Instantiate(explosionFX, other.transform) as GameObject;
            activeExplosionFX.transform.SetParent(null);
            Destroy(activeExplosionFX, 1f);
            Destroy(other.gameObject);
        }
    }

    private void ProcessFruit(Fruit caughtFruit)
    {
        fruitInBasket[(int)caughtFruit.fruitType]++;
        fruitInBasketUI.UpdateFruitAmountText();
        
        if (caughtFruit.fruitType != FruitType.Rotten && orderGenerator.currentFruitAmountsInOrder[(int)caughtFruit.fruitType] > 0)
        {
            source.PlayOneShot(fruit);
            orderGenerator.currentFruitAmountsInOrder[(int)caughtFruit.fruitType]--;
        }
        else if(caughtFruit.fruitType == FruitType.Rotten)
        {
            source.PlayOneShot(rotten);
            orderGenerator.currentPlayerPenalties = 3;
            playerPenalties.UpdatePenaltyIconUI();
        }
        else
        {
            source.PlayOneShot(stamp);
            orderGenerator.currentPlayerPenalties++;
            playerPenalties.UpdatePenaltyIconUI();
        }

        StartCoroutine(orderGenerator.CheckBasket());
        Destroy(caughtFruit.gameObject);
    }

    public void ClearBasket()
    {
        fruitInBasketUI.ClearFruitAmountText();
        fruitInBasket = new int[5];
    }

    public void OrderComplete()
    {
        source.PlayOneShot(order);
    }
}
