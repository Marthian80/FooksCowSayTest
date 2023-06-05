using Newtonsoft.Json;

namespace FooksCowSayTest
{
    public class JsonReadWrite
    {
        public void WriteToFile(Object data, string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }

        public MessageData ReadMessageDataFromFile(string fileName) 
        {
            using StreamReader streamReader = new(fileName);
            string jSon = streamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<MessageData>(jSon);
        }
    }   
}