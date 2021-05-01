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
    [SerializeField] private RectTransform TicketTransform;
    [SerializeField] private float TicketTweenXRatio;
    [SerializeField] private float TicketTweenXRatioOutside;
    [SerializeField] private float TicketTweenYRatio;
    [SerializeField] private float TicketTweenInTime;
    [SerializeField] private float TicketTweenOutTime;
    [SerializeField] private AnimationCurve InAnimationCurve;
    [SerializeField] private AnimationCurve OutAnimationCurve;
    private void Awake()
    {
        basket = FindObjectOfType<Basket>();
        DOTween.Init(true, true, LogBehaviour.Default);
    }

    private void Start()
    {
        StartCoroutine(GenerateRandomOrder());
    }

    private void Update()
    {
        TickOrderTime();
    }

    private IEnumerator GenerateRandomOrder()
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
        yield return BounceTicketOut();
        InitializeOrderUI();
        yield return BounceTicketIn();
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
            StartCoroutine(GenerateRandomOrder());
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
            
            StartCoroutine(GenerateRandomOrder());
        }
    }

    private IEnumerator BounceTicketIn()
    {
        Tween bounceIn = TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatio, Screen.height * TicketTweenYRatio), TicketTweenInTime)
                            .SetEase(InAnimationCurve);
        yield return bounceIn.WaitForCompletion();
    }

    private IEnumerator BounceTicketOut()
    {
        Tween bounceOut = TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatioOutside, Screen.height * TicketTweenYRatio), TicketTweenOutTime)
            .SetEase(OutAnimationCurve);
        yield return bounceOut.WaitForCompletion();
    }

    private IEnumerator BounceTicketOutThenIn()
    {
        Sequence bounceOutThenIn = DOTween.Sequence();
        bounceOutThenIn.Append(TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatioOutside, Screen.height * TicketTweenYRatio), TicketTweenOutTime)
            .SetEase(OutAnimationCurve));
        bounceOutThenIn.Append(TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatio, Screen.height * TicketTweenYRatio), TicketTweenInTime)
            .SetEase(InAnimationCurve));
        
        yield return bounceOutThenIn.WaitForCompletion();
        // TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatioOutside, Screen.height * TicketTweenYRatio), TicketTweenOutTime)
        //     .SetEase(OutAnimationCurve);
        // TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatio, Screen.height * TicketTweenYRatio), TicketTweenInTime)
        //     .SetEase(InAnimationCurve);
    }
}

