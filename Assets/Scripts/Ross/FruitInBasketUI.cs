using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitInBasketUI : MonoBehaviour
{
    Basket basket;

    public Text fruit1AmountText;
    public Text fruit2AmountText;
    public Text fruit3AmountText;
    public Text fruit4AmountText;
    public Text fruit5AmountText;

    private void Awake()
    {
        basket = FindObjectOfType<Basket>();
    }

    public void UpdateFruitAmountText()
    {
        fruit1AmountText.text = basket.fruit1Total.ToString();
        fruit2AmountText.text = basket.fruit2Total.ToString();
        fruit3AmountText.text = basket.fruit3Total.ToString();
        fruit4AmountText.text = basket.fruit4Total.ToString();
        fruit5AmountText.text = basket.fruit5Total.ToString();
    }

    public void ClearFruitAmountText()
    {
        fruit1AmountText.text = "0";
        fruit2AmountText.text = "0";
        fruit3AmountText.text = "0";
        fruit4AmountText.text = "0";
        fruit5AmountText.text = "0";
    }
}
