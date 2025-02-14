﻿using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBnb.web.ViewModels
{
    public class VillaNumberViewModel
    {
        public VillaNumber? VillaNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? VillaList{get;set;}
    }
}
