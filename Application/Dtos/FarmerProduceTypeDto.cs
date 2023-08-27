using System.Collections.Generic;
using Domain.Enum;


namespace Application.Dtos;
public class FarmerProduceTypeDto
{
   public FarmerDto FarmerDto {get;set;}
   public List<ProduceTypeDto> ProduceTypeDto {get;set;}
   public Status Status { get; set; }

}

