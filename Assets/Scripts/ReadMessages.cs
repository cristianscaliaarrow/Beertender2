﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ReadMessages : MonoBehaviour {

    public GameObject prefabMessage;
    public Transform panelLayout;
    
    public Text PopUpPendingMessage;
    string pendingPath;
    string readedPath;

    public GameObject popUpDescriptionMessage;

    public List<Message> serverMessages;

    List<Message> pendingMessages = new List<Message>();
    LinkedList<Message> readedMessages = new LinkedList<Message>();

    private void Start()
    {
         pendingPath = Application.persistentDataPath + "/pending.txt";
         readedPath = Application.persistentDataPath + "/readed.txt";

        if (File.Exists(pendingPath))
        {
            string pendingStr = File.ReadAllText(pendingPath);
            pendingMessages = JsonParser<List<Message>>.GetObject(pendingStr);
        }

        if (File.Exists(readedPath))
        {
            string readedStr = File.ReadAllText(readedPath);
            List<Message> readed = JsonParser<List<Message>>.GetObject(readedStr);
            readedMessages.Clear();
            foreach (var item in readed)
                readedMessages.AddLast(item);
        }

        UpdatePopUpPendingMessages();
    }


    public void UpdatePopUpPendingMessages()
    {
        PopUpPendingMessage.text = pendingMessages.Count + "";
        PopUpPendingMessage.transform.parent.gameObject.SetActive(PopUpPendingMessage.text != "0");
    }

    private void OnEnable()
    {
        ReadMessageFromServer();
        CreateMessages();
    }

    private void ReadMessageFromServer()
    {
        bool debug = true;
        
        if (!debug)
        {
            PhpQuery.GetMessages((str) => 
            {
                serverMessages = JsonParser<List<Message>>.GetObject(str);
                serverMessages = FilterMessages(serverMessages);
                foreach (var item in serverMessages)
                    pendingMessages.Add(item);
                CreateMessages();
                UpdatePopUpPendingMessages();
            });
        }
        else
        {
            serverMessages = FilterMessages(serverMessages);
            foreach (var item in serverMessages)
                pendingMessages.Add(item);
            CreateMessages();
            UpdatePopUpPendingMessages();
        } 

    }

    private void OnDestroy()
    {
        SaveReadedMessagesInFile();
        SavePendingMessagesInFile();
    } 

    private List<Message> FilterMessages(List<Message> serverMessages)
    {
        List<Message> msg = new List<Message>();
        foreach (var item in serverMessages)
        {
            if (!readedMessages.Contains(item) && !pendingMessages.Contains(item))
            {
                print("ADDED");
                msg.Add(item);
            }
            else
                print("FAIL!");
        }
        return msg;
    }

    private void OnDisable()
    {
        
    }



    private void CreateMessages()
    {
        foreach (Transform item in panelLayout)
        {
            Destroy(item.gameObject);
        }
       

        foreach (var item in pendingMessages)
        {
            GameObject go = Instantiate(prefabMessage);
            go.GetComponent<Text>().text = item.title;
            go.transform.SetParent(panelLayout);
            go.transform.localScale = Vector3.one;
            go.GetComponent<Button>().onClick.AddListener(() => MoveToReaded(item));
            go.GetComponent<Button>().onClick.AddListener(() => OnClickMe(item));
        }

        foreach (var item in readedMessages)
        {
            GameObject go = Instantiate(prefabMessage);
            go.GetComponent<Text>().text = item.title;
            go.GetComponent<Text>().color = Color.gray;
            go.GetComponent<Text>().fontSize = (int)(go.GetComponent<Text>().fontSize* 0.80f);
            go.transform.SetParent(panelLayout);
            go.transform.localScale = Vector3.one;
            go.GetComponent<Button>().onClick.AddListener(() => OnClickMe(item));
        }
    }

    private void MoveToReaded(Message messg)
    {
        pendingMessages.Remove(messg);
        readedMessages.AddFirst(messg);
    }

    private void OnClickMe(Message messg)
    {
        CreateMessages();
        ShowPopUpMessageDescription(messg.title, messg.message);
        UpdatePopUpPendingMessages();
    }

    public void BTN_ShowPopupMessage(bool visible)
    {
        popUpDescriptionMessage.SetActive(visible);
    }

    public void ShowPopUpMessageDescription(string title, string description)
    {
        BTN_ShowPopupMessage(true);
        GameObject.Find(popUpDescriptionMessage.name + "/Title").GetComponent<Text>().text = title;
        GameObject.Find(popUpDescriptionMessage.name + "/Description").GetComponent<Text>().text = description;
    }

    private void SaveReadedMessagesInFile()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("{\"data\":[");
        foreach (var item in readedMessages)
        {
            sb.AppendLine("{");
            sb.AppendLine("\"id\":\"" + item.id + "\",");
            sb.AppendLine("\"title\":\"" + item.title + "\",");
            sb.AppendLine("\"message\":\"" + item.message + "\"");
            sb.AppendLine("}");
            if (item != readedMessages.Last.Value)
                sb.AppendLine(",");
        }
        sb.AppendLine("]}");
        File.WriteAllText(Application.persistentDataPath + "/readed.txt", sb.ToString());
    }

    private void SavePendingMessagesInFile()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("{\"data\":[");
        foreach (var item in pendingMessages)
        {
            sb.AppendLine("{");
            sb.AppendLine("\"id\":\"" + item.id + "\",");
            sb.AppendLine("\"title\":\"" + item.title + "\",");
            sb.AppendLine("\"message\":\"" + item.message + "\"");
            sb.AppendLine("}");
            if (item != pendingMessages[pendingMessages.Count - 1])
                sb.AppendLine(",");
        }
        sb.AppendLine("]}");
        File.WriteAllText(Application.persistentDataPath + "/pending.txt", sb.ToString());
    }

}


[System.Serializable]
public class Message
{
    public int id;
    public string title;
    public string message;
    public string date;

    public override bool Equals(object obj)
    {
        return this.id == ((Message)obj).id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

