using Microsoft.EntityFrameworkCore;
using Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.ViewModels;

namespace Employee.Models
{
    public class EmployeeRepository
    {
        private readonly AppDbContext context;
        private readonly ProvinceRepository prepo;

        public EmployeeRepository(AppDbContext context, ProvinceRepository  prepo)
        {
            this.context = context;
            this.prepo = prepo;
        }
        public EmployeeDisplayViewModel GetEmployee(Guid Id)
        {
            //using (var context = new AppDbContext())
            //{
                EmployeePoco employee = context.EmployeePocos
                    .Where(x => x.Id == Id)
                    .Include(x => x.ProvincePoco)
                    .FirstOrDefault();

                var emp = new EmployeeDisplayViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Province = employee.ProvincePoco.Name,
                    Telephone = employee.Telephone,
                    PostalCode = employee.PostalCode,
                    Salary = employee.Salary
                };
                return emp;
            //}
        }
        public List<EmployeeDisplayViewModel> GetAllEmployees()
        {
            //using(var context = new AppDbContext())
            //{
                List<EmployeePoco> employeePocos = context.EmployeePocos
                    .Include(x => x.ProvincePoco)
                    .ToList();

                if(employeePocos != null)
                {
                    List<EmployeeDisplayViewModel> empDisplayList = new List<EmployeeDisplayViewModel>();
                    foreach (var emp in employeePocos)
                    {
                        var employee = new EmployeeDisplayViewModel()
                        {
                            Id = emp.Id,
                            Name = emp.Name,
                            Province =emp.ProvincePoco.Name,
                            Telephone = emp.Telephone,
                            PostalCode = emp.PostalCode,
                            Salary = emp.Salary
                        };
                        empDisplayList.Add(employee);
                    }
                    return empDisplayList;
                }
                
                return null;
            //}
            
        }
        public List<EmployeeDisplayViewModel> GetEmployees()
        {
            //using (var context = new AppDbContext())
            //{
                List<EmployeePoco> employeePocos = new List<EmployeePoco>();
                employeePocos = context.EmployeePocos.AsNoTracking()
                    .Include(x => x.ProvincePoco)
                    .ToList();

                if (employeePocos != null)
                {
                    List<EmployeeDisplayViewModel> employeesDisplay = new List<EmployeeDisplayViewModel>();
                    foreach (var e in employeePocos)
                    {
                        var employee = new EmployeeDisplayViewModel()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Province = e.ProvincePoco.Name,
                            Telephone = e.Telephone,
                            PostalCode = e.PostalCode,
                            Salary = e.Salary
                        };
                        employeesDisplay.Add(employee);
                    }
                    return employeesDisplay;
                }
                return null;
            //}
        }

        public PagedModel GetPagedEmployees(int recordPerPage, int currentPage)
        {
            //using(var context = new AppDbContext())
            //{
                List<EmployeePoco> pocos = context.EmployeePocos.AsNoTracking()
                                                .OrderBy( x => x.TimeStamp)
                                                .Include(x => x.ProvincePoco)
                                                .ToList();

                double TotalPageDouble = pocos.Count() / (double)recordPerPage;
                int TotalPage = (int)Math.Ceiling(TotalPageDouble);

                if (currentPage > TotalPage)
                    currentPage = TotalPage;

                List<EmployeePoco> pocosForDispaly = pocos.Skip((currentPage - 1) * recordPerPage)
                                                    .Take(recordPerPage)
                                                    .ToList();
                if (pocos != null)
                {
                    List<EmployeeDisplayViewModel> employeesDisplay = new List<EmployeeDisplayViewModel>();
                    foreach (var e in pocosForDispaly)
                    {
                        var employee = new EmployeeDisplayViewModel()
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Province = e.ProvincePoco.Name,
                            Telephone = e.Telephone,
                            PostalCode = e.PostalCode,
                            Salary = e.Salary
                        };
                        employeesDisplay.Add(employee);
                    }
                    PagedModel model = new PagedModel();
                    model.employeeDisplays = employeesDisplay;
                    model.totalPage = TotalPage;

                    return model;
                }
                return null;

            //}
        }
        public EmployeeEditViewModel CreateEmployee()
        {
            //var cRepo = new ProvinceRepository();
            var employee = new EmployeeEditViewModel()
            {
                Id = Guid.NewGuid(),
                //ProvinceNames = cRepo.GetProvinces()
                ProvinceNames = prepo.GetProvinces()
            };
            return employee;
        }

        public bool SaveEmployee(EmployeeEditViewModel employeeEdit)
        {
            if (employeeEdit != null)
            {
               // using (var context = new AppDbContext())
                //{
                    var employee = new EmployeePoco()
                    {
                        Id = employeeEdit.Id,
                        Name = employeeEdit.Name,
                        ProvinceId = employeeEdit.SelectedProvinceId,
                        Telephone = employeeEdit.Telephone,
                        PostalCode = employeeEdit.PostalCode,
                        Salary = employeeEdit.Salary
                    };
                    employee.ProvincePoco = context.ProvincePocos.Find(employeeEdit.SelectedProvinceId);

                    context.EmployeePocos.Add(employee);
                    context.SaveChanges();
                    return true;
               // }
            }
            // Return false if customeredit == null or CustomerID is not a guid
            return false;
        }

        public void Delete(Guid id)
        {
            EmployeePoco employee = context.EmployeePocos.Find(id);
            context.EmployeePocos.Remove(employee);
            context.SaveChanges();
        }

        public void Update(Guid id)
        {
            EmployeePoco employee = context.EmployeePocos.Find(id);
            context.EmployeePocos.Remove(employee);
            context.SaveChanges();
        }
    }
}
