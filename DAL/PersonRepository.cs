using Microsoft.EntityFrameworkCore;
using Person.ViewModels;
using Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PersonRepository
    {
        public List<PersonDisplayViewModel> GetPersons()
        {
            using (var context = new AppDbContext())
            {
                List<PersonPoco> personPocos = new List<PersonPoco>();
                personPocos = context.PersonPocos.AsNoTracking()
                    .Include(x => x.ProvincePoco)
                    .ToList();

                if (personPocos != null)
                {
                    List<PersonDisplayViewModel> personsDisplay = new List<PersonDisplayViewModel>();
                    foreach (var p in personPocos)
                    {
                        var person = new PersonDisplayViewModel()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Province = p.ProvincePoco.Name,
                            Telephone = p.Telephone,
                            PostalCode = p.PostalCode,
                            Salary = p.Salary
                        };
                        personsDisplay.Add(person);
                    }
                    return personsDisplay;
                }
                return null;
            }
        }


        public PersonEditViewModel CreatePerson()
        {
            var cRepo = new ProvinceRepository();
            var customer = new PersonEditViewModel()
            {
                Id = Guid.NewGuid(),
                ProvincePoco = cRepo.GetProvinces()
            };
            return customer;
        }

        public bool SaveCustomer(PersonEditViewModel personEdit)
        {
            if (personEdit != null)
            {
                using (var context = new AppDbContext())
                {
                        var person = new PersonPoco()
                        {
                            Id = personEdit.Id,
                            Name = personEdit.Name,
                            ProvinceId = personEdit.SelectedProvinceId,
                            Telephone = personEdit.Telephone,
                            PostalCode = personEdit.PostalCode,
                            Salary = personEdit.Salary
                        };
                        person.ProvincePoco = context.ProvincePocos.Find(personEdit.SelectedProvinceId);
                        
                        context.PersonPocos.Add(person);
                        context.SaveChanges();
                        return true;
                }
            }
            // Return false if customeredit == null or CustomerID is not a guid
            return false;
        }
    }
}
