using AirBnb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirBnb.web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using AirBnb.Application.Common.Interfaces;

namespace AirBnb.web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {   
            var villaNumbers = _unitOfWork.villaNumber.GetAll(includeProperties: "Villa");    
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberViewModel viewModel = new()
            {
                VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberViewModel obj) 
        {
            bool isNumberExists = _unitOfWork.villaNumber.Any(u=>u.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !isNumberExists) 
            {
                _unitOfWork.villaNumber.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["Success"] = "The Villa Number has been created";
                return RedirectToAction("Index");
            }

            if (isNumberExists) 
            {
                TempData["Error"] = "This Villa Number already exists";
            }

            obj.VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberViewModel villaNumberViewModel = new()
            {
                VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                VillaNumber = _unitOfWork.villaNumber.Get(u=>u.Villa_Number == villaNumberId)
            };

            if (villaNumberViewModel.VillaNumber == null) 
            {
                return RedirectToAction("Error","Home");
            }
            return View(villaNumberViewModel);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberViewModel obj)
        { 
            if (ModelState.IsValid )
            {
                _unitOfWork.villaNumber.Update(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["Success"] = "The Villa Number has been updated";
                return RedirectToAction("Index");
            }

            obj.VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberViewModel villaNumberViewModel = new()
            {
                VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                VillaNumber = _unitOfWork.villaNumber.Get(u => u.Villa_Number == villaNumberId)
            };

            if (villaNumberViewModel.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberViewModel);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberViewModel villaNumberViewModel)
        {
            VillaNumber? dbObj = _unitOfWork.villaNumber.Get(u=>u.Villa_Number == villaNumberViewModel.VillaNumber.Villa_Number);
            if (dbObj is not null)
            {
                _unitOfWork.villaNumber.Remove(dbObj);
                _unitOfWork.Save();
                TempData["Success"] = "The Villa Number has been Deleted";
                return RedirectToAction("Index");
            }

            villaNumberViewModel.VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View();
        }
    }
}