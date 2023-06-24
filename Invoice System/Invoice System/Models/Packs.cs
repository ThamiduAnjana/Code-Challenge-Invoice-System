namespace Invoice_System.Models
{
    public class Packs
    {
        public string Name { get; set; }
        public int Qty { get; set; }
        public int PreQty { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
    }
}
