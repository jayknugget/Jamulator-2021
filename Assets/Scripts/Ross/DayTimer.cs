using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{
    private float _dayLengthSeconds = 120;
    public float totalLevelTimeSeconds;
    public Text currentLevelTimeText;
    [SerializeField] private RectTransform timerArrow;

    private void Awake()
    {
        currentLevelTimeText.text = "";
        totalLevelTimeSeconds = _dayLengthSeconds;
        GameManager.Instance.Timer = this;  
    }

    private void Start() 
    {
        timerArrow.RotateAround(timerArrow.position, Vector3.forward, 72);
        GameManager.Instance.StartDay();
    }

    void Update()
    {
        TickClock();
    }

    private void TickClock()
    {
        if(totalLevelTimeSeconds <= 0)
        {
            totalLevelTimeSeconds = 0;
            return;
        }
        else
        {
            totalLevelTimeSeconds -= Time.deltaTime;
            AdjustDayTimerText();
        }

    }

    private void AdjustDayTimerText()
    {
        if (totalLevelTimeSeconds <= 0)
        {
            GameManager.Instance.EndDay();
        }
        timerArrow.RotateAround(timerArrow.position, Vector3.forward, -Time.deltaTime * (144.0f / _dayLengthSeconds));
    }

    public void SetDayLength( float timeSeconds )
    {
        _dayLengthSeconds = timeSeconds;
    }

    public void Reset()
    {
        totalLevelTimeSeconds = _dayLengthSeconds;
        AdjustDayTimerText();
    }
}
