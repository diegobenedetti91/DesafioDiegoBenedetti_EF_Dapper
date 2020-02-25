using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioDiegoBenedetti.EF_Dapper.Models
{
    //Classe criada para manipular os dados das tabelas Filmes e Genero
    public class Movie_RentalViewModel
    {
        public Guid idRentalMovie { get; set; }

        public DateTime? rentalDate { get; set; }

        public string cpf { get; set; }

        public Guid idMovie { get; set; }

        public List<string> movies { get; set; }

        //Campo para fazer a filtragem e buscar o ID do genero
        public string nameMovie { get; set; }

        //Campo Model para manipular e usar o cpf para consutlar os filmes vinculados a locação
        public UserModel user { get; set; }
    }
}
