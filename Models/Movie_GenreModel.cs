using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;
using System.Collections.Generic;
using System;

namespace DesafioDiegoBenedetti.EF_Dapper.Models
{
    // Esta classe model foi criada para realizar a manipulação da CRUD da tela de listagem, exclusão inserção e locação de filmes
    public partial class Movie_GenreModel
    {
        public Guid idMovie { get; set; }
        public string nameMovie { get; set; }

        public DateTime? createdAt { get; set; }

        public bool status { get; set; }

        public Guid idGenreMovie { get; set; }

        public List<string> genre { get; set; }

        public string movieGenre { get; set; }

        public string cpf { get; set; }

        public DateTime? dateRental { get; set; }

        public bool isChecked { get; set; }
    }

    public class Movie_Genrel
    {
        public List<Movie_GenreModel> movie_GenrelModel { get; set; }
    }
}
