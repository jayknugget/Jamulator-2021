using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPenalties : MonoBehaviour
{
    OrderGenerator orderGenerator;
    public Image[] penaltyIcons;

    private void Awake()
    {
        orderGenerator = FindObjectOfType<OrderGenerator>();
        for (int i = 0; i < penaltyIcons.Length; i++)
        {
            penaltyIcons[i].gameObject.SetActive(false);
        }
    }

    public void UpdatePenaltyIconUI()
    {
        for (int i = 0; i < penaltyIcons.Length; i++)
        {
            if(orderGenerator.currentPlayerPenalties > i)
            {
                penaltyIcons[i].gameObject.SetActive(true);
            }
            else
            {
                penaltyIcons[i].gameObject.SetActive(false);
            }
        }
    }
}
