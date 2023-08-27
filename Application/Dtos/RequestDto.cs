using System;
using Domain.Enum;

namespace Application.Dtos
{
    public class RequestDto
    {
        public Guid FarmerId { get; set; }
        public Guid ProduceTypeId { get; set; }
        public RequestType RequestType { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string RejectionReason { get; set; }
        public string ReasonForStopSelling { get; set; }
        public string Email { get; set; }
    }


    public class AddNewProduceTypeRequestModel
    {
        public Guid ProduceTypeId { get; set; }
        public string RejectionReason { get; set; }
    }
    public class RemoveExistingProduceTypeRequestModel
    {
        public Guid ProduceTypeId { get; set; }
        public string ReasonForStopSelling { get; set; }
    }

    public class RequestApproveRequestModel
    {
        public Guid RequestId { get; set; }
        public Guid FarmerId { get; set; }
        public string? RejectionReason { get; set; }

        // public RequestType RequestType { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
    public class GetAllProduceTypeRequestModel
    {
        public RequestType RequestType { get; set; }
        public RequestStatus? RequestStatus { get; set; }
    }



}