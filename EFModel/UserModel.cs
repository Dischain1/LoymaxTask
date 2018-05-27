using System;
using System.ComponentModel.DataAnnotations;

namespace EFModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Patronymic { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
