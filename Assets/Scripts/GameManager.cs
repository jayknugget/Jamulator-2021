using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    // money
    private float _totalMoney;
    private float _dailyMoney;

    // day counter
    private int _currentDay;
    [SerializeField] private int _totalDays;
    // rent per day
    [SerializeField] private int[] _rents;
    // gravity per day
    [SerializeField] private float[] _gravity;

    public Text MoneyText;
    public DayTimer Timer;
    public float DayLengthSeconds;

    private void Awake()
    {
        if( _instance != null && _instance != this )
        {
            Destroy( gameObject );
        }
        else
        {
            _instance = this;
        }

        Instance.Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        _totalMoney = 0.0f;
        _dailyMoney = 0.0f;
        _currentDay = 0;
        Timer.SetDayLength( DayLengthSeconds );
        StartDay();
    }

    public void EndDay()
    {
        _totalMoney += _dailyMoney;
        _totalMoney -= _rents[_currentDay];
        if( _totalMoney <= 0.0f )
        {
            // LOSER
            Debug.Log( "LOSER" );
        }

        _currentDay++;
        // anything between eod and next day?
        StartDay();
    }

    public void StartDay()
    {
        if( _currentDay == _totalDays )
        {
            // WINNER
            Debug.Log( "WINNER" );
        }

        Physics.gravity = new Vector3( 0.0f, -_gravity[_currentDay], 0.0f );
        _dailyMoney = 0.0f;

        // Update daily UI stuff
        Timer.Reset();
        UpdateMoneyText();
    }

    public void AddMoney( float money )
    {
        _dailyMoney += money;
        // update UI
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = _dailyMoney.ToString("F2");
    }
}
