using System;
using System.Collections.Generic;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using DesafioDiegoBenedetti.EF_Dapper.Models;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper
{
    //Criação do CRUD Filmes
    public interface IMovieRepository
    {
        Task<int> Add(MovieModel movie);

        Task<int> Update(MovieModel movie);

        Task<int> Delete(MovieModel movie);

        Task<IEnumerable<Movie_GenreModel>> ListMovies(string cpf);

        Task<IEnumerable<Movie_GenreModel>> ListMoviesWithOutRental();

        Task<IEnumerable<MovieModel>> ListAll();

        Task<MovieModel> ListOfId(Guid id);
    }
}