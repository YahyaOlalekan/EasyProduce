using System.Collections.Generic;

namespace Application.Dtos;
public class FarmerProduceTypeDto
{
   public FarmerDto farmerDto {get;set;}
   public List<ProduceTypeDto> produceTypeDto {get;set;}
}

