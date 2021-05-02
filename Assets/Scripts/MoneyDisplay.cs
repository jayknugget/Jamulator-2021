using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public Text MoneyDisplayText;

    void Awake()
    {
        GameManager.Instance.MoneyText = MoneyDisplayText;
    }
}
