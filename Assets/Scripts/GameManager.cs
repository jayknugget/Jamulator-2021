﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    // money
    private float _totalMoney;
    private float _dailyMoney;
    public float TotalMoney { get => _totalMoney; }
    public float DailyMoney { get => _dailyMoney; }

    // day counter
    private int _currentDay;
    [SerializeField] private int _totalDays;
    // rent per day
    [SerializeField] private int[] _rents;
    // gravity per day
    [SerializeField] private float[] _gravity;

    public int TodayRent { get => _rents[_currentDay]; }

    public Text MoneyText;
    public DayTimer Timer;
    public float DayLengthSeconds;

    // audio
    public AudioSource source;
    public AudioClip MenuMusic, GameMusic;

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

        DontDestroyOnLoad( _instance );
        // Instance.Reset();
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
        // StartDay();
    }

    public void EndDay()
    {
        _totalMoney += _dailyMoney;

        SceneManager.LoadScene( "EndOfDay" );
    }

    public void StartDay()
    {
        Timer.SetDayLength( DayLengthSeconds );

        // Physics.gravity = new Vector3( 0.0f, -_gravity[_currentDay], 0.0f );
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

    public void DeductRent()
    {
        _totalMoney -= _rents[_currentDay];
    }

    public void StartNextDay()
    {
        LevelLoadTransitions transitioner = FindObjectOfType<LevelLoadTransitions>();
        if( _totalMoney <= 0.0f )
        {
            // LOSER
            Debug.Log( "LOSER" );
            transitioner.Lose();
            // SceneManager.LoadScene( "Lose" );
            return;
        }

        _currentDay++;

        if( _currentDay == _totalDays )
        {
            // WINNER
            Debug.Log( "WINNER" );
            transitioner.Win();
            // SceneManager.LoadScene( "Win" );
            return;
        }
        transitioner.LoadPlayScene();
        // SceneManager.LoadScene( "JakeScene" );
    }

    public void PlayMenuMusic()
    {
        source.clip = MenuMusic;
        source.Play();
    }

    public void PlayGameMusic()
    {
        source.clip = GameMusic;
        source.Play();
    }

    public void StopCurrentMusic()
    {
        source.Stop();
    }
}
