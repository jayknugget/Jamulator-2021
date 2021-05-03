using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void OnClickPlay()
    {
        GameManager.Instance.Reset();
        // SceneManager.LoadScene( "JakeScene" );
    }

    public void OnClickHowToPlay()
    {
        // SceneManager.LoadScene( "HowToPlay" );
    }

    public void OnClickQuit()
    {
        // Application.Quit();
    }
}
