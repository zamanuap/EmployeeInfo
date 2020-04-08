using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<PersonPoco> PersonPocos { get; set; }
        public DbSet<ProvincePoco> ProvincePocos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("PersonDBConnection").Value;
            optionsBuilder.UseSqlServer(_connStr);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonPoco>
                (entry => entry.HasOne(province => province.ProvincePoco)
                               .WithMany(person => person.PersonPocos)
                               .HasForeignKey(person => person.ProvinceId)
                );
        }
    }
}
