namespace HatElektrik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galeri",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 255),
                        ResimURL = c.String(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.iletisim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false),
                        TelNo = c.String(nullable: false, maxLength: 20),
                        Konu = c.String(maxLength: 200),
                        Mesaj = c.String(nullable: false, maxLength: 1500),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kampanya",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 255),
                        KisaAciklama = c.String(),
                        Aciklama = c.String(),
                        Okunma = c.Int(nullable: false),
                        ResimURL = c.String(nullable: false, maxLength: 255),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 150),
                        URL = c.String(maxLength: 150),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Urun",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 255),
                        KisaAciklama = c.String(),
                        Aciklama = c.String(),
                        Okunma = c.Int(nullable: false),
                        Adet = c.String(),
                        Fiyat = c.String(),
                        KategoriID = c.Int(nullable: false),
                        ResimURL = c.String(maxLength: 255),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kategori", t => t.KategoriID, cascadeDelete: true)
                .Index(t => t.KategoriID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(maxLength: 150),
                        Email = c.String(nullable: false),
                        Sifre = c.String(nullable: false, maxLength: 16),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Referans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 255),
                        ResimURL = c.String(nullable: false, maxLength: 255),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 255),
                        Aciklama = c.String(maxLength: 255),
                        ResimURL = c.String(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Urun", "KategoriID", "dbo.Kategori");
            DropIndex("dbo.Urun", new[] { "KategoriID" });
            DropTable("dbo.Slider");
            DropTable("dbo.Referans");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Urun");
            DropTable("dbo.Kategori");
            DropTable("dbo.Kampanya");
            DropTable("dbo.iletisim");
            DropTable("dbo.Galeri");
        }
    }
}
