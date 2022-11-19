using System.Collections.Generic;

public static class UserInfo
{
    private static readonly Dictionary<string, string> Users = new();

    public static bool HasUser(string username, string password)
    {
        return Users.TryGetValue(username, out var value) && value.Equals(password);
    }

    public static bool InsertUser(string username, string password)
    {
        return Users.TryAdd(username, password);
    }

}