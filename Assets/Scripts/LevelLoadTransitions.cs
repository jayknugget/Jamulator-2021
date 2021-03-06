using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadTransitions : MonoBehaviour
{
    [SerializeField] private Animator howToPlayTransition;
    [SerializeField] private float howToPlayTransitionTime = 1f;

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
        GameManager.Instance.PlayMenuMusic();
    }

    public void LoadPlayScene()
    {
        StartCoroutine(LoadPlay());
    }

    private IEnumerator LoadPlay()
    {
        GameManager.Instance.StopCurrentMusic();
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("JakeScene");
        GameManager.Instance.PlayGameMusic();
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
        print("Switching to Lose");
        SceneManager.LoadScene(4);//LOSE
    }

    public void EndOfDay()
    {
        StartCoroutine(LoadEndOfDay());
    }

    private IEnumerator LoadEndOfDay()
    {
        GameManager.Instance.StopCurrentMusic();
        howToPlayTransition.SetTrigger("Start");
        yield return new WaitForSeconds(howToPlayTransitionTime);
        SceneManager.LoadScene("EndOfDay");
    }
}
