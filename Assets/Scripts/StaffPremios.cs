using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StaffPremios : MonoBehaviour {
    public GameObject prefabPrize;
    public List<PrizeStaff> premios;
    public GameObject panelLayout;

    public GameObject popUpCanjear;

    private void Start()
    {
        PhpQuery.GetPrizeStaff(LoadPrices);
    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        
    }

    private void LoadPrices(string result)
    {
        premios = JsonParser<List<PrizeStaff>>.GetObject(result);
        int i = 0;
      
        foreach (var item in premios)
        {
            int id = item.id;
            GameObject go = Instantiate(prefabPrize);
            go.name = "PrizeStaff (" + i++ + ")";
            go.transform.SetParent(panelLayout.transform);
            go.transform.localScale = Vector3.one;
            GameObject.Find(go.name + "/txtTitle").GetComponent<Text>().text = item.name;
            // GameObject.Find(go.name + "/txtPuntos").GetComponent<Text>().text = item.pts_cost + " PTOS.";
            if (Login.debugUser.role_id == (int)Rol.MANAGER && !CanManagerChange())
                GameObject.Find(go.name + "/Button").SetActive(false);
            else
            {
                GameObject goEvent = GameObject.Find(go.name + "/Button");

                EventTrigger.Entry onEntry = new EventTrigger.Entry();
                onEntry.eventID = EventTriggerType.PointerClick;
                onEntry.callback.RemoveAllListeners();
                onEntry.callback.AddListener((eventData) => { Click(item); });
                goEvent.AddComponent<EventTrigger>().triggers.Add(onEntry);
            }
        }
    }

    private void Click(PrizeStaff prize)
    {
        PopupChangePrize.prize = prize;
        popUpCanjear.SetActive(true);
    }


    private bool CanManagerChange()
    {
        Debug.LogError("Chequear con el servidor! Manager Data Change Prize");
        return true;
    }

    private void OnPressedChange(PrizeStaff prize)
    {
        popUpCanjear.SetActive(true);
        PopupChangePrize.prize = prize;
    }

    
}
