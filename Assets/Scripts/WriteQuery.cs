using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteQuery : MonoBehaviour {

    public InputField nombre;
    public InputField correo;
    public InputField tema;
    public InputField message;

    public void ClearFields()
    {
        nombre.text = "";
        correo.text = "";
        tema.text = "";
        message.text = "";
    }

    public void BTN_Enviar()
    {
        PhpQuery.SendContact(nombre,correo,tema,message);
        ClearFields();
    }
}

