namespace Invoice_System.Models
{
    public class LineItems
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public int TotalItemQty { get; set; }
        public decimal LineItemTotal { get; set; }
        public List<InvoicePacks> Packs { get; set;}
    }
}
