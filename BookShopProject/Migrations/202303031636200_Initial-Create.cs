namespace BookShopProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false, maxLength: 40),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(nullable: false, maxLength: 40),
                        Picture = c.String(nullable: false, maxLength: 40),
                        PublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AuthorId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        PublishDate = c.DateTime(nullable: false, storeType: "date"),
                        TotalPage = c.Int(nullable: false),
                        AvaiableStock = c.Boolean(nullable: false),
                        CoverPage = c.String(nullable: false, maxLength: 50),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        OrderDate = c.DateTime(nullable: false, storeType: "date"),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(nullable: false, maxLength: 40),
                        WebUrl = c.String(nullable: false, maxLength: 40),
                        Phone = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.PublisherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Authors", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Orders", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Orders", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Authors", new[] { "PublisherId" });
            DropTable("dbo.Publishers");
            DropTable("dbo.Orders");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
