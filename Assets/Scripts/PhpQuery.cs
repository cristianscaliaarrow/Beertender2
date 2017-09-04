using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhpQuery : MonoBehaviour {
    public static PhpQuery instance;

    void Awake()
    {
        instance = this;
    }

    public static void GetPrizeStaff(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/prizestaff/",callBack));
    }

    internal static void GetTOS(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/params/4", callBack));
    }

    internal static void GetHash(string username,string hash,int first,Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/params/4", callBack));
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

    internal static void GetMessages(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/messages/", callBack));
    }

    public static void AddUser()
    {
        //instance.StartCoroutine(StartQuery("api.nextcode.ml/users", ""));

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.nextcode.ml/users");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        httpWebRequest.Headers.Add("Authentication", "Bearer "+User.authorization);

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"data\": {\"dni\": \"12345678\",\"email\": \"12345678t@email.com\",\"shop_id\": \"9\",\"role_id\": \"2\"}}";
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }


    }

    public static void SendContact(string nombre, string correo, string tema, string message)
    {
        
    }

    public static void GetRanking(Action<string> callBack)
    {
        instance.StartCoroutine(StartQuery("http://api.nextcode.ml/rankings/shop/", callBack));
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



    public static void SendQueryContacto(string body)
    {
        //instance.StartCoroutine(StartQuery("api.nextcode.ml/users", ""));

        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.nextcode.ml/msjs");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        httpWebRequest.Headers.Add("Authentication", "Bearer " + User.authorization);

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = body;
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
        }

        print("TERMINE! SendQueryContacto");

    }
}
