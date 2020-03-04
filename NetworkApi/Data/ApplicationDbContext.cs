using Microsoft.EntityFrameworkCore;
using NetworkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkApi.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
                
        }

        public DbSet<NationalNetwork> NationalNetworks
        {
            get;set;
        }

        //insert users 
        public DbSet<User> Users { get; set; }
       
    }
}
