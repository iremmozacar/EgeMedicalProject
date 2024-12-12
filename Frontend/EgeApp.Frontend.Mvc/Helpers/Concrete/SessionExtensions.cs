using System;

namespace EgeApp.Frontend.Mvc.Helpers.Concrete;

public static class SessionExtensions
{
    public static void SetString(this ISession session, string key, string value)
    {
        session.SetString(key, value);
    }

    public static string GetString(this ISession session, string key)
    {
        return session.GetString(key);
    }
}
