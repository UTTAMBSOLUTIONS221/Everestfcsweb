﻿namespace Everestfcsweb.Models
{
    public class UserDataModel
    {
        public int RespStatus { get; set; }
        public string? RespMessage { get; set; }
        public long Userid { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public long TenantAccountId { get; set; }
        public long TenantUserid { get; set; }
        public int LoginStatus { get; set; }
        public DateTime Lastlogin { get; set; }
        public DateTime Passwordchange { get; set; }
        public bool IsCustomerUser { get; set; }
        public string? TenantName { get; set; }
        public string? TenantReference { get; set; }
        public string? TenantLogo { get; set; }
        public string? TenantEmail { get; set; }
        public string? TenantPhone { get; set; }
        public string? ConnectionString { get; set; }
        public string? ReportConnectionString { get; set; }
        public string? TenantApiURL { get; set; }
        public string? SubDomain { get; set; }
        public string? EmailType { get; set; }
        public string? EmailServer { get; set; }
        public string? EmailPassword { get; set; }
        public List<SystemPermissions> Userpermissions { get; set; }
    }
}
