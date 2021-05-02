using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPenalties : MonoBehaviour
{
    OrderGenerator orderGenerator;
    public Image[] penaltyIcons;
    private int numPenalties = 0;
    [SerializeField] private Camera mainCamera;
    [SerializeField][Range(0,2)] private float stampTime;
    [SerializeField][Range(0,2)] private float stampScale;
    [SerializeField] private AnimationCurve stampScaleCurve;
    [SerializeField] private AnimationCurve stampPositionCurve;
    [SerializeField] private AnimationCurve stampOpacityCurve;

    private void Awake()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
        for (int i = 0; i < penaltyIcons.Length; i++)
        {
            penaltyIcons[i].gameObject.SetActive(false);
        }
    }

    public void UpdatePenaltyIconUI()
    {
        for (int i = numPenalties; i < penaltyIcons.Length; i++)
        {
            if(orderGenerator.currentPlayerPenalties > i)
            {
                if(!penaltyIcons[i].gameObject.activeSelf)
                {
                    penaltyIcons[i].gameObject.SetActive(true);
                    StartCoroutine(SadStamp(penaltyIcons[i]));
                }
                
            }
            else
            {
                penaltyIcons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateAllPenaltyIconUI()
    {
        for (int i = numPenalties; i < penaltyIcons.Length; i++)
        {
            if(orderGenerator.currentPlayerPenalties > i)
            {
                if(!penaltyIcons[i].gameObject.activeSelf)
                {
                    penaltyIcons[i].gameObject.SetActive(true);
                }
                
            }
            else
            {
                penaltyIcons[i].gameObject.SetActive(false);
            }
        }
        StartCoroutine(SadStampAll());
    }


    private IEnumerator SadStamp(Image sadFace)
    {
        Sequence stampSequence = DOTween.Sequence();
        stampSequence.Append(sadFace.DOFade(0, stampTime).SetEase(stampOpacityCurve).From());
        stampSequence.Join(sadFace.GetComponent<RectTransform>().DOScale(stampScale, stampTime).SetEase(stampScaleCurve).From());
        stampSequence.Join(mainCamera.DOShakePosition(.3f,.5f,20,100,true));
        // stampSequence.Append(mainCamera.DOShakePosition(.3f,3,20,100,true));
        yield return stampSequence.WaitForCompletion();
    }

    private IEnumerator SadStampAll()
    {

        Sequence stampSequence = DOTween.Sequence();
        stampSequence.AppendInterval(0);
        foreach(Image sadFace in penaltyIcons)
        {
            stampSequence.Join(sadFace.DOFade(0, stampTime).SetEase(stampOpacityCurve).From());
            stampSequence.Join(sadFace.GetComponent<RectTransform>().DOScale(stampScale, stampTime).SetEase(stampScaleCurve).From());
        }
        
        stampSequence.Join(mainCamera.DOShakePosition(.3f,.5f,20,100,true));
        // stampSequence.Append(mainCamera.DOShakePosition(.3f,3,20,100,true));
        yield return stampSequence.WaitForCompletion();
    }
}
