using System.Web.Script.Serialization;

namespace General.Apt.App.Extensions
{
    public static class JsonSerializeExtension
    {
        #region Newtonsoft.Json

        //private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        //{
        //    ContractResolver = new DefaultContractResolver(),
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
        //    Formatting = Formatting.Indented,
        //    DateFormatString = "yyyy-MM-dd HH:mm:ss"
        //};
        //public static string ToJson(this object obj)
        //{
        //    return JsonConvert.SerializeObject(obj, _serializerSettings);
        //}
        //public static string ToJson(this object obj, JsonSerializerSettings serializerSettings)
        //{
        //    return JsonConvert.SerializeObject(obj, serializerSettings);
        //}
        //public static T JsonTo<T>(this string Json)
        //{
        //    return JsonConvert.DeserializeObject<T>(Json, _serializerSettings);
        //}
        //public static T JsonTo<T>(this string Json, JsonSerializerSettings serializerSettings)
        //{
        //    return JsonConvert.DeserializeObject<T>(Json, serializerSettings);
        //}
        //public static object JsonTo(this string Json)
        //{
        //    return JsonConvert.DeserializeObject(Json, _serializerSettings);
        //}
        //public static object JsonTo(this string Json, JsonSerializerSettings serializerSettings)
        //{
        //    return JsonConvert.DeserializeObject(Json, serializerSettings);
        //}
        //public static JObject JsonToJObject(this string Json)
        //{
        //    return JObject.Parse(Json);
        //}
        //public static JObject JsonToJObject(this string Json, JsonLoadSettings loadSettings)
        //{
        //    return JObject.Parse(Json, loadSettings);
        //}

        #endregion

        #region System.Runtime.Serialization.Json

        //public static string ToJson(this object obj)
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        var serializer = new DataContractJsonSerializer(obj.GetType());
        //        serializer.WriteObject(stream, obj);
        //        return Encoding.UTF8.GetString(stream.ToArray());
        //    }
        //}

        //public static T JsonTo<T>(this string strJson)
        //{
        //    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(strJson)))
        //    {
        //        var serializer = new DataContractJsonSerializer(typeof(T));
        //        return (T)serializer.ReadObject(stream);
        //    }
        //}

        #endregion

        #region System.Web.Script.Serialization

        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static T JsonTo<T>(this string str)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(str);
        }

        #endregion
    }
}