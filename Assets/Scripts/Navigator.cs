﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Navigator : MonoBehaviour {

    public ToggleButton ranking;
    public ToggleButton premios;
    public ToggleButton beertender;
    public ToggleButton contacto;

    public GameObject panelRanking;
    public GameObject panelPremios;
    public GameObject panelBeertender;
    public GameObject panelContacto;
    public GameObject panelHome;


    public void TurnOffAll()
    {
        ranking.Toggle(false);
        premios.Toggle(false);
        beertender.Toggle(false);
        contacto.Toggle(false);
        panelRanking.SetActive(false);
        panelPremios.SetActive(false);
        panelBeertender.SetActive(false);
        panelContacto.SetActive(false);
        panelHome.SetActive(false);
    }

    public void BTN_Ranking()
    {
        ranking.Toggle(true);
        panelRanking.SetActive(true);
        
    }

    public void BTN_Premios()
    {
        premios.Toggle(true);
        panelPremios.SetActive(true);
    }

    public void BTN_Beertender()
    {
        beertender.Toggle(true);
        panelBeertender.SetActive(true);
    }

    public void BTN_Contacto()
    {
        contacto.Toggle(true);
        panelContacto.SetActive(true);
    }

    public void BTN_Home()
    {
        panelHome.SetActive(true);
    }


}

[System.Serializable]
public class ToggleButton
{
    public Button button;
    public Sprite on;
    public Sprite off;

    public void Toggle(bool active)
    {
        button.image.sprite = (active) ? on : off;
    }

}