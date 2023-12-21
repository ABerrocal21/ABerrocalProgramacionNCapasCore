using System.Text.Json;

namespace PL
{
    public static class SessionExtension
    {
        public static void Set<List>(this ISession session, string key, List<object> list)
        {
            session.SetString(key, JsonSerializer.Serialize(list));
        }

        public static List<ML.VentaProducto>? Get<List>(this ISession session, string key)
        {
            var value = session.Get(key);
            return value == null ? default : JsonSerializer.Deserialize<List<ML.VentaProducto>>(value);
        }

    }
}
