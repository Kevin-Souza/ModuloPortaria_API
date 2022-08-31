using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModuloPortaria.models;

namespace ModuloPortaria.Data
{
    public class AppCont : DbContext
    {
        public AppCont(DbContextOptions<AppCont> options): base(options)
        {
        }

        public DbSet<visitante> visitantes {get; set;}
    }
}