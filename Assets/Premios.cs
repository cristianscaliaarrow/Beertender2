using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Premios : MonoBehaviour {

    public GameObject panelStaff;
    public GameObject panelManager;
    public GameObject panelOwner;

    private void OnDisable()
    {
        panelStaff.SetActive(false);
        panelManager.SetActive(false);
        panelOwner.SetActive(false);

    }

    public void BTN_ShowPremios()
    {
        switch (User.instance.rol)
        {
            case Rol.STAFF:
                panelStaff.SetActive(true);
                break;
            case Rol.MANAGER:
                panelManager.SetActive(true);
                break;
            case Rol.OWNER:
                panelOwner.SetActive(true);
                break;
        }
    }
}

[System.Serializable]
public enum Rol
{
    STAFF,MANAGER,OWNER
}