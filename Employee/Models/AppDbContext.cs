using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<EmployeePoco> EmployeePocos { get; set; }
        public DbSet<ProvincePoco> ProvincePocos { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("EmployeeDBConnection").Value;
            optionsBuilder.UseSqlServer(_connStr);
            base.OnConfiguring(optionsBuilder);
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePoco>
                (entry => entry.HasOne(province => province.ProvincePoco)
                               .WithMany(employee => employee.EmployeePocos)
                               .HasForeignKey(employee => employee.ProvinceId)
                );
        }
    }
}
