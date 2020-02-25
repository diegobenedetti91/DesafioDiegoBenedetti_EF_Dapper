using System;
using System.Collections.Generic;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository;
using DesafioDiegoBenedetti.EF_Dapper.Models;
using DesafioDiegoBenedetti.EF_Dapper.Context;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration configuration;
        private AppDBContext _context;

        //Toda classe instanciada rentalMovie irá sempre receber o padrão do config para informar a conexão do BD
        //A interface movie recebe também como paramêtro a context para buscar dados do genero do filme
        public MovieRepository(IConfiguration config, AppDBContext context)
        {
            _context = context;
            configuration = config;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        // Insere os dados dos filmes
        public async Task<int> Add(MovieModel movie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.ExecuteAsync("INSERT INTO Movie     " +
                                        "(idMovie   " +
                                        ",name      " +
                                        ",createdAt " +
                                        ",status    " +
                                    ",idGenreMovie) " +
                                    "VALUES         " +
                                        "(@idMovie   " +
                                        ",@name      " +
                                        ",@createdAt " +
                                        ",@status    " +
                                        ",@idGenreMovie)",movie);
            }
        }


        //Exclusão dos dados do file pelo @idmovie
        public async Task<int> Delete(MovieModel movie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.ExecuteAsync("DELETE FROM Movie where idMovie = @idMovie", movie);
            }            
        }

        //Busca todos os dados dos FIlmes
        public async Task<IEnumerable<MovieModel>> ListAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var movie = await conn.QueryAsync<MovieModel>("SELECT * FROM Movie");
                List<MovieModel> movieReturn = new List<MovieModel>();
                List<RentalMovieModel> rentalMovie = new List<RentalMovieModel>();


                foreach (var item in movie)
                {
                    item.movieGenre = _context.movieGenres.ToList().Find(g => g.idGenreMovie == item.idGenreMovie);

                    movieReturn.Add(item);
                }

                return movieReturn;
            }
        }

        public async Task<IEnumerable<Movie_GenreModel>> ListMoviesWithOutRental()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Movie_GenreModel>(string.Format("SELECT Movie.idMovie,Movie.name nameMovie, MovieGenre.name movieGenre,movie.status FROM Movie"
                                                                                         + " JOIN MovieGenre ON MovieGenre.idGenreMovie = Movie.idGenreMovie"
                                                                                         + "    WHERE idMovie not in(select idMovie from RentalMovie)"));
            }
        }

        // Busca os dados com campos filtrados dos filmes com vinculo da tabela genero e locação
        // existe a condição por cpf pelo qual trará somente o qual usuário esta logado
        public async Task<IEnumerable<Movie_GenreModel>> ListMovies(string cpf)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<Movie_GenreModel>(string.Format("SELECT Movie.idMovie,cpf,Movie.name nameMovie, MovieGenre.name movieGenre,rentalDate dateRental,movie.status FROM RentalMovie"
                                                                                         + " JOIN Movie ON Movie.idMovie = RentalMovie.idMovie "
                                                                                         + " JOIN MovieGenre ON MovieGenre.idGenreMovie = Movie.idGenreMovie"
                                                                                         + "    WHERE cpf = '{0}'", cpf));
            }
            
        }

        //busca dados da tabela filmes pelo idMovie
        public async Task<MovieModel> ListOfId(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {                
                return await conn.QuerySingleAsync<MovieModel>(string.Format("SELECT * FROM Movie WHERE idMovie = '{0}'",id));
            }
        }

        //Altera os dados do filme
        public async Task<int> Update(MovieModel movie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.ExecuteAsync("UPDATE Movie  " +
                                "SET  " +
                                    "name = @name " +
                                    ",createdAt = @createdAt " +
                                    ",status = @status " +
                                    ",idGenreMovie = @idGenreMovie " +
                                "WHERE idMovie = @idMovie",movie);
            }
        }
    }
}