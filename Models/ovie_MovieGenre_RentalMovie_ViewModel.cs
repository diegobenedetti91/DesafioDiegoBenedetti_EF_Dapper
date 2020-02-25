using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using System;

namespace DesafioDiegoBenedetti.EF_Dapper.Models
{
    //Classe para manipular os dados das tabelas Filmes e Locação de Filmes
    public class Movie_MovieGenre_RentalMovie_ViewModel
    {
        public string cpf { get; set; }

        public string name { get; set; }

        public string nameGenre { get; set; }

        public DateTime? dateRental { get; set; }

        //Campo Model para manipular e usar o cpf para consutlar os filmes vinculados a locação
        public UserModel user { get; set; } 
    }
}
