using Invoice_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice_System.Services
{
    public class WirteJson
    {
        public void WirteJsonData(List<Invoice> invoices)
        {
            GlobelService globelService = new GlobelService();
            try
            {
                var jsonToWrite = JsonConvert.SerializeObject(invoices,Formatting.Indented);

                using (var write = new StreamWriter(globelService.filePath))
                {
                    write.Write(jsonToWrite);
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
