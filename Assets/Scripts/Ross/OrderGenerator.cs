using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrderGenerator : MonoBehaviour
{
    Basket basket;
    PlayerPenalties playerPenalties;

    public float startingSecondsLeftOnOrder;
    public float totalSecondsLeftOnOrder;

    public int currentPlayerPenalties;

    public int[] currentFruitAmountsInOrder;

    public Image[] fruitIcons;
    public Text[] fruitAmountText;
    [SerializeField] private RectTransform TicketTransform;
    [SerializeField] private float TicketTweenXRatio;
    [SerializeField] private float TicketTweenXRatioOutside;
    [SerializeField] private float TicketTweenYRatio;
    [SerializeField] private float TicketTweenInTime;
    [SerializeField] private float TicketTweenOutTime;
    [SerializeField] private float InactiveWaitTime;
    [SerializeField] private AnimationCurve InAnimationCurve;
    [SerializeField] private AnimationCurve OutAnimationCurve;
<<<<<<< HEAD

    public FruitType nextFruitOnTicket;
    public FruitType[] fruitTypesOnTicket;

=======
    private WaitForSeconds InactiveWait;
>>>>>>> 65262d55aebb65b8fd80ab4125ab16dec4aca2f5
    private void Awake()
    {
        basket = FindObjectOfType<Basket>();
        playerPenalties = FindObjectOfType<PlayerPenalties>();
        DOTween.Init(true, true, LogBehaviour.Default);
        InactiveWait = new WaitForSeconds(InactiveWaitTime);
    }

    private void Start()
    {
        currentPlayerPenalties = 0;
        playerPenalties.UpdatePenaltyIconUI();
        StartCoroutine(GenerateRandomOrder());
    }

    private void Update()
    {
        TickOrderTime();
    }

    private IEnumerator GenerateRandomOrder()
    {
        basket.ClearBasket();
        totalSecondsLeftOnOrder = startingSecondsLeftOnOrder;

        int[] fruitAmountsInOrder = { Random.Range(0, 3),Random.Range(0,3),
            Random.Range(0,3), Random.Range(0,3), Random.Range(0,3)};
        currentFruitAmountsInOrder = fruitAmountsInOrder;
        SetNextFruitOnOrder();
        yield return BounceTicketOut();
        InitializeOrderUI();
        yield return BounceTicketIn();
    }

    private void InitializeOrderUI()
    {
        for (int i = 0; i < currentFruitAmountsInOrder.Length; i++)
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
        for (int i = 0; i < currentFruitAmountsInOrder.Length; i++)
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
        if(currentPlayerPenalties>=3)
        {
            Debug.Log("You lose this order because you got 3 penalties");
            StartCoroutine(GenerateRandomOrder());
        }

        for (int i = 0; i < currentFruitAmountsInOrder.Length; i++)
        {
            if(basket.fruitInBasket[i] >= currentFruitAmountsInOrder[i])
            {
                if(fruitIcons[i].gameObject.activeSelf)
                {
                    StartCoroutine(WaitToSetInactive(fruitIcons[i].gameObject));
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
            GameManager.Instance.AddMoney( 1.0f );
            GenerateRandomOrder();
        }
    }

    public void SetNextFruitOnOrder()
    {
        if(currentFruitAmountsInOrder[0] >= 1)
        {
            nextFruitOnTicket = FruitType.Apple;
        }
        else if(currentFruitAmountsInOrder[1] >= 1)
        {
            nextFruitOnTicket = FruitType.Banana;
        }
        else if(currentFruitAmountsInOrder[2] >= 1)
        {
            nextFruitOnTicket = FruitType.Mango;
        }
        else if(currentFruitAmountsInOrder[3]>= 1)
        {
            nextFruitOnTicket = FruitType.Orange;
        }
        else if(currentFruitAmountsInOrder[4] >= 1)
        {
            nextFruitOnTicket = FruitType.Peach;
        }
        else
        {
            Debug.Log("Order is complete.");
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

        currentPlayerPenalties = 0;
        playerPenalties.UpdatePenaltyIconUI();
    }

    private IEnumerator BounceTicketOutThenIn()
    {
        Sequence bounceOutThenIn = DOTween.Sequence();
        bounceOutThenIn.Append(TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatioOutside, Screen.height * TicketTweenYRatio), TicketTweenOutTime)
            .SetEase(OutAnimationCurve));
        bounceOutThenIn.Append(TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatio, Screen.height * TicketTweenYRatio), TicketTweenInTime)
            .SetEase(InAnimationCurve));
        
        yield return bounceOutThenIn.WaitForCompletion();
    }

    private IEnumerator WaitToSetInactive(GameObject objectToInactive)
    {
        yield return InactiveWait;
        objectToInactive.SetActive(false);
        CheckBasket();
    }
}

