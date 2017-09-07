using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMM : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Login>().txtUser.text = "stafftest@gmail.com";
        GetComponent<Login>().txtPassword.text = "33333333";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
