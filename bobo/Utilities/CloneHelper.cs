using Newtonsoft.Json;
namespace bobo.Utilities
{
    public class CloneHelper
    {
        public static T Clone<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>( JsonConvert.SerializeObject(obj));
        }
    }
}