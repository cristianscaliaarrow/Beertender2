using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffPremios : MonoBehaviour {
    public GameObject prefabPrize;
    public List<String> premios;
    public GameObject panelLayout;

    private void OnEnable()
    {
        LoadPrices();
    }

    private void OnDisable()
    {
        foreach (Transform item in panelLayout.transform)
        {
            Destroy(item.gameObject);
        }
    }

    private void LoadPrices()
    {
        foreach (var item in premios)
        {
            GameObject go = Instantiate(prefabPrize);
            go.transform.SetParent(panelLayout.transform);
            go.transform.localScale = Vector3.one;
        }
    }
}
