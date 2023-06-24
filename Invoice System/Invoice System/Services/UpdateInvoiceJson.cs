using Invoice_System.Models;
using System.Text.Json;

namespace Invoice_System.Services
{
    public class UpdateInvoiceJson
    {
        public string UpdateInvoice(Invoice invoice)
        {
            var msg = $"Success full generate invoice " + invoice.InvoiceNumber;
            GlobelService globelService = new GlobelService();
            string jsonString = File.ReadAllText(globelService.defultFilePath + "\\AllInvoice.json");

            string emptyJsonArray = "";
            File.WriteAllText(globelService.defultFilePath + "\\AllInvoice.json", emptyJsonArray);

            if (jsonString != null || jsonString != "")
            {
                var old_invoice = JsonSerializer.Deserialize<AllInvoices>(jsonString);

                List<AllInvoices> all_invoices = new List<AllInvoices>();
                
                all_invoices.Add(old_invoice);

                if(old_invoice != null) {
                    foreach (var old in all_invoices)
                    {
                        if (old.Invoices.Count > 0)
                        {
                            old.Invoices.Add(invoice);
                            string updatedJsonString = JsonSerializer.Serialize(old_invoice);
                            File.AppendAllText(globelService.defultFilePath + "\\AllInvoice.json", updatedJsonString);
                        }
                    }
                }

            }
            else
            {
                WirteInvoiceJson wirteInvoiceJson = new WirteInvoiceJson();
                msg = wirteInvoiceJson.WirteInvoiceData(invoice);
            }

            return msg;
        }
    }
}
