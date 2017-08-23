using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testomg : MonoBehaviour {

    public List<Shop> list;
	void Start () {
        //PhpQuery.GetShops(OnReceive);
        PhpQuery.EditUser(1);
	}

    private void OnReceive(string result)
    {
        list = JsonParser<List<Shop>>.GetObject(result);
        print(list.Count);
    }
}
