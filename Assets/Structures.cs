
using UnityEngine;

[System.Serializable]
public class JsonParser<T>
{
    public T data;

    public static T GetObject(string str)
    {
        return JsonUtility.FromJson<JsonParser<T>>(str).data;
    } 
}


[System.Serializable]
public class User
{
    public static User instance;
    public static int authorization;
    public int id;
    public string firstName;
    public string lastName;
    public int dni;
    public string email;
    public string phone;
    public int total_pts;
    public int total_used_pts;
    public int shop_id;
    public int role_id;
    public string created;
    public string updated;

    public int rol { get { return role_id; } }

    public User()
    {
        instance = this;
    }
}

[System.Serializable]
public class ErrorMsg
{
    public Error error;
}

[System.Serializable]
public class Error
{
    public int code;
    public int long_code;
    public string message;
    public string developerMessage;
    public string moreInfo; 
}

[System.Serializable]
public class PrizeStaff
{
    public int id;
    public string description;
    public string name;
    public string img_url;
    public int pts_cost;
    public int catagoloID;
    public int SKU;
    public int enabled;
    public int stock;
}

[System.Serializable]
public enum Rol
{
    OWNER = 1, MANAGER, STAFF
}

[System.Serializable]
public class Shop
{
    public int id;
    public string name;
    public int idPM;
    public string img_url;
    public int objVolumen;
    public int objDisponibilidad;
    public int total_pts;
}

