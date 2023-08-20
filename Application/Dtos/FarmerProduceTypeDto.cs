using System.Collections.Generic;
using Domain.Enum;


namespace Application.Dtos;
public class FarmerProduceTypeDto
{
   public FarmerDto farmerDto {get;set;}
   public List<ProduceTypeDto> produceTypeDto {get;set;}
   public Status Status { get; set; }

}

