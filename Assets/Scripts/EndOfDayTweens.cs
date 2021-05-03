using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndOfDayTweens : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float paperBG_Y;
    [SerializeField] private RectTransform paperBG;
    [SerializeField][Range(0,1)] private float paperBGSquashFactor;
    [SerializeField] private AnimationCurve paperBGScaleCurve;
    [SerializeField][Range(0,2)] private float paperBGBounceUpTime;
    [SerializeField][Range(0,2)] private float paperBGBounceUpWaitTime;
    [SerializeField] private AnimationCurve paperBGBounceUpCurve;

    [SerializeField] private Image pin;
    [SerializeField][Range(0,2)] private float pinTime;
    [SerializeField][Range(0,5)] private float pinScale;
    [SerializeField] private AnimationCurve pinScaleCurve;
    [SerializeField] private AnimationCurve pinPositionCurve;
    [SerializeField] private AnimationCurve pinOpacityCurve;

    void Start()
    {
        LoadInMenuSequence();
    }

    private void LoadInMenuSequence()
    {
        Sequence paperBGSequence= DOTween.Sequence();
        paperBGSequence.Append(paperBG.DOMoveY(Screen.height * paperBG_Y, paperBGBounceUpTime).SetEase(paperBGBounceUpCurve));
        paperBGSequence.Join(paperBG.DOScaleY(paperBGSquashFactor, paperBGBounceUpTime).SetEase(paperBGScaleCurve));    
        
        Sequence pinSequence = DOTween.Sequence();
        pinSequence.Append(pin.DOFade(0, pinTime).SetEase(pinOpacityCurve).From());
        pinSequence.Join(pin.GetComponent<RectTransform>().DOScale(pinScale, pinTime).SetEase(pinScaleCurve).From());

        Sequence LoadSequence = DOTween.Sequence();
        LoadSequence.AppendInterval(paperBGBounceUpWaitTime);
        LoadSequence.Append(paperBGSequence);
        LoadSequence.Append(pinSequence);
        LoadSequence.Play();
    }

    private IEnumerator PinPaper()
    {
        Sequence pinSequence = DOTween.Sequence();
        pinSequence.Append(pin.DOFade(0, pinTime).SetEase(pinOpacityCurve).From());
        pinSequence.Join(pin.GetComponent<RectTransform>().DOScale(pinScale, pinTime).SetEase(pinScaleCurve).From());
        // pinSequence.Append(mainCamera.DOShakePosition(.3f,3,20,100,true));
        yield return pinSequence.WaitForCompletion();
    }
}
