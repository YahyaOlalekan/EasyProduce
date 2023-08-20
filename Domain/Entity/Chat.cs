using System;

namespace Domain.Entity
{
    public class Chat : BaseEntity
    {
        public string Message { get; set; }
        public bool Seen { get; set; } = false; 
        public string PostedTime { get; set; }
        public Guid ManagerId { get; set; }
        public Guid FarmerId { get; set; }
        public Manager Manager { get; set; }
        public Farmer Farmer { get; set; }
    }
}