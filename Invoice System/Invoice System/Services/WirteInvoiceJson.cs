using Invoice_System.Models;
using Newtonsoft.Json;
using System;

namespace Invoice_System.Services
{
    public class WirteInvoiceJson
    {
        public string WirteInvoiceData(Invoice invoice)
        {
            GlobelService globelService = new GlobelService();

            AllInvoices allInvoices = new AllInvoices
            {
                Invoices = new List<Invoice> { invoice }
            };

            string json = JsonConvert.SerializeObject(allInvoices);
            using (StreamWriter file = File.CreateText(globelService.defultFilePath + "\\AllInvoice.json"))
            {
                file.Write(json);
            }
            return $"Success full generate invoice " + invoice.InvoiceNumber;
        }
    }
}
