﻿using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBnb.web.ViewModels
{
    public class HomeViewModel
    {
        public DateOnly CheckInDate  { get; set; }   
        public DateOnly? CheckOutDate { get; set; }
        public int Nights {  get; set; }    
        public IEnumerable<Villa>? VillaList{ get; set; }
    }
}