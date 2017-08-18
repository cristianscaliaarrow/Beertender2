using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour {

    public Image background;
    public Sprite ownerManagerBackgroundImage;
    public Sprite StaffBackgroundImage;
    public Sprite commonBackgroundImage;

    public GameObject panelStaff;
    public GameObject panelOwnerManager;

    public GameObject home;
    private void OnEnable()
    {
        home.SetActive(false);
    }

    private void OnDisable()
    {
        background.sprite = commonBackgroundImage;
        home.SetActive(true);
    }

    public void BTN_ShowHome()
    {
        switch (User.instance.rol)
        {
            case (int)Rol.STAFF:
                panelStaff.SetActive(true);
                panelOwnerManager.SetActive(false);
                background.sprite = StaffBackgroundImage;

                break;
            case (int)Rol.MANAGER:
            case (int)Rol.OWNER:
                panelStaff.SetActive(false);
                panelOwnerManager.SetActive(true);
                background.sprite = ownerManagerBackgroundImage;
                break;
        }
    }

    public void BTN_ShowHitoryPuntos()
    {
        gameObject.SetActive(false);
    }
}
