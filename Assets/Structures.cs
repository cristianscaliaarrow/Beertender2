
[System.Serializable]
public class JsonParser<T>
{
    public T data;
}


[System.Serializable]
public class User
{
    public static User instance;
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

public class UserList
{

}

