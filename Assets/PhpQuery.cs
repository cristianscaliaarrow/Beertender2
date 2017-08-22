using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhpQuery : MonoBehaviour {
    public static PhpQuery instance;
    public delegate void CallBackResult(string result);

    void Awake()
    {
        instance = this;
    }

    #region "Users"
    public static void GetUsers(CallBackResult callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/users", callBack));
    }

    public static void GetUser(int id , CallBackResult callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/users/" + id, callBack));
    }
    #endregion

    #region "Shops"
    public static void GetShops(CallBackResult callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/shops", callBack));
    }

    public static void GetShop(int id, CallBackResult callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/shops/" + id, callBack));
    }

    #endregion

    private static IEnumerator StartQuery(string query,CallBackResult callBack)
    {
        var headers = new Dictionary<string,string>();
        headers.Add("Authorization", "Bearer "+User.authorization);
        WWW www = new WWW(query,null,headers);
        yield return www;
        if(www.error != null)
        {
            print("ERROR!");
        }else
        {
            if (www.text.Contains("error"))
            {
                Error e = JsonUtility.FromJson<ErrorMsg>(www.text).error;
                Debug.LogError(e.developerMessage);
            }else {
                callBack(www.text);
            }
        }
    }
}
