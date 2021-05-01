using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FruitInBasketUI : MonoBehaviour
{
    Basket basket;
    [SerializeField] private OrderGenerator orderGenerator;
    [SerializeField] private List<Text> fruitAmountTexts;

    [SerializeField][Range(1,2)] private float FruitIconPulseScale;
    [SerializeField][Range(0,2)] private float FruitIconPulseTime;
    [SerializeField] private AnimationCurve FruitIconPulseIn;
    [SerializeField] private AnimationCurve FruitIconPulseOut;
    private Image[] TicketImages;
    private void Awake()
    {
        basket = FindObjectOfType<Basket>();
        TicketImages = orderGenerator.fruitIcons;
    }

    public void UpdateFruitAmountText()
    {
        for(int fruitTextIndex = 0; fruitTextIndex < fruitAmountTexts.Count; fruitTextIndex++)
        {
            if(!fruitAmountTexts[fruitTextIndex].text.Equals(basket.fruitInBasket[fruitTextIndex].ToString()))
            {
                StartCoroutine(fruitIconPulse(fruitAmountTexts[fruitTextIndex].transform.parent.GetComponent<RectTransform>()));
                if(TicketImages[fruitTextIndex].gameObject.activeSelf)
                {
                    StartCoroutine(fruitIconPulse(TicketImages[fruitTextIndex].GetComponent<RectTransform>()));
                }
            }
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

    public IEnumerator fruitIconPulse(RectTransform FruitIcon)
    {
        Sequence IconPulse = DOTween.Sequence();
        IconPulse.Append(FruitIcon.DOScale(FruitIconPulseScale,FruitIconPulseTime).SetEase(FruitIconPulseIn));
        IconPulse.Append(FruitIcon.DOScale(1,FruitIconPulseTime).SetEase(FruitIconPulseOut));
        yield return IconPulse.WaitForCompletion();
    }
}
