using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloraCFHN.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;


namespace FloraCFHN.AccesoADatos
{
    public class BDContexto : DbContext
    {

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Planta> Plantas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=DESKTOP-MCDA32G\SQLEXPRESS;Initial Catalog=DB_Flora;Integrated Security=True");
        }

    }
}
