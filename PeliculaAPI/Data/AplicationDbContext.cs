using Microsoft.EntityFrameworkCore;
using PeliculaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace PeliculaAPI.Data
{
        public class ApplicationDbContext : DbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
            {

            }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet <Pelicula> Peliculas { get; set; }
    }
            
}
