using Invoice_System.Models;
using Invoice_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Invoice_System.Controllers
{
    [Route("Invoice/")]
    [ApiController]
    public class CreateInvoiceController : ControllerBase
    {
        [HttpPost]
        [Route("CreateInvoice")]
        public IActionResult CreateInvoice(List<RequestLineItems> lineItems) {

            GlobelService globelService = new GlobelService();
            CreateInvoice createInvoice = new CreateInvoice();
            var msg_update = "";

            try
            {
                string jsonString = System.IO.File.ReadAllText(globelService.filePath);
                var products = JsonSerializer.Deserialize<ProductsDetails>(jsonString);

                ProductsDetails productsDetails = new ProductsDetails();

                productsDetails = products;

                bool available_products = false;

                foreach(Products item in productsDetails.Products)
                {
                    foreach(RequestLineItems requestLineItems in lineItems)
                    {
                        if (item.Code.Equals(requestLineItems.Code)){
                            available_products = true; break;
                        }
                    }
                }

                if (available_products)
                {
                    var invoices = createInvoice.GenerateInvoice(lineItems, products);

                    UpdateInvoiceJson updateInvoiceJson = new UpdateInvoiceJson();
                    msg_update = updateInvoiceJson.UpdateInvoice(invoices);
                }
                else
                {
                    msg_update = "Not available products";
                }

                //WirteInvoiceJson wirteInvoiceJson = new WirteInvoiceJson();
                //var msg_update = wirteInvoiceJson.WirteInvoiceData(invoices);

                return StatusCode(200, msg_update);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading JSON file: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Invoices")]
        public IActionResult Invoices()
        {
            GlobelService globelService = new GlobelService();
            List<AllInvoices> invoices = new List<AllInvoices>();
            try
            {
                string jsonString = System.IO.File.ReadAllText(globelService.defultFilePath + "\\AllInvoice.json");
                var old_invoice = JsonSerializer.Deserialize<AllInvoices>(jsonString);

                return StatusCode(200, old_invoice); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error reading JSON file: {ex.Message}");
            }
        }
    }
}
