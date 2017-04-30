using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("Kategori")]
    public class Kategori:BaseEntity
    {
        [MinLength(2, ErrorMessage = "Kategori Adı Alanı 2 karakterden az olamaz"), MaxLength(150, ErrorMessage = "Kategori Adı Alanı 150 Karakterden fazla girilemez")]
        [Required]
        public string KategoriAdi { get; set; }

        [MinLength(2, ErrorMessage = "Kategori URL Alanı 2 karakterden az olamaz"), MaxLength(150, ErrorMessage = "Kategori URL Alanı 150 Karakterden fazla girilemez")]
        public string URL { get; set; }


        public virtual List<Urun> Urun { get; set; }
    }
}