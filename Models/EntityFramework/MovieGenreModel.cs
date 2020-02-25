using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;

namespace DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork
{
    [Table("MovieGenre",Schema="dbo")]
    // Classe que manipula os dados da tabela MovieGenre para insert,update e delete
    public class MovieGenreModel
    {
        //No construtor busca sempre um novo ID quando for inserir um novo registro
        public MovieGenreModel()
        {
            idGenreMovie = Guid.NewGuid();
        }

        [Key]
        public Guid idGenreMovie { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        public DateTime? createdAt { get; set; }

        [Required]
        public bool ativo { get; set; }

        public virtual ICollection<MovieModel> MovieModels { get; set; }
    }
}