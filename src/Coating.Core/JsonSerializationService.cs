using Newtonsoft.Json;

namespace Coating
{
    public class JsonSerializationService
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializationService()
        {
            _settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateParseHandling = DateParseHandling.DateTime,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                    Formatting = Formatting.None,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
        }

        public string Serialize(object o)
        {
            if (o == null)
            {
                return "";
            }

            return JsonConvert.SerializeObject(o, _settings);
        }

        public T Deserialize<T>(string jsonData) where T : class 
        {
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(jsonData, _settings);
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
    }
}