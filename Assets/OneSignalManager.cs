using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneSignalManager : MonoBehaviour {

    public string str;
    public GUIStyle style;
    public Text txt;
    void Start()
    {
       
        OneSignal.StartInit("fa5c60e2-ee41-48fa-8835-a84b37b3dec5").HandleNotificationOpened(HandleNotificationOpened).HandleNotificationReceived(HandleNotificationReceived).EndInit();
        string strs = PlayerPrefs.GetString("title");
        if (strs != "") str = strs;
        txt.text = strs;
        Destroy(txt.transform.parent.gameObject, 8);
        Invoke("Algo", 1);
    }

    public void Algo()
    {
        PhpQuery.SendContact();
    }
    private void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
        str = ("GameControllerExample:HandleNotificationReceived: " + result.notification.payload.title + "\n");
        PlayerPrefs.SetString("title", result.notification.payload.title);
        Debug.Log("POKE HandleNotificationOpened " + result.notification.payload.body);
    }


    private void HandleNotificationReceived(OSNotification notification)
    {
        OSNotificationPayload payload = notification.payload;

        string message = payload.body;

        str = ("GameControllerExample:HandleNotificationReceived: " + message + "\n");
        str += ("displayType: " + notification.displayType + "\n");
        str += "Notification received with text: " + message + "\n";
        Debug.Log("POKE HandleNotificationReceived " + message);
        /* Dictionary<string, object> additionalData = payload.additionalData;
         if (additionalData == null)
             Debug.Log("[HandleNotificationReceived] Additional Data == null");
         else
             Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Json.Serialize(additionalData) as string);*/
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "ASDD",style);
    }
}
