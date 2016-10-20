namespace Open.GI.hypermart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    public partial class Initial : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StorageType = c.Int(nullable: false),
                        FileName = c.String(),
                        BLOB = c.Binary(),
                        Link = c.String(),
                        Version = c.String(maxLength: 50),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 10),
                        Platform1 = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Tagline = c.String(),
                        Lead = c.String(maxLength: 50),
                        SourceCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        userID = c.String(nullable: false, maxLength: 128),
                        RatingCategory = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        rating = c.Double(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => new { t.userID, t.RatingCategory, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Screenshots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ScreenShot1 = c.Binary(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.InstallationHistories",
                c => new
                    {
                        userID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                        InstallationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.userID, t.ProductID, t.FileID });
            
            CreateTable(
                "dbo.PlatformFiles",
                c => new
                    {
                        Platform_ID = c.String(nullable: false, maxLength: 10),
                        File_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_ID, t.File_ID })
                .ForeignKey("dbo.Platforms", t => t.Platform_ID, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.File_ID, cascadeDelete: true)
                .Index(t => t.Platform_ID)
                .Index(t => t.File_ID);
            
        }
        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Screenshots", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Ratings", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Files", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PlatformFiles", "File_ID", "dbo.Files");
            DropForeignKey("dbo.PlatformFiles", "Platform_ID", "dbo.Platforms");
            DropIndex("dbo.PlatformFiles", new[] { "File_ID" });
            DropIndex("dbo.PlatformFiles", new[] { "Platform_ID" });
            DropIndex("dbo.Screenshots", new[] { "ProductID" });
            DropIndex("dbo.Ratings", new[] { "ProductID" });
            DropIndex("dbo.Files", new[] { "ProductID" });
            DropTable("dbo.PlatformFiles");
            DropTable("dbo.InstallationHistories");
            DropTable("dbo.Screenshots");
            DropTable("dbo.Ratings");
            DropTable("dbo.Products");
            DropTable("dbo.Platforms");
            DropTable("dbo.Files");
        }
    }
}
