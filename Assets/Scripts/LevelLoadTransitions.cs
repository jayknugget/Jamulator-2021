using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTransitions : MonoBehaviour
{
    [SerializeField] private Animator howToPlayTransition;
    [SerializeField] private float howToPlayTransitionTime = 1f;
    void Update()
    {
     
    }

    public void LoadHowToPlayScene()
    {
        StartCoroutine(LoadHowToPlay());
    }

    private IEnumerator LoadHowToPlay()
    {
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("HowToPlay");
    }
    public void LoadTitleScene()
    {
        StartCoroutine(LoadTitle());
    }

    private IEnumerator LoadTitle()
    {
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadPlayScene()
    {
        StartCoroutine(LoadPlay());
    }

    private IEnumerator LoadPlay()
    {
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("JakeScene");
    }

    public void Quit()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        Application.Quit();
    }

    public void Win()
    {
        StartCoroutine(LoadWin());
    }

    private IEnumerator LoadWin()
    {
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("Win");
    }

    public void Lose()
    {
        StartCoroutine(LoadLose());
    }

    private IEnumerator LoadLose()
    {
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("Lose");
    }
}
