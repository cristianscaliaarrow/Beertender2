using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TerminosYCondiciones : MonoBehaviour {
    public static TerminosYCondiciones instance;
    public Toggle toggle;
    public GameObject PanelTerminos;
    public GameObject buttonAccept;
    public Text textTOS1;
    public Text textTOS2;

    public ScrollRect scroll;

    private void Start()
    {
        PlayerPrefs.SetString("tos", "");
        instance = this;
        gameObject.SetActive(false);
        buttonAccept.SetActive(false);
        PhpQuery.GetTOS(OnTos);
    }

    TOS tos;
    private void OnTos(string obj)
    {
        tos = JsonParser<TOS>.GetObject(obj);
        int halfIndex = tos.value.Length / 2;
        textTOS1.text = tos.value.Substring(0, halfIndex);
        textTOS2.text = tos.value.Substring(halfIndex);
        /*SceneView.RepaintAll();
        HandleUtility.Repaint();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(textTOS1.transform.parent.GetComponent<VerticalLayoutGroup>());
            EditorUtility.SetDirty(textTOS1);
            EditorUtility.SetDirty(textTOS2);

        }*/
        if (tos.updated != PlayerPrefs.GetString("tos"))
        {
            gameObject.SetActive(true);
        }
        StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return 1f;
        textTOS1.transform.parent.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();

    }

    public void BTN_AceptarTerminos()
    {
        if (toggle.isOn)
        {
            PanelTerminos.SetActive(false);
            PlayerPrefs.SetString("tos",tos.updated);
        }
    }

    public void OnValueChange()
    {
        print(scroll.verticalScrollbar.value);
    }
   
}
