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
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var amenities = _unitOfWork.amenity.GetAll(includeProperties: "Villa");
            return View(amenities);
        }

        public IActionResult Create()
        {
            AmenityViewModel viewModel = new()
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
        public IActionResult Create(AmenityViewModel obj) 
        {
            if (ModelState.IsValid ) 
            {
                _unitOfWork.amenity.Add(obj.amenity);
                _unitOfWork.Save();
                TempData["Success"] = "The amenity has been created";
                return RedirectToAction("Index");
            }

            obj.VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Update(int amenityId)
        {
            AmenityViewModel amenityViewModel = new()
            {
                VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                amenity = _unitOfWork.amenity.Get(u=>u.Id == amenityId)
            };

            if (amenityViewModel.amenity == null) 
            {
                return RedirectToAction("Error","Home");
            }
            return View(amenityViewModel);
        }

        [HttpPost]
        public IActionResult Update(AmenityViewModel obj)
        { 
            if (ModelState.IsValid )
            {
                _unitOfWork.amenity.Update(obj.amenity);
                _unitOfWork.Save();
                TempData["Success"] = "The amenity has been updated";
                return RedirectToAction("Index");
            }

            obj.VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }

        public IActionResult Delete(int amenityId)
        {
            AmenityViewModel amenityViewModel = new()
            {
                VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                amenity = _unitOfWork.amenity.Get(u => u.Id == amenityId)
            };

            if (amenityViewModel.amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityViewModel);
        }

        [HttpPost]
        public IActionResult Delete(AmenityViewModel amenityViewModel)
        {
            Amenity? dbObj = _unitOfWork.amenity.Get(u=>u.Id == amenityViewModel.amenity.Id);
            if (dbObj is not null)
            {
                _unitOfWork.amenity.Remove(dbObj);
                _unitOfWork.Save();
                TempData["Success"] = "The amenity has been Deleted";
                return RedirectToAction("Index");
            }

            amenityViewModel.VillaList = _unitOfWork.villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View();
        }
    }
}