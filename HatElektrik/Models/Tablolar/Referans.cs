using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("Referans")]
    public class Referans:BaseEntity
    {
        [Display(Name = "Referans Başlık")]
        [MaxLength(255, ErrorMessage = "Çok fazla Referans Başlık Karakteri Girdiniz!")]
        [Required(ErrorMessage = "Lütfen Referans Başlığını giriniz !")]
        public string Baslik { get; set; }


        [Display(Name = "Referans Resim")]
        [MaxLength(255, ErrorMessage = "Çok fazla Karakter Girdiniz!")]
        [Required(ErrorMessage = "Referans Resmi Eklemek Zorundasınız!")]
        public string ResimURL { get; set; }
    }
}