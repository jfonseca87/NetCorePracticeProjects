using System;

namespace ConsoleApp2.Models.Interfaces
{
    public interface IAudit
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set;}
        public DateTime? UpdatedAt { get; set;}
        public string LocalIp { get; set; }
        public string MacAddress { get; set; }
    }
}
