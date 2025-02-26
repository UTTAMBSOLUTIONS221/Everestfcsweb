﻿namespace Everestfcsweb.Models
{
    public class UserModel
    {
        public long Userid { get; set; }
        public long Tenantid { get; set; }
        public string? Tenantname { get; set; }
        public string? Tenantsubdomain { get; set; }
        public string? TenantLogo { get; set; }
        public string? Currencyname { get; set; }
        public string? Utcname { get; set; }
        public string? Fullname { get; set; }
        public string? Phonenumber { get; set; }
        public string? Username { get; set; }
        public string? Emailaddress { get; set; }
        public int Roleid { get; set; }
        public string? Rolename { get; set; }
        public string? Passharsh { get; set; }
        public string? Passwords { get; set; }
        public int LimitTypeId { get; set; }
        public decimal LimitTypeValue { get; set; }
        public bool Isactive { get; set; }
        public bool Isdeleted { get; set; }
        public int Loginstatus { get; set; }
        public DateTime Passwordresetdate { get; set; }
        public long Createdby { get; set; }
        public long Modifiedby { get; set; }
        public DateTime Lastlogin { get; set; }
        public DateTime Datemodified { get; set; }
        public DateTime Datecreated { get; set; }
        //public List<SystemPermissions>? Permission { get; set; }
        public List<UserStations>? Stations { get; set; }
    }
}
