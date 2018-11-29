using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace provider.Extensions
{
    public static class DTOExtensions
    {
        public static T AsAPIEntity<T>(this object dto)
        {
            string dtoJson = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<JObject>(dtoJson).ToObject<T>();
        }
    }
}