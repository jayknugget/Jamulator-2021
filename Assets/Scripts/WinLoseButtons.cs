using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseButtons : MonoBehaviour
{
    public AudioSource source;
    public AudioClip hover, click;

    public void OnClick()
    {
        LevelLoadTransitions transitioner = FindObjectOfType<LevelLoadTransitions>();
        transitioner.LoadTitleScene();
    }

    public void OnHoverSFX()
    {
        source.PlayOneShot(hover);
    }

    public void OnClickSFX()
    {
        source.PlayOneShot(click);
    }
}
