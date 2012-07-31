using System;
using System.IO;
using System.Text;
using Facebook.Json;

namespace Facebook
{
    public static class ObjectExtensions
    {
        public static String ToJson(this Object value)
        {
            var jsonBuilder = new StringBuilder();
            var serializer = new JsonSerializer();
            var writer = new JsonTextWriter(new StringWriter(jsonBuilder));
            serializer.Serialize(writer, value);
            return jsonBuilder.ToString();
        }
    }
}
