using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseButtons : MonoBehaviour
{
    public void OnClick()
    {
        LevelLoadTransitions transitioner = FindObjectOfType<LevelLoadTransitions>();
        transitioner.LoadTitleScene();
    }
}
