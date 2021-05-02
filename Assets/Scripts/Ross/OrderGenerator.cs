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
    [SerializeField][Range(0,1)] private float TicketTweenXRatio;
    [SerializeField][Range(0,1)] private float TicketTweenXRatioOutside;
    [SerializeField][Range(0,1)] private float TicketTweenYRatio;
    [SerializeField][Range(0,4)] private float TicketTweenInTime;
    [SerializeField][Range(0,4)] private float TicketTweenOutTime;
    [SerializeField][Range(0,4)] private float InactiveWaitTime;
    [SerializeField] private AnimationCurve InAnimationCurve;
    [SerializeField] private AnimationCurve OutAnimationCurve;
    [SerializeField] private float RotateAmount;
    [SerializeField][Range(0,2)] private float RotateTime;
    [SerializeField][Range(0,10)] private int RotateVibrato;
    [SerializeField][Range(0,1)] private float RotateElasticity;
    [SerializeField] private AnimationCurve InRotateAnimationCurve;
    [SerializeField] private AnimationCurve OutRotateAnimationCurve;

    public FruitType nextFruitOnTicket;
    public FruitType[] fruitTypesOnTicket;

    private WaitForSeconds InactiveWait;
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
        // SetNextFruitOnOrder();
        string currentOrderStatus = "Generated Order: ";
        for(int i = 0; i < currentFruitAmountsInOrder.Length; i++)
        {
            currentOrderStatus+= (FruitType)i + ": " +currentFruitAmountsInOrder[i] + " ";
        }
        print(currentOrderStatus);
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
                fruitAmountText[i].text = "x" + (currentFruitAmountsInOrder[i]).ToString();
            }
        }
    }

    private void UpdateOrderUI()
    {
        for (int i = 0; i < currentFruitAmountsInOrder.Length; i++)
        {
            fruitAmountText[i].text = "x" + (currentFruitAmountsInOrder[i]).ToString();
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

    public IEnumerator CheckBasket()
    {
        if(currentPlayerPenalties>=3)
        {
            StartCoroutine(GenerateRandomOrder());
        }

        for (int i = 0; i < currentFruitAmountsInOrder.Length; i++)
        {
            if(currentFruitAmountsInOrder[i] <= 0)
            {
                
                if(fruitIcons[i].gameObject.activeSelf)
                {
                    StartCoroutine(WaitToSetInactive(fruitIcons[i].gameObject));
                }
            }
        }
        
        // string currentOrderStatus = "Order Left";
        // for(int i = 0; i < currentFruitAmountsInOrder.Length; i++)
        // {
        //     currentOrderStatus+= (FruitType)i + ": " + currentFruitAmountsInOrder[i] + " ";
        // }
        // print(currentOrderStatus);
        yield return null;
        UpdateOrderUI();


        if(CheckFruitAmountsArrayIsZero())
        {
            GameObject successFX = Instantiate(basket.successFX, basket.transform)as GameObject;
            Destroy(successFX, 1f);

            StartCoroutine(GenerateRandomOrder());
            GameManager.Instance.AddMoney( 1.0f );
            GenerateRandomOrder();
        }
    }
    private bool CheckFruitAmountsArrayIsZero()
    {
        bool allZero = true;
        foreach (int num in currentFruitAmountsInOrder)
        {
            if (num != 0)
            {
                allZero = false;
                break;
            }
        }
        return allZero;
    }
    // public void SetNextFruitOnOrder()
    // {
        
    //     if(currentFruitAmountsInOrder[0] >= 1)
    //     {
    //         nextFruitOnTicket = FruitType.Apple;
    //     }
    //     else if(currentFruitAmountsInOrder[1] >= 1)
    //     {
    //         nextFruitOnTicket = FruitType.Banana;
    //     }
    //     else if(currentFruitAmountsInOrder[2] >= 1)
    //     {
    //         nextFruitOnTicket = FruitType.Mango;
    //     }
    //     else if(currentFruitAmountsInOrder[3]>= 1)
    //     {
    //         nextFruitOnTicket = FruitType.Orange;
    //     }
    //     else if(currentFruitAmountsInOrder[4] >= 1)
    //     {
    //         nextFruitOnTicket = FruitType.Peach;
    //     }
    //     else
    //     {
    //         Debug.Log("Order is complete.");
    //     }
    //     print("next fruit: " + nextFruitOnTicket);
    // }

    private IEnumerator BounceTicketIn()
    {
        Sequence bounceIn = DOTween.Sequence();
        bounceIn.Append(TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatio, Screen.height * TicketTweenYRatio), TicketTweenInTime)
            .SetEase(InAnimationCurve));
        bounceIn.Join(TicketTransform.DOPunchRotation(new Vector3(0, 0, RotateAmount), RotateTime, RotateVibrato, RotateElasticity)
            .SetEase(InRotateAnimationCurve));
        yield return bounceIn.WaitForCompletion();
    }

    private IEnumerator BounceTicketOut()
    {
        Sequence bounceOut = DOTween.Sequence();
        bounceOut.Append(TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatioOutside, Screen.height * TicketTweenYRatio), TicketTweenOutTime)
            .SetEase(OutAnimationCurve));
        bounceOut.Join(TicketTransform.DOPunchRotation(new Vector3(0, 0, -RotateAmount), TicketTweenOutTime, RotateVibrato, RotateElasticity)
            .SetEase(OutRotateAnimationCurve));
        // Tween bounceOut = TicketTransform.DOMove(new Vector2(Screen.width * TicketTweenXRatioOutside, Screen.height * TicketTweenYRatio), TicketTweenOutTime)
        //     .SetEase(OutAnimationCurve);
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
    }
}

