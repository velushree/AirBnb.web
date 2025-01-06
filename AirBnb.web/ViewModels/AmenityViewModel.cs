using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBnb.web.ViewModels
{
    public class AmenityViewModel
    {
        public Amenity? amenity { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList{get;set;}
    }
}
