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
}
