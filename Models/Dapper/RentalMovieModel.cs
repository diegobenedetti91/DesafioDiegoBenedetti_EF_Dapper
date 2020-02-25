using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork;

namespace DesafioDiegoBenedetti.EF_Dapper.Models.Dapper
{
    [Table("RentalMovie",Schema="dbo")]

    // Classe que manipula os dados da tabela RentalMovie para insert,update e delete
    public class RentalMovieModel
    {
        //No construtor busca sempre um novo ID quando for inserir um novo registro
        public RentalMovieModel()
        {
            idRentalMovie = Guid.NewGuid();
        }

        [Key]
        public Guid idRentalMovie { get; set; }

        public virtual MovieModel movie { get; set; }

        public virtual UserModel user { get; set; }

        [Required]
        public DateTime? rentalDate { get; set; }

        [Required]
        [ForeignKey("cpf")]
        public string cpf { get; set; }

        [Required]
        [ForeignKey("idMovie")]
        public Guid idMovie { get; set; }
    }
}