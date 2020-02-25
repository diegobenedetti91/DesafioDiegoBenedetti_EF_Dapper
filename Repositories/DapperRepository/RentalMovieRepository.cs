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

        //Toda classe instanciada rentalMovie ir� sempre receber o padr�o do config para informar a conex�o do BD
        public RentalMovieRepository(IConfiguration config)
        {
            configuration = config;            
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Exclui uma loca��o de filme pelo CPF e o id do filme
        public async Task<int> DeleteRentalMovie(RentalMovieModel rentalMovie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.ExecuteAsync("DELETE FROM RentalMovie WHERE cpf = @cpf and idMovie = @idMovie", rentalMovie);
            }
        }

        //Insert da classe Loca��o de filmes
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

            // n�o cpnt�m m�todo de exclus�o e altera��o de loca��o, Pois ao excluir o filme, a chave estrangeira ir�
            //excluir a loca��o, pois o banco de dados est� em efeito cascata

        //Busca todos as loca��es
        public async Task<IEnumerable<RentalMovieModel>> ListAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<RentalMovieModel>("SELECT * FROM RentalMovie");
            }            
        }

        // Busca a loca��o de filme por ID de loca��o
        public async Task<RentalMovieModel> ListOfId(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QuerySingleAsync<RentalMovieModel>(string.Format("SELECT * FROM RentalMovie WHERE idRentalMovie = '{0}'",id));
            }
        }
    }
}