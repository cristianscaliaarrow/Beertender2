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

    public static void GetUsers(CallBackResult callBack)
    {
        instance.StartCoroutine(StartQuery("http://apibeer.000webhostapp.com/static/users.json", callBack));
    }

    public static void GetUser(int id , CallBackResult callBack)
    {
        instance.StartCoroutine(StartQuery("http://apibeer.000webhostapp.com/static/users/"+id+".json", callBack));
    }


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
