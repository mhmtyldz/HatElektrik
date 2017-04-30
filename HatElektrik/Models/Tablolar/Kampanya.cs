using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("Kampanya")]
    public class Kampanya:BaseEntity
    {
        [Display(Name = "Kampanya Başlık")]
        [MaxLength(255, ErrorMessage = "Çok fazla Karakter Girdiniz!")]
        [Required(ErrorMessage = "Lütfen Kampanya Başlığını giriniz !")]
        public string Baslik { get; set; }

        [Display(Name = "Kampanya Kısa Açıklama")]
        public string KisaAciklama { get; set; }

        [Display(Name = "Kampanya Açıklama")]
        public string Aciklama { get; set; }

        public int Okunma { get; set; }

        [Display(Name = "Kampanya Resim")]
        [MaxLength(255, ErrorMessage = "Çok fazla Karakter Girdiniz!")]
        [Required(ErrorMessage="Kampanya Resmi Eklemek Zorundasınız!")]
        public string ResimURL { get; set; }


    }
}