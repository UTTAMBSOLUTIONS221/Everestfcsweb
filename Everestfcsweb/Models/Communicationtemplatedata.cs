﻿namespace Everestfcsweb.Models
{
    public class Communicationtemplatedata
    {
        public long Templateid { get; set; }
        public string? Templatename { get; set; }
        public string? Templatesubject { get; set; }
        public string? Templatebody { get; set; }
        public bool Isactive { get; set; }
        public bool Isdeleted { get; set; }
        public bool Isemailsms { get; set; }
    }
}
