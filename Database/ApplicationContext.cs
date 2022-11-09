using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.Entity<Pokemon>().ToTable("Pokemons");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<Tipo>().ToTable("Tipos");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Pokemon>().HasKey(e => e.Id);
            modelBuilder.Entity<Region>().HasKey(e => e.Id);
            modelBuilder.Entity<Tipo>().HasKey(e => e.Id);
            #endregion

            #region Property configurations

            #region pokemons
            modelBuilder.Entity<Pokemon>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Pokemon>()
                .Property(p => p.UrlImage)
                .IsRequired();

            modelBuilder.Entity<Pokemon>()
               .Property(p => p.RegionId)
               .IsRequired();

            modelBuilder.Entity<Pokemon>()
               .Property(p => p.FirstTypeId)
               .IsRequired();

            modelBuilder.Entity<Pokemon>()
               .Property(p => p.SecondTypeId)
               .HasDefaultValue(null);

            #endregion

            #region Regions
            modelBuilder.Entity<Region>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #region Tipos
            modelBuilder.Entity<Tipo>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #endregion

            #region Relationships

            #region Regions 
            modelBuilder.Entity<Region>()
                .HasMany<Pokemon>(r => r.Pokemons)
                .WithOne(g => g.Region)
                .HasForeignKey(r => r.RegionId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Tipos 
            modelBuilder.Entity<Tipo>()
                .HasMany<Pokemon>(t => t.PokemonsFirst)
                .WithOne(g => g.FirstType)
                .HasForeignKey(r => r.FirstTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Tipo>()
                .HasMany<Pokemon>(t => t.PokemonsSecond)
                .WithOne(g => g.SecondType)
                .HasForeignKey(r => r.SecondTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion

            #endregion
        }
    }
}
