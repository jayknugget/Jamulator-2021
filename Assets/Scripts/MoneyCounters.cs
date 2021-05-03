using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounters : MonoBehaviour
{
    public AudioSource source;
    public AudioClip hover, click, money;

    public Text DailyText, RentText, TotalText;
    public Button ContinueButton;
    public float CountdownTime;

    void Awake()
    {
        DailyText.text = "0.00";
        RentText.text = GameManager.Instance.TodayRent.ToString("F2");
        TotalText.text = GameManager.Instance.TotalMoney.ToString("F2");
        ContinueButton.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDailyMoney());
    }

    private IEnumerator CountDailyMoney()
    {
        yield return new WaitForSeconds(2.0f);

        float currentTime = 0.0f;
        float endtime = CountdownTime;

        while( currentTime < endtime )
        {
            DailyText.text = Mathf.Lerp(0.0f, GameManager.Instance.DailyMoney, currentTime / endtime).ToString("F2");
            TotalText.text = Mathf.Lerp(GameManager.Instance.TotalMoney - GameManager.Instance.DailyMoney, GameManager.Instance.TotalMoney, currentTime / endtime).ToString("F2");
            currentTime += Time.deltaTime;
            yield return null;
        }

        DailyText.text = GameManager.Instance.DailyMoney.ToString("F2");
        TotalText.text = GameManager.Instance.TotalMoney.ToString("F2");

        source.PlayOneShot(money);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CountDownRent());
    }

    private IEnumerator CountDownRent()
    {
        float currentTime = 0.0f;
        float endtime = CountdownTime;

        while( currentTime < endtime )
        {
            RentText.text = Mathf.Lerp(GameManager.Instance.TodayRent, 0.0f, currentTime / endtime).ToString("F2");
            TotalText.text = Mathf.Lerp(GameManager.Instance.TotalMoney, GameManager.Instance.TotalMoney - GameManager.Instance.TodayRent, currentTime / endtime).ToString("F2");
            currentTime += Time.deltaTime;
            yield return null;
        }

        GameManager.Instance.DeductRent();
        RentText.text = "0.00";
        TotalText.text = GameManager.Instance.TotalMoney.ToString("F2");

        source.PlayOneShot(money);
        ContinueButton.enabled = true;
    }

    public void OnClickContinue()
    {
        GameManager.Instance.StartNextDay();
    }

    public void OnHoverSFX()
    {
        source.PlayOneShot(hover);
    }

    public void OnClickSFX()
    {
        source.PlayOneShot(click);
    }
}
