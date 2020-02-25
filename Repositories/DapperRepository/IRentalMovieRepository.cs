using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper
{    //Cria��o do CRUD Loca��o
    public interface IRentalMovieRepository
    {
        Task<int> Add(RentalMovieModel rentalMovie);

        Task<int> DeleteRentalMovie(RentalMovieModel rentalMovie);

        Task<IEnumerable<RentalMovieModel>> ListAll();

        Task<RentalMovieModel> ListOfId(Guid id);
    }
}