using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

    public InputField txtUser;
    public InputField txtPassword;

    public Text nameTopRigth;
    public Text premiosStaffPuntos;

    public Text puntosOwnerManager;
    public Text puntosStaff;
    public Text registrosStaff;


    public static User debugUser;
    public static Login instance;


	void Start () {
        instance = this;
	}

    public void BTN_Ingresar()
    {
        PhpQuery.GetUser(int.Parse(txtUser.text), Algo);
    }

    private void Algo(string result)
    {
        User u = JsonUtility.FromJson<JsonParser<User>>(result).data;
        debugUser = u;
        PhpQuery.GetShop(u.shop_id, OnGetShopInfo);
    }

    public static void UpdateGUI() {
        instance.nameTopRigth.text = debugUser.firstName.ToUpper() + " " + debugUser.lastName.ToUpper();
        instance.premiosStaffPuntos.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
        instance.puntosOwnerManager.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
        instance.puntosStaff.text = "TENÉS <size=55> " + debugUser.total_pts + " </size> PUNTOS";
        instance.registrosStaff.text = "HICISTE <size=55> " + 50 + " </size> REGISTROS";
    }


    private void OnGetShopInfo(string result)
    {
        try
        {
            Shop u = JsonUtility.FromJson<JsonParser<Shop>>(result).data;
            Home.instance.gameObject.SetActive(true);
            Home.instance.BTN_ShowHome();
            gameObject.SetActive(false);
            UpdateGUI();
        }
        catch
        {
        }

       
    }
}
