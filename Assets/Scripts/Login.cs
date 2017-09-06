using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
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

    public Image icoUserHome;

    public Image icoUserNavigator;
    public Text txtUserNavigator;

    public InputField txtNombreWritecontacto;
    public InputField txtMailWritecontacto;
    Shop u;

    void Start () {
        instance = this;
	}

    public void BTN_Ingresar()
    {
        var jsonString = "{\"data\": {\"user\": \""+txtUser.text+"\",\"hash\": \""+SHA512(SHA512(txtPassword.text) +GetDate())+"\"}}";
        PhpQuery.SendQueryResponse(PhpQuery.url+"login", jsonString, OnResultLogin);
    }

    private void OnResultLogin(UnityWebRequest obj)
    {
        if(obj.responseCode == 200)
        {
            print(obj.downloadHandler.text);
            HashLogin hl = JsonParser<HashLogin>.GetObject(obj.downloadHandler.text);
            if (hl!=null)
            {
                print("OK");
            }
            User.authorization = hl.token;
            PhpQuery.GetUser(int.Parse(hl.user_id), Algo);
        }
        else
        {
            ErrorGet error = JsonUtility.FromJson<ErrorGet>(obj.downloadHandler.text);
            PanelResult.ShowMsg(error.error,2,nohacernada);
        }
    }

    private void nohacernada()
    {
        //throw new NotImplementedException();
    }

    private void Algo(string result)
    {
        User u = JsonUtility.FromJson<JsonParser<User>>(result).data;
        debugUser = u;
        PhpQuery.GetShop(u.shop_id, OnGetShopInfo);
    }

    public static void UpdateGUI() {

        if(debugUser.rol == (int)Rol.STAFF)
            instance.nameTopRigth.text = debugUser.firstName.ToUpper() + " " + debugUser.lastName.ToUpper();
        else
            instance.nameTopRigth.text = instance.u.name;

        string path = PlayerPrefs.GetString("imagePath-"+debugUser.id);
        if (path != "")
        {
            GameManager.instance.StartCoroutine(instance.LoadImage(path, instance.icoUserNavigator));
            GameManager.instance.StartCoroutine(instance.LoadImage(path, instance.icoUserHome));
        }

        instance.premiosStaffPuntos.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
        instance.puntosOwnerManager.text = "TENÉS <size=70> " + debugUser.total_pts + " </size> PUNTOS";
        instance.puntosStaff.text = "TENÉS <size=55> " + debugUser.total_pts + " </size> PUNTOS";
        instance.registrosStaff.text = "HICISTE <size=55> " + 50 + " </size> REGISTROS";
        instance.txtNombreWritecontacto.text = debugUser.firstName;
        instance.txtMailWritecontacto.text = debugUser.email;
    }


    private void OnGetShopInfo(string result)
    {
        try
        {
            u = JsonUtility.FromJson<JsonParser<Shop>>(result).data;
            Home.instance.gameObject.SetActive(true);
            Home.instance.BTN_ShowHome();
            gameObject.SetActive(false);
            UpdateGUI();
        }
        catch
        {
        }
    }

    private IEnumerator LoadImage(string path, Image output)
    {
        var url = "file://" + path;
        var www = new WWW(url);
        yield return www;
        yield return new WaitForSeconds(0.5f);
    
        var texture = www.texture;
        if (texture == null)
        {
            Debug.LogError("Failed to load texture url:" + url);
        }else
        {
            Debug.Log("POKE SE CARGO LA IMAGEN!");
        }

        output.transform.eulerAngles = new Vector3(0, 0, -90);
        output.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
    }


    private string GetDate()
    {
        return System.DateTime.Now.ToString("yyyy-MM-dd");
    }

    public string SHA512(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using (var hash = System.Security.Cryptography.SHA512.Create())
        {
            var hashedInputBytes = hash.ComputeHash(bytes);

            var hashedInputStringBuilder = new System.Text.StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString().ToLower();
        }
    }
}

[System.Serializable]
public class HashLogin
{
    public string user_id;
    public string token;
}

[System.Serializable]
public class ErrorGet
{
    public string error;
}