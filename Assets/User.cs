using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    public static User instance;
    public Rol rol;

    private void Awake()
    {
        instance = this;
    }

}
