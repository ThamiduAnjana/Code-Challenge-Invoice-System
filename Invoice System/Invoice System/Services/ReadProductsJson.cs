using Invoice_System.Models;
using System.Text.Json;

namespace Invoice_System.Services
{
    public class ReadProductsJson
    {
        public string ReadJsonData(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                var jsonData = JsonSerializer.Deserialize<object>(jsonString);
                return (string)jsonData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
