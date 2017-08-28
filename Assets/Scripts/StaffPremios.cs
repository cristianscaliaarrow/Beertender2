using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffPremios : MonoBehaviour {
    public GameObject prefabPrize;
    public List<PrizeStaff> premios;
    public GameObject panelLayout;

    private void OnEnable()
    {
        PhpQuery.GetPrizeStaff(LoadPrices);
    }

    private void OnDisable()
    {
        foreach (Transform item in panelLayout.transform)
        {
            Destroy(item.gameObject);
        }
    }

    private void LoadPrices(string result)
    {
        premios = JsonParser<List<PrizeStaff>>.GetObject(result);
        int i = 0;
      
        foreach (var item in premios)
        {
            GameObject go = Instantiate(prefabPrize);
            go.name = "PrizeStaff (" + i++ + ")";
            go.transform.SetParent(panelLayout.transform);
            go.transform.localScale = Vector3.one;
            GameObject.Find(go.name + "/txtPremio").GetComponent<Text>().text = item.name;
            GameObject.Find(go.name + "/txtPuntos").GetComponent<Text>().text = item.pts_cost + " PTOS.";
        }
    }
}
