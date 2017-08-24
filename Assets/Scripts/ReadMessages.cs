using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadMessages : MonoBehaviour {
    public GameObject prefabMessage;
    public Transform panelLayout;
    public List<Message> pendingMessages;
    public LinkedList<Message> readedMessages = new LinkedList<Message>();

    private void OnEnable()
    {
        CreateMessages();
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
        print("Moving To readed " + messg.id);
        pendingMessages.Remove(messg);
        readedMessages.AddFirst(messg);
    }

    private void OnClickMe(Message messg)
    {
        print("Reading " + messg.title);
        CreateMessages();
    }
}


[System.Serializable]
public class Message
{
    public int id;
    public string title;
    public string message;
}

