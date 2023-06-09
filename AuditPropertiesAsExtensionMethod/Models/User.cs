using ConsoleApp2.Models.Interfaces;
using System;

namespace ConsoleApp2.Models
{
    internal class User: IIdentifiable, IAudit
    {
        public string Name { get; set; }
        public string Email {  get; set; }  
        public string Password {  get; set; }   
        public string UserName {  get; set; }
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string LocalIp { get; set; }
        public string MacAddress { get; set; }
    }
}
