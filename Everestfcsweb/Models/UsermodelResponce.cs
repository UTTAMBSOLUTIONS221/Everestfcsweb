﻿namespace Everestfcsweb.Models
{
    public class UsermodelResponce
    {
        public int RespStatus { get; set; }
        public string? RespMessage { get; set; }
        public string? Token { get; set; }
        public UserModel? Usermodel { get; set; }
    }
}
