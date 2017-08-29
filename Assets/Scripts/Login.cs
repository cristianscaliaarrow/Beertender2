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
        UpdateGUI();
    }

    public static void UpdateGUI() {
        instance.nameTopRigth.text = debugUser.firstName.ToUpper() + " " + debugUser.lastName.ToUpper();
        instance.premiosStaffPuntos.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
    }


    private void OnGetShopInfo(string result)
    {
        try
        {
            Shop u = JsonUtility.FromJson<JsonParser<Shop>>(result).data;
            Home.instance.gameObject.SetActive(true);
            Home.instance.BTN_ShowHome();
            gameObject.SetActive(false);
        }
        catch
        {
            NativePlugin.instance.ShowDialog("Error!", "El Usuario no Existe", "Aceptar", delegate { });
        }

       
    }
}
