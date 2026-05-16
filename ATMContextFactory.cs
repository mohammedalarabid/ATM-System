using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ATMSystem
{
    public class ATMContextFactory : IDesignTimeDbContextFactory<ATMContext>
    {
        public ATMContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ATMContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"));

            return new ATMContext(optionsBuilder.Options);
        }
    }
}

