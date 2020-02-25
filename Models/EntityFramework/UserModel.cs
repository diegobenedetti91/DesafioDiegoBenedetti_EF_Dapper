using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DesafioDiegoBenedetti.EF_Dapper.Models.Dapper;

namespace DesafioDiegoBenedetti.EF_Dapper.Models.EntityFrameWork
{
    [Table("User",Schema="dbo")]
    // Classe que manipula os dados da tabela User para insert,update e delete
    public class UserModel
    {
        public UserModel()
        {
            createdAt = DateTime.Now;
        }

        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]        
        public string cpf { get; set; }

        [Required]
        [MaxLength(50)]
        public string fullName { get; set; }
                
        [Required]
        [MaxLength(20)]
        public string nameUser { get; set; }

        [Required]
        [MaxLength(50)]
        public string password { get; set; }

        [Required]
        public bool status { get; set; }

        public DateTime? createdAt { get; set; }

        public virtual ICollection<RentalMovieModel> rentalMovie { get; set; }
    }
}