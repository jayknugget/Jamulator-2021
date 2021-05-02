using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuButtonTweening : MonoBehaviour
{
    [SerializeField][Range(0,2)] private float ButtonHoverScale;
    [SerializeField][Range(0,2)] private float ButtonHoverScaleTime;
    [SerializeField][Range(0,2)] private float ButtonDropdownTime;
    [SerializeField][Range(0,2)] private float ButtonDropdownWaitTime;
    [SerializeField][Range(0,2)] private float TimeGapBetweenEachDrop;
    [SerializeField] private AnimationCurve ButtonDropdownCurve;
    [SerializeField][Range(0,1)] private float TitleY;
    [SerializeField][Range(0,1)] private float PlayButtonY;
    [SerializeField][Range(0,1)] private float HowToPlayButtonY;
    [SerializeField][Range(0,1)] private float QuitButtonY;
    [SerializeField] private AnimationCurve ButtonHoverScaleCurve;
    [SerializeField] private RectTransform Title;
    [SerializeField] private RectTransform PlayButton;
    [SerializeField] private RectTransform HowToPlayButton;
    [SerializeField] private RectTransform QuitButton;
    void Start()
    {
        StartCoroutine(LoadInMenuSequence());
    }

    void Update()
    {
        
    }

    public void OnHoverEnterTween(Button button)
    {
        button.GetComponent<RectTransform>().DOScale(ButtonHoverScale,ButtonHoverScaleTime).SetEase(ButtonHoverScaleCurve);
        print(button.transform.position);
        // print("hover enter " + button.GetComponentInChildren<Text>().text);
    }

    public void OnHoverExitTween(Button button)
    {
        button.GetComponent<RectTransform>().DOScale(1,ButtonHoverScaleTime).SetEase(ButtonHoverScaleCurve);
        // print("hover exit " + button.GetComponentInChildren<Text>().text);
    }

    private IEnumerator LoadInMenuSequence()
    {
        Sequence QuitButtonSequence = DOTween.Sequence();
        QuitButtonSequence.AppendInterval(TimeGapBetweenEachDrop);
        QuitButtonSequence.Append(QuitButton.DOMoveY(Screen.height * QuitButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve));

        Sequence HowToPlayButtonSequence = DOTween.Sequence();
        HowToPlayButtonSequence.AppendInterval(TimeGapBetweenEachDrop * 2);
        HowToPlayButtonSequence.Append(HowToPlayButton.DOMoveY(Screen.height * HowToPlayButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve));
        
        Sequence PlayButtonSequence = DOTween.Sequence();
        PlayButtonSequence.AppendInterval(TimeGapBetweenEachDrop * 3);
        PlayButtonSequence.Append(PlayButton.DOMoveY(Screen.height * PlayButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve));
        
        Sequence TitleSequence = DOTween.Sequence();
        TitleSequence.AppendInterval(TimeGapBetweenEachDrop * 4);
        TitleSequence.Append(Title.DOMoveY(Screen.height * TitleY, ButtonDropdownTime).SetEase(ButtonDropdownCurve));    
        
        Sequence LoadSequence = DOTween.Sequence();
        LoadSequence.AppendInterval(ButtonDropdownWaitTime);
        LoadSequence.Append(QuitButtonSequence);
        LoadSequence.Join(HowToPlayButtonSequence);
        LoadSequence.Join(PlayButtonSequence);
        LoadSequence.Join(TitleSequence);
        LoadSequence.Play();
        yield return null;

        // Tween QuitTween = QuitButton.DOMoveY(Screen.height * QuitButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
        // Tween HowToPlayTween = HowToPlayButton.DOMoveY(Screen.height * HowToPlayButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
        // Tween PlayTween = PlayButton.DOMoveY(Screen.height * PlayButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
        // WaitForSeconds betweenWait = new WaitForSeconds(TimeGapBetweenEachDrop);
        
        // yield return new WaitForSeconds(ButtonDropdownWaitTime);
        // QuitButton.DOMoveY(Screen.height * QuitButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
        // // QuitTween.Play();
        // yield return betweenWait;
        // HowToPlayButton.DOMoveY(Screen.height * HowToPlayButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
        // // HowToPlayTween.Play();
        // yield return betweenWait;
        // PlayButton.DOMoveY(Screen.height * PlayButtonY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
        // // PlayTween.Play();
        // yield return betweenWait;
        // Title.DOMoveY(Screen.height * TitleY, ButtonDropdownTime).SetEase(ButtonDropdownCurve);
    }
}
