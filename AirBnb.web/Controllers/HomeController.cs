using System.Diagnostics;
using AirBnb.Application.Common.Interfaces;
using AirBnb.web.Models;
using AirBnb.web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new()
            {
                VillaList = _unitOfWork.villa.GetAll(includeProperties: "VillaAmenity"),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now)
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
