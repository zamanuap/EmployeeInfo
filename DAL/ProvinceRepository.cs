using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ProvinceRepository
    {
        public IEnumerable<SelectListItem> GetProvinces()
        {
            using (var context = new AppDbContext())
            {
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
                    Text = "--- select country ---"
                };
                provinces.Insert(0, provincetip);
                return new SelectList(provinces, "Value", "Text");
            }
        }
    }
}
