using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Models
{
    public class BibliotecaDBContext : DbContext
    {
        public BibliotecaDBContext(DbContextOptions<BibliotecaDBContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Carti> Carti { get; set; }
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<ImprumuturiCarti> ImprumuturiCarti { get; set; }
    }


    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BibliotecaDBContext>
    {
        public BibliotecaDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BibliotecaDBContext>();
            var connectionString = configuration.GetConnectionString("BibliotecaDBConnection");
            builder.UseSqlServer(connectionString);
            return new BibliotecaDBContext(builder.Options);
        }
    }
}
