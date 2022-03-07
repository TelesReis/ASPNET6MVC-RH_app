#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; }
        public DbSet<MvcMovie.Models.Vaga> Vaga { get; set; }
        public DbSet<MvcMovie.Models.Tecnologia> Tecnologia { get; set; }
        public DbSet<MvcMovie.Models.Candidato> Candidato { get; set; }
        public DbSet<MvcMovie.Models.VagaTecnologia> VagaTecnologia { get; set; }
        public DbSet<MvcMovie.Models.CandidatoTecnologia> CandidatoTecnologia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CandidatoTecnologia>().ToTable("candidato_tecnologia");
        modelBuilder.Entity<VagaTecnologia>().ToTable("vaga_tecnologia");
    }
    }
