namespace Invoice_System.Models
{
    public class InvoicePacks
    {
        public string PackageCode { get; set; }
        public int ItemsPerPack { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }
}
