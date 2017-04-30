using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("Urun")]
    public class Urun : BaseEntity
    {
        [Display(Name = "Ürün Başlık")]
        [MaxLength(255, ErrorMessage = "Çok fazla Karakter Girdiniz!")]
        [Required(ErrorMessage = "Lütfen Başlığı giriniz !")]
        public string Baslik { get; set; }

        [Display(Name = "Ürün Kısa Açıklama")]
        public string KisaAciklama { get; set; }

        [Display(Name = "Ürün Açıklama")]
        public string Aciklama { get; set; }


        public int Okunma { get; set; }

        [Display(Name = "Ürün Adet")]
        public string Adet { get; set; }

        [Display(Name = "Ürün Fiyat")]
        public string Fiyat { get; set; }

        public int KategoriID { get; set; }

        [Display(Name = "Ürün Resim")]
        [MaxLength(255, ErrorMessage = "Çok fazla Karakter Girdiniz!")]
        public string ResimURL { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}