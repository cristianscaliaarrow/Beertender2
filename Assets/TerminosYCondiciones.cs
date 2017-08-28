using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TerminosYCondiciones : MonoBehaviour {
    public Toggle toggle;
    public GameObject PanelTerminos;
    public void BTN_AceptarTerminos()
    {
        if (toggle.isOn)
        {
            PanelTerminos.SetActive(false);
            File.WriteAllText(Application.persistentDataPath + "/FirstInit.txt", "acepted");
        }
    }

}
