using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Models;

namespace DesafioDiegoBenedetti.EF_Dapper.Context
{
    //Criação do banco de dados e os DbSet para iniciarmos toda a manipulação dos models para o CRUD 
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Infoma as tabelas a serem validadas ao iniciar a aplicação
            modelBuilder.Entity<MovieModel>().ToTable("Movie");
            modelBuilder.Entity<MovieGenreModel>().ToTable("MovieGenre");
            modelBuilder.Entity<UserModel>().ToTable("User");
            modelBuilder.Entity<RentalMovieModel>().ToTable("RentalMovie");

            //Ignora a classe model pois só manipula dados e não tem comunicação com o DB
            modelBuilder.Ignore<Movie_Genrel>();
        }

        public DbSet<UserModel> users { get; set; }
        public DbSet<MovieGenreModel> movieGenres { get; set; }    
        public DbSet<MovieModel> movies {set; get; }        
        public DbSet<RentalMovieModel> rentalMovies { set; get; }

        public DbSet<Movie_Genrel> movie_Genrels { get; set; }
    }    
}	