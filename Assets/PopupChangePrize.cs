﻿using UnityEngine;
using UnityEngine.UI;

public class PopupChangePrize : MonoBehaviour {
    public static PrizeStaff prize;
    public InputField input;
    public Text textPts;

    public void OnEnable()
    {
        textPts.text = Login.debugUser.total_pts+"";    
    }

    public void BTN_ChangePrize()
    {
        if (Login.debugUser.total_pts >= int.Parse(input.text)) 
        {
            print(" CONSUMIR EL ITEM ID:" + prize.id);
            Login.debugUser.total_pts -= int.Parse(input.text);
            Login.UpdateGUI();
            BTN_ClosePopUp();
        }
        else
        {
            BTN_ClosePopUp();
            Debug.LogError("no tenes puntos");
        }

       
    }

    public void BTN_ClosePopUp()
    {
        gameObject.SetActive(false);
    }

}
