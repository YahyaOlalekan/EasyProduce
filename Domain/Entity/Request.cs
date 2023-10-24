using System;
using Domain.Enum;

namespace Domain.Entity
{
    public class Request : BaseEntity
    {
        public Guid FarmerId { get; set; }
        public Guid ProduceTypeId { get; set; }
        public string RegistrationNumber { get; set; }
        public RequestType RequestType { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string? RejectionReason { get; set; }
        public string ReasonForStopSelling { get; set; }
        public Farmer Farmer { get; set; }
        public string RequestNumber { get; set; }
        public string Email { get; set; }
    }
}