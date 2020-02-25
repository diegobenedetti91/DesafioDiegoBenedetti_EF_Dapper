using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository
{
    //Criação do CRUD Genero de Filme
    public interface IMovieGenreRepository
    {
        Task<int> Add(MovieGenreModel movieGenre);

        Task<int> Update(MovieGenreModel movieGenre);

        Task<int> Delete(MovieGenreModel movieGenre);

        Task<ICollection<MovieGenreModel>> ListAll();

        Task<MovieGenreModel> ListOfId(Guid id);
    }
}