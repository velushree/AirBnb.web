using AirBnb.Application.Common.Interfaces;
using AirBnb.Domain.Entities;
using AirBnb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {   
            var villas = _unitOfWork.villa.GetAll();
            return View(villas);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("description","Description Cannot be same as Name");
            }
            if (ModelState.IsValid) 
            {
                if (obj.Image != null) 
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath,@"images\VillaImage");
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }   
                _unitOfWork.villa.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();   
        }

        public IActionResult Update(int villaId)
        {  
            Villa ?villa = _unitOfWork.villa.Get(villa => villa.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id>0)
            {   
                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath,@"images\VillaImage\");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);    
                        }
                    }
                    using var fileStream = new FileStream(Path.Combine(imagePath,fileName),FileMode.Create);
                    obj.Image.CopyTo(fileStream);
                    obj.ImageUrl = @"\images\VillaImage\" + fileName;
                }
                else 
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }

                _unitOfWork.villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Villa has been updated Sucessfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? villa = _unitOfWork.villa.Get(villa => villa.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {   
            Villa? dbObj = _unitOfWork.villa.Get(db => db.Id == obj.Id); 
            if (dbObj is not null)
            {
                _unitOfWork.villa.Remove(dbObj);
                _unitOfWork.Save();
                TempData["success"] = "Villa Deleted Sucessfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa could not be Deleted !";
            return View();
        }
    }
}