using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.Tablolar
{
    [Table("Kullanici")]
    public class Kullanici:BaseEntity
    {
        [MaxLength(150, ErrorMessage = "Lütfen 50 karakterden fazla değer girmeyiniz ! ")]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Lütfen Email Kısmını Doldurunuz")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Lütfen geçerli bir email adresi giriniz!!")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lütfen Şifre Kısmını Doldurunuz")]
        [MaxLength(16, ErrorMessage = "Lütfen 16 karakterden fazla değer girmeyiniz ! ")]
        public string Sifre { get; set; }

       
    }
}