namespace Invoice_System.Models
{
    public class Products
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Packs> Packs { get; set; }
    }
}
