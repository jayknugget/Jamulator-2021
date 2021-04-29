using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{
    public float totalLevelTimeSeconds = 120;
    public Text currentLevelTimeText;

    private void Awake()
    {
        currentLevelTimeText.text = "0:00";
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
            currentLevelTimeText.text = "00:00";
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
            return;
        currentLevelTimeText.text = Mathf.Floor(totalLevelTimeSeconds / 60).ToString("00")
            + ":" + Mathf.FloorToInt(totalLevelTimeSeconds % 60).ToString("00");
    }
}
