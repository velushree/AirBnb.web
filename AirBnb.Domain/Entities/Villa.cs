﻿using System;
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
    public class Villa
    {   
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Display(Name="Price Per Night")]
        [Range(10,10000)]
        public double? Price { get; set; }  
        public int Sqft { get; set; }

        [Range(1,10)]
        public int Occupancy { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        [Display(Name="Image Url")]
        public string? ImageUrl {  get; set; }
        public DateTime Created_Date {  get; set; }
        public DateTime Updated_Date { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity> VillaAmenity { get; set; }
    }
}
