using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("Galeri")]
    public class Galeri:BaseEntity
    {
        [Display(Name = "Başlık")]
        [MinLength(3, ErrorMessage = "Başlık En az 3 karakter olmalıdır!"), MaxLength(255, ErrorMessage = "Başlık Maximum 255 karakter olabilir")]
        public string Baslik { get; set; }


        [Display(Name = "Resim")]
        [Required(ErrorMessage = "Resim Alanını Girmelisiniz! ")]
        public string ResimURL { get; set; }
    }
}