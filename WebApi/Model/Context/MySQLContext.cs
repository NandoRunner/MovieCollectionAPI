﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) {}

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<_vw_mc_ator> vw_mc_ator { get; set; }
        public DbSet<_vw_mc_diretor> vw_mc_diretor { get; set; }
        public DbSet<_vw_mc_filme_visto> vw_mc_filme_visto { get; set; }
        public DbSet<_vw_mc_filme_ver> vw_mc_filme_ver { get; set; }
        public DbSet<_vw_mc_genero> vw_mc_genero { get; set; }

        public DbSet<_vw_mc_filme_por_ator> vw_mc_filme_por_ator { get; set; }
        public DbSet<_vw_mc_filme_por_diretor> vw_mc_filme_por_diretor { get; set; }
        public DbSet<_vw_mc_filme_por_genero> vw_mc_filme_por_genero { get; set; }

        public DbSet<WebUser> WebUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                .Property<long?>("id").HasColumnName("ato_ator_id");
            modelBuilder.Entity<Actor>()
                .Property<string>("name").HasColumnName("ato_nome");

            modelBuilder.Entity<Genre>()
                .Property<long?>("id").HasColumnName("gen_genero_id");
            modelBuilder.Entity<Genre>()
                .Property<string>("name").HasColumnName("gen_nome");

            modelBuilder.Entity<Director>()
                .Property<long?>("id").HasColumnName("dir_diretor_id");
            modelBuilder.Entity<Director>()
                .Property<string>("name").HasColumnName("dir_nome");

            modelBuilder.Entity<_vw_mc_filme_por_ator>()
                .HasKey(c => new { c.id, c.filme_id });

            modelBuilder.Entity<_vw_mc_filme_por_diretor>()
                .HasKey(c => new { c.id, c.filme_id });

            modelBuilder.Entity<_vw_mc_filme_por_genero>()
                .HasKey(c => new { c.id, c.filme_id });

            modelBuilder.Entity<Movie>()
                .Property<long?>("id").HasColumnName("fil_filme_id");
            modelBuilder.Entity<Movie>()
                .Property<string>("titulo").HasColumnName("fil_titulo");

        }
    }
}
