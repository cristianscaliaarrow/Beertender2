using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneSignalManager : MonoBehaviour {

   // public GUIStyle style;
    public ReadMessages readMessage;

    void Start()
    {
        OneSignal.StartInit("fa5c60e2-ee41-48fa-8835-a84b37b3dec5").HandleNotificationOpened(HandleNotificationOpened).HandleNotificationReceived(HandleNotificationReceived).EndInit();
    }

   
    private void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
        readMessage.LoadMessagesFromFile();
        Message m = new Message();
        m.title = result.notification.payload.title;
        m.message = result.notification.payload.body;
        readMessage.pendingMessages.Add(m);
        readMessage.UpdatePopUpPendingMessages();
        readMessage.SaveAllMessage();
        Debug.Log("POKE " + m.title + " " + m.message);
    }


    private void HandleNotificationReceived(OSNotification notification)
    {
        OSNotificationPayload payload = notification.payload;
        string message = payload.body;
    }

   /* private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "ASDD",style);
    }*/
}
