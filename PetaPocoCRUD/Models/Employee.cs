using System;
using System.ComponentModel.DataAnnotations;

namespace PetaPocoCRUD.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string firstName { get; set; }

        [Required]
        [StringLength(200)]
        public string lastName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int age { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}