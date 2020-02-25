using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Repositories.Dapper
{
    public class RentalMovieRepository : IRentalMovieRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration configuration;

        //Toda classe instanciada rentalMovie irá sempre receber o padrão do config para informar a conexão do BD
        public RentalMovieRepository(IConfiguration config)
        {
            configuration = config;            
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Exclui uma locação de filme pelo CPF e o id do filme
        public async Task<int> DeleteRentalMovie(RentalMovieModel rentalMovie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.ExecuteAsync("DELETE FROM RentalMovie WHERE cpf = @cpf and idMovie = @idMovie", rentalMovie);
            }
        }

        //Insert da classe Locação de filmes
        public async Task<int> Add(RentalMovieModel rentalMovie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.ExecuteAsync("INSERT INTO RentalMovie " +
                                    "(idRentalMovie " +
                                    ",rentalDate " +
                                    ",cpf " +
                                    ",idMovie)" +
                            "VALUES " +
                                    "(@idRentalMovie" +
                                    ",@rentalDate" +
                                    ",@cpf" +
                                    ",@idMovie)", rentalMovie);
            }
        }

            // não cpntém método de exclusão e alteração de locação, Pois ao excluir o filme, a chave estrangeira irá
            //excluir a locação, pois o banco de dados está em efeito cascata

        //Busca todos as locações
        public async Task<IEnumerable<RentalMovieModel>> ListAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<RentalMovieModel>("SELECT * FROM RentalMovie");
            }            
        }

        // Busca a locação de filme por ID de locação
        public async Task<RentalMovieModel> ListOfId(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QuerySingleAsync<RentalMovieModel>(string.Format("SELECT * FROM RentalMovie WHERE idRentalMovie = '{0}'",id));
            }
        }
    }
}