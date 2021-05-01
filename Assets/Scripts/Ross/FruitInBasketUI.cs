using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitInBasketUI : MonoBehaviour
{
    Basket basket;
    [SerializeField] private List<Text> fruitAmountTexts;

    private void Awake()
    {
        basket = FindObjectOfType<Basket>();
    }

    public void UpdateFruitAmountText()
    {
        for(int fruitTextIndex = 0; fruitTextIndex < fruitAmountTexts.Count; fruitTextIndex++)
        {
            fruitAmountTexts[fruitTextIndex].text = basket.fruitInBasket[fruitTextIndex].ToString();
        }
    }

    public void ClearFruitAmountText()
    {
        foreach(Text fruitAmountText in fruitAmountTexts)
        {
            fruitAmountText.text = "0";
        }
    }
}
