using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class ProduceDto
    {
         public Guid Id { get; set; }
         public string ProduceName { get; set; }
         public string DescriptionOfProduce { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProduceTypeDto> ProduceTypes { get; set; }
    }

        public class CreateProduceRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        [Display(Name = "Name of Produce")]
        public string ProduceName { get; set; }

        [Required, MaxLength(60), MinLength(3)]
        [Display(Name = "Description of Produce")]
        public string DescriptionOfProduce { get; set; }
    }
    public class UpdateProduceRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        [Display(Name = "Name of Produce")]
        public string ProduceName { get; set; }

        [Required, MaxLength(60), MinLength(3)]
        [Display(Name = "Description of Produce")]
        public string DescriptionOfProduce { get; set; }
    }

}