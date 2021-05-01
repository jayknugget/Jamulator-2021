using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrderGenerator : MonoBehaviour
{
    Basket basket;

    public float startingSecondsLeftOnOrder;
    public float totalSecondsLeftOnOrder;

    public int[] currentFruitTypesInOrder;
    public int[] currentFruitAmountsInOrder;

    public Image[] fruitIcons;
    public Text[] fruitAmountText;

    private void Awake()
    {
        basket = FindObjectOfType<Basket>();
        DOTween.Init(true, true, LogBehaviour.Default);
    }

    private void Start()
    {
        GenerateRandomOrder();
    }

    private void Update()
    {
        TickOrderTime();
    }

    private void GenerateRandomOrder()
    {
        // There will be parallel arrays to determine what kind of fruit the customer wants
        // and what amount of that fruit they want
        basket.ClearBasket();
        totalSecondsLeftOnOrder = startingSecondsLeftOnOrder;

        int[] fruitTypesInOrder = { Random.Range(1, 5),Random.Range(1,5),
            Random.Range(1,5), Random.Range(1,5), Random.Range(1,5)};
        currentFruitTypesInOrder = fruitTypesInOrder;

        int[] fruitAmountsInOrder = { Random.Range(0, 3),Random.Range(0,3),
            Random.Range(0,3), Random.Range(0,3), Random.Range(0,3)};
        currentFruitAmountsInOrder = fruitAmountsInOrder;

        InitializeOrderUI();
    }

    private void InitializeOrderUI()
    {
        for (int i = 0; i < currentFruitTypesInOrder.Length; i++)
        {
            if(currentFruitAmountsInOrder[i]==0)
            {
                fruitIcons[i].gameObject.SetActive(false);
            }
            else
            {
                fruitIcons[i].gameObject.SetActive(true);
                fruitAmountText[i].text = "x" + (currentFruitAmountsInOrder[i] - basket.fruitInBasket[i]).ToString();
            }
        }
    }

    private void UpdateOrderUI()
    {
        for (int i = 0; i < currentFruitTypesInOrder.Length; i++)
        {
            fruitAmountText[i].text = "x" + (currentFruitAmountsInOrder[i] - basket.fruitInBasket[i]).ToString();
        }
    }

    private void TickOrderTime()
    {
        totalSecondsLeftOnOrder -= Time.deltaTime;

        if(totalSecondsLeftOnOrder<=0)
        {
            basket.ClearBasket();
            GenerateRandomOrder();
        }
    }

    public void CheckBasket()
    {
        for (int i = 0; i < currentFruitAmountsInOrder.Length; i++)
        {
            if(basket.fruitInBasket[i] >= currentFruitAmountsInOrder[i])
            {
                if(fruitIcons[i].gameObject.activeSelf)
                {
                    fruitIcons[i].gameObject.SetActive(false);
                }
            }
        }

        UpdateOrderUI();


        if(!fruitIcons[0].gameObject.activeSelf
            && !fruitIcons[1].gameObject.activeSelf
            && !fruitIcons[2].gameObject.activeSelf
            && !fruitIcons[3].gameObject.activeSelf
            && !fruitIcons[4].gameObject.activeSelf)
        {
            //GIVE PLAYER MONEY
            Debug.Log("You got all the fruit");
            GameManager.Instance.AddMoney( 1.0f );
            GenerateRandomOrder();
        }
    }
}

