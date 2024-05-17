using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Home_Haven.Models
{
    public static class SessionExtensions
    {
        // Extension method to set an object as JSON in session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            session.SetString(key, json);
        }

        // Extension method to get an object from JSON in session
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string json = session.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
