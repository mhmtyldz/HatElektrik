using HatElektrik.Models.Tablolar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HatElektrik.Models.DataContext
{
    public class HatElektrikContext:DbContext
    {
        public DbSet<Kullanici> Kullanici { get; set; }

        public DbSet<Kategori> Kategori { get; set; }

        public DbSet<Urun> Urun { get; set; }

        public DbSet<Slider> Slider { get; set; }

        public DbSet<Kampanya> Kampanya { get; set; }

        public DbSet<Referans> Referans { get; set; }

        public DbSet<Galeri> Galeri { get; set; }

        public DbSet<iletisim> iletisim { get; set; }
    }
}