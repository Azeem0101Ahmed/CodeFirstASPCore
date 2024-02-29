﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstASPCore.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Column ("StudentName",TypeName ="varchar(100)")]
        [Required]
        public string Name { get; set; }
        [Required]
        [Column("StudentGender",TypeName ="varchar(20)")]
        public string Gender { get; set; }
        [Required]
        public int? Age { get; set; }
        [Required]
        public int Standard { get; set; }
       
    }
}
