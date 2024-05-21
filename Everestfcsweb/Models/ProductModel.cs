namespace Everestfcsweb.Models
{
    public class ProductModel
    {
        public long ProductVariationId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscount { get; set; }
        public string? ProductCode { get; set; }
    }
}
