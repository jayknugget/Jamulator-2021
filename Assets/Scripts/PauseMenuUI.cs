using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject PauseMenu;

    void Awake()
    {
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.P ) )
        {
            if( PauseMenu.activeSelf )
            {
                OnClickResume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1.0f;
        // SceneManager.LoadScene( "TitleScreen" );
    }

    public void OnClickResume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
