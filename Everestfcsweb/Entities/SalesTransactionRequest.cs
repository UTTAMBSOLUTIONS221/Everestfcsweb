namespace Everestfcsweb.Entities
{
    public class SalesTransactionRequest
    {
        public string? TerminalName { get; set; }
        public long AccountId { get; set; }
        public long TenantId { get; set; }
        public long AccountNumber { get; set; }
        public long PaymentModeId { get; set; }
        public string? CardUID { get; set; }
        public string? Mask { get; set; }
        public string? AutomationReferenceId { get; set; }
        public long StationId { get; set; }
        public long Userid { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalMoneySold { get; set; }
        public long CustomerVehicleId { get; set; }
        public string? DriverCode { get; set; }
        public string? SaleReference { get; set; }
        public decimal OdometerReading { get; set; }
        public string? TransactionCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsSaleOnline { get; set; }
        public List<SaleTicketLines>? TicketLines { get; set; }
        public List<SalePaymentList>? PaymentList { get; set; }
        public long CreatedbyId { get; set; }
        public string? Createdby { get; set; }
        public long ModifiedId { get; set; }
        public string? Modifiedby { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime Datemodified { get; set; }
    }
}
