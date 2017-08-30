using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignalManager : MonoBehaviour {

    public string str;

    void Start()
    {
        OneSignal.StartInit("fa5c60e2-ee41-48fa-8835-a84b37b3dec5").HandleNotificationOpened(HandleNotificationOpened).HandleNotificationReceived(HandleNotificationReceived).EndInit();
    }

    private void HandleNotificationOpened(OSNotificationOpenedResult result)
    {

    }

   
    private void HandleNotificationReceived(OSNotification notification)
    {
        OSNotificationPayload payload = notification.payload;

        string message = payload.body;

        str = ("GameControllerExample:HandleNotificationReceived: " + message + "\n");
        str += ("displayType: " + notification.displayType + "\n");
        str += "Notification received with text: " + message + "\n";

        /* Dictionary<string, object> additionalData = payload.additionalData;
         if (additionalData == null)
             Debug.Log("[HandleNotificationReceived] Additional Data == null");
         else
             Debug.Log("[HandleNotificationReceived] message " + message + ", additionalData: " + Json.Serialize(additionalData) as string);*/
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), str);
    }
}
