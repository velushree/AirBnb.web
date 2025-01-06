using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBnb.Domain.Entities
{
    public class Amenity 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    

        [ForeignKey("Villa")]
        public int VillaId {  get; set; }

        [ValidateNever]
        public Villa Villa { get; set; }
        public string? Description {  get; set; }

    }
}
