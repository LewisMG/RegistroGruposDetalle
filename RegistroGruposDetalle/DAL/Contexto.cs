using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RegistroGruposDetalle.Entidades;

namespace RegistroGruposDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Personas> personas { get; set; }
        public DbSet<Grupos> grupos { get; set; }

        public Contexto() : base("ConStr")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
