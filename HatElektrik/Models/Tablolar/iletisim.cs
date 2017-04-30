using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("iletisim")]
    public class iletisim:BaseEntity
    {
        [Display(Name = "Ad Soyad")]
        [MaxLength(255, ErrorMessage = "Ad Soyad için Çok fazla Karakter Girdiniz!")]
        [Required(ErrorMessage = "Lütfen Ad Soyad Kısmını Doldurunuz")]
        public string AdSoyad { get; set; }

        

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Lütfen Email Kısmını Doldurunuz")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Lütfen geçerli bir email adresi giriniz!!")]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [MaxLength(20, ErrorMessage = "Telefon numarası için çok fazla Karakter girdiniz!")]
        [MinLength(7, ErrorMessage = "Telefon numarası için çok az Karakter girdiniz!")]
        [Required(ErrorMessage = "Lütfen Telefon Kısmını Doldurunuz")]
        public string  TelNo { get; set; }

        [Display(Name = "Konu")]
        [MaxLength(200, ErrorMessage = "Konu  Alanına  Çok Fazla Karakter Girdiniz!")]
        [MinLength(2, ErrorMessage = "Konu Alanı için Çok az Karakter Girdiniz!")]
        public string Konu { get; set; }

        [Display(Name = "Mesaj")]
        [Required(ErrorMessage = "Lütfen Mesaj Kısmını Doldurunuz!")]
        [MaxLength(1500, ErrorMessage = "Mesaj  Alanına  Çok Fazla Karakter Girdiniz!")]
        [MinLength(2, ErrorMessage = "Mesaj Alanı için Çok az Karakter Girdiniz!")]
        public string Mesaj { get; set; }
    }
}