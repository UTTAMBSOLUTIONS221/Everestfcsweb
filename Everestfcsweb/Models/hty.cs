namespace Everestfcsweb.Models
{
    public class hty
    {
        public string? output { get; set; }
        public long AccountId { get; set; }
        public long AccountNumber { get; set; }
        public string? Agreementtypename { get; set; }
        public decimal Balance { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalTicket { get; set; }
        public List<CustomerAccountEquipmentItem>? CustomerAccountEquipment { get; set; }
        public string? Uid { get; set; }
    }
}
