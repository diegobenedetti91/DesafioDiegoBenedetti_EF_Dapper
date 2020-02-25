using System;
using System.Collections.Generic;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Context;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.EntityFrameWorkRepository
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        private AppDBContext _context;

        //Todo repositório de genero de filme recebe como parametro a conexão do DBContext
        public MovieGenreRepository(AppDBContext context)
        {
            _context = context;
        }

        //Adiciona genero de filme
        public async Task<int> Add(MovieGenreModel movieGenre)
        {
            _context.movieGenres.Add(movieGenre);
            return await _context.SaveChangesAsync();
        }

        //Exclui genero de filme
        public async Task<int> Delete(MovieGenreModel movieGenre)
        {
            _context.movieGenres.Remove(movieGenre);
            return await _context.SaveChangesAsync();
        }

        //Busca todos os dados da tabela genero de filme
        public async Task<ICollection<MovieGenreModel>> ListAll()
        {
            return await _context.movieGenres.ToAsyncEnumerable().ToList();
        }

        //Busca dados da tabela genero de filme pelo id do genero
        public async Task<MovieGenreModel> ListOfId(Guid id)
        {
            return await _context.movieGenres.ToAsyncEnumerable().FirstOrDefault(mg => mg.idGenreMovie == id);
        }

        //Altera os dados da tabela genero de filme
        public async Task<int> Update(MovieGenreModel movieGenre)
        {
            _context.movieGenres.Update(movieGenre);
            return await _context.SaveChangesAsync();
        }
    }
}