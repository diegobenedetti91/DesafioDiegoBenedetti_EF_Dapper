using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;

namespace DesafioDiegoBenedetti.EF_Dapper.Models.Dapper
{
    // Classe que manipula os dados da tabela Movie para insert,update e delete
    [Table("Movie",Schema="dbo")]
    public class MovieModel
    {
        //No construtor busca sempre um novo ID quando for inserir um novo registro
        public MovieModel()
        {
            idMovie = Guid.NewGuid();
            createdAt = DateTime.Now;
        }
        
        [Key]   
        public Guid idMovie { get; set; }

        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        public DateTime? createdAt { get; set; }

        [Required]
        public bool status {get; set; }

        public virtual MovieGenreModel movieGenre { get; set; }
        
        public virtual ICollection<RentalMovieModel> rentalMovies { get; set; }     

        [Required]
        [ForeignKey("idGenreMovie")]
        public Guid idGenreMovie { get; set; }        
    }
}