namespace Everestfcsweb.Entities
{
    public class CustomerAccountTransfer
    {
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
        public long TenanttId { get; set; }
        public decimal TransferAmount { get; set; }
        public string? TransactionDescription { get; set; }
        public long CreatedbyId { get; set; }
        public long ModifiedId { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime Datemodified { get; set; }
    }
}
