namespace Everestfcsweb.Entities
{
    public class CustomerAgreementAccountData
    {
        public long AccountId { get; set; }
        public long AgreementId { get; set; }
        public long AccountNumber { get; set; }
        public long MaskType { get; set; }
        public long? MaskId { get; set; }
        public string? MaskSno { get; set; }
        public long GroupingId { get; set; }
        public long ParentId { get; set; }
        public long CredittypeId { get; set; }
        public long LimitTypeId { get; set; }
        public decimal ConsumptionLimit { get; set; }
        public string? ConsumptionPeriod { get; set; }
        public string? EquipmentReg { get; set; }
        public string? CreateAccountType { get; set; }
        public long VehicleMakeId { get; set; }
        public long VehicleModelId { get; set; }
        public long ProductVariationId { get; set; }
        public decimal TankCapacity { get; set; }
        public decimal OdometerReading { get; set; }
        public bool Isadminactive { get; set; }
        public bool Iscustomeractive { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedbyId { get; set; }
        public string? Createdby { get; set; }
        public long ModifiedId { get; set; }
        public string? Modifiedby { get; set; }
        public DateTime Datecreated { get; set; }
        public DateTime Datemodified { get; set; }
    }
}
