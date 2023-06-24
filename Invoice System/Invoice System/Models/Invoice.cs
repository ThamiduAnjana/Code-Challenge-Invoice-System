namespace Invoice_System.Models
{
    public class Invoice
    {
        public DateTime InvoiceIssueDate { get; set; }
        public string InvoiceNumber { get; set;}
        public decimal InvoiceTotal { get; set;}

        public List<LineItems> LineItems { get; set;}
    }
}
