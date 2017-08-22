using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testomg : MonoBehaviour {

	void Start () {
        PhpQuery.GetShops(OnReceive);
	}

    private void OnReceive(string result)
    {
        print(JsonParser<List<Shop>>.GetObject(result).Count);
    }
}
