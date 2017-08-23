using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class PhpQuery : MonoBehaviour {
    public static PhpQuery instance;

    void Awake()
    {
        instance = this;
    }

    #region "Users"
    public static void GetUsers(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/users", callBack));
    }

    public static void GetUser(int id , Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/users/" + id, callBack));
    }
    #endregion

    #region "Shops"
    public static void GetShops(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/shops", callBack));
    }

    public static void GetShop(int id, Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/shops/" + id, callBack));
    }

    #endregion

    public static void EditUser(int id)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/users/" + id, "{\"data\": {\"firstName\":\"cambio\"}}"));
    }

    private static IEnumerator StartQuery(string query,Action<string> callBack)
    {
        var headers = new Dictionary<string,string>();
        headers.Add("Authorization", "Bearer "+User.authorization);


        UnityWebRequest webRequest = UnityWebRequest.Get(query);
        yield return webRequest.Send();

        if (!webRequest.isError)
        {
            callBack(webRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log(webRequest.error);
        }

        /* WWW www = new WWW(query,null,headers);

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

         */
    }

    private static IEnumerator StartQuery(string query, string body)
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer " + User.authorization);

        UnityWebRequest webRequest;
        byte[] bytes = Encoding.UTF8.GetBytes(body);
        webRequest = UnityWebRequest.Put(query, bytes);
        webRequest.SetRequestHeader("X-HTTP-Method-Override", "PUT");

        yield return webRequest.Send();

        if (!webRequest.isError)
        {
            Debug.Log("Upload complete!");
        }
        else
        {
            Debug.Log(webRequest.error);
        }

    }
}
