using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.Models;
using Employee.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Poco;
using ReflectionIT.Mvc.Paging;
using X.PagedList;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repo;
        private readonly ProvinceRepository _prepo;

        public EmployeeController(EmployeeRepository repo, ProvinceRepository prepo)
        {
            _repo = repo;
            _prepo = prepo;
        }
         
        public IActionResult Create()
        {
            var employee = _repo.CreateEmployee();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Create([Bind("Id, Name, SelectedProvinceId, ProvinceNames, Telephone, PostalCode, Salary")] EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool saved = _repo.SaveEmployee(model);
                if (saved)
                {
                    TempData["Id"] = model.Id;
                    return RedirectToAction("Details","Employee");
                }
            }
            
            //var provinceRepo = new ProvinceRepository();
            //model.ProvinceNames = provinceRepo.GetProvinces();
            model.ProvinceNames = _prepo.GetProvinces();
            return View(model);

        }
       
        public IActionResult Details()
        {
            Guid Id = Guid.Parse(TempData["Id"].ToString());
            EmployeeDisplayViewModel employee =  _repo.GetEmployee(Id);
            return View(employee);
        }

        public IActionResult Index(int pageindex = 1)
        {
            List<EmployeeDisplayViewModel> employeePocos = _repo.GetAllEmployees();
            var model = PagingList.Create(employeePocos, 2, pageindex);
            return View(model);
        }
        public IActionResult PageDisplay(int page=1)
        {
            ViewBag.Page = page;
            return View();
        }

        [HttpPost]
        public PagedModel Record(int currentPage)
        {
            int recordPerPage = 2;
            PagedModel model = _repo.GetPagedEmployees(recordPerPage, currentPage);
            return model;
        }

        public IActionResult Delete(Guid id, int page)
        {
            _repo.Delete(id);
            return RedirectToAction("PageDisplay", new { page = page }); ;
        }

        public IActionResult Update(Guid id)
        {
            EmployeeDisplayViewModel viewModel = _repo.GetEmployee(id);
            return View("Create");
        }
    }
}