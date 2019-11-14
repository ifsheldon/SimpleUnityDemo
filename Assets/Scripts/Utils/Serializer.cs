using System.IO;
using Newtonsoft.Json;
/// <summary>
/// Helper class for serializing
/// </summary>
class Serializer
{
    public static void serailizeTo(object o, string filePath)
    {
        string str = JsonConvert.SerializeObject(o);
        File.WriteAllText(filePath,str);
    }

    public static T deserializeFrom<T>(string filePath)
    {
        string content = File.ReadAllText(filePath);
        T t = JsonConvert.DeserializeObject<T>(content);
        return t;
    }
}