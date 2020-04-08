using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class ProvinceRepository
    {
        private readonly AppDbContext context;

        public ProvinceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetProvinces()
        {
            //using (var context = new AppDbContext())
            //{
                List<SelectListItem> provinces = context.ProvincePocos.AsNoTracking()
                     .Select(n =>
                        new SelectListItem
                        {
                            Value = n.Id.ToString(),
                            Text = n.Name
                        }).ToList();
                var provincetip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Province ---"
                };
                provinces.Insert(0, provincetip);
                return new SelectList(provinces, "Value", "Text");
            //}
        }
        public string GetProvince(int id)
        {
            //using (var context = new AppDbContext())
            //{
                var province = context.ProvincePocos.Find(id);
                return province.Name;
            //}
        }
    }
}