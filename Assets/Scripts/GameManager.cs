using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        StartDay();
    }

    public void EndDay()
    {
        _totalMoney += _dailyMoney;
        _totalMoney -= _rents[_currentDay];
        if( _totalMoney <= 0.0f )
        {
            // LOSER
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
        }

        Physics.gravity = new Vector3( 0.0f, -_gravity[_currentDay], 0.0f );
        _dailyMoney = 0.0f;

        // Update daily UI stuff
    }

    public void AddMoney( float money )
    {
        _dailyMoney += money;
        // update UI
    }
}
