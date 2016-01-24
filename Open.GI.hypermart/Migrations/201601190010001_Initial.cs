namespace Open.GI.hypermart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// Initial Migration - settin up the tables and links
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
                "dbo.FilePlatform",
                c => new
                    {
                        Files_ID = c.Int(nullable: false),
                        Platforms_ID = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => new { t.Files_ID, t.Platforms_ID })
                .ForeignKey("dbo.Files", t => t.Files_ID, cascadeDelete: true)
                .ForeignKey("dbo.Platforms", t => t.Platforms_ID, cascadeDelete: true)
                .Index(t => t.Files_ID)
                .Index(t => t.Platforms_ID);
            
        }

        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Screenshots", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Files", "ProductID", "dbo.Products");
            DropForeignKey("dbo.FilePlatform", "Platforms_ID", "dbo.Platforms");
            DropForeignKey("dbo.FilePlatform", "Files_ID", "dbo.Files");
            DropIndex("dbo.FilePlatform", new[] { "Platforms_ID" });
            DropIndex("dbo.FilePlatform", new[] { "Files_ID" });
            DropIndex("dbo.Screenshots", new[] { "ProductID" });
            DropIndex("dbo.Files", new[] { "ProductID" });
            DropTable("dbo.FilePlatform");
            DropTable("dbo.Screenshots");
            DropTable("dbo.Products");
            DropTable("dbo.Platforms");
            DropTable("dbo.Files");
        }
    }
}
