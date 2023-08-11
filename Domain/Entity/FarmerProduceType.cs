using System;

namespace Domain.Entity;
public class FarmerProduceType : BaseEntity
{
    public Guid FarmerId {get;set;}
    public Farmer Farmer {get;set;}
    public Guid ProduceTypeId {get;set;}
    public ProduceType ProduceType {get;set;}
}
