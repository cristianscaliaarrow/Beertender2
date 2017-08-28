using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TerminosYCondiciones : MonoBehaviour {
    public static TerminosYCondiciones instance;
    public Toggle toggle;
    public GameObject PanelTerminos;
    public GameObject buttonAccept;
    public Text textTOS;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
        buttonAccept.SetActive(false);
        PhpQuery.GetTOS(OnTos);
    }

    TOS tos;
    private void OnTos(string obj)
    {
        tos = JsonParser<TOS>.GetObject(obj);
        textTOS.text = tos.value;
        buttonAccept.SetActive(true);
        print(tos.updated +"!="+ PlayerPrefs.GetString("tos"));
        if(tos.updated != PlayerPrefs.GetString("tos"))
        {
            gameObject.SetActive(true);
        }
    }

    public void BTN_AceptarTerminos()
    {
        if (toggle.isOn)
        {
            PanelTerminos.SetActive(false);
            PlayerPrefs.SetString("tos",tos.updated);
        }
    }

   
}
