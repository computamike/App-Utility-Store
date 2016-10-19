namespace Open.GI.hypermart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// Migration - add Installation History Table.
    /// </summary>
    public partial class AddInstallationHistory : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.InstallationHistory",
                c => new
                {
                    userID = c.String(nullable: false, maxLength: 128),
                    ProductID = c.Int(nullable: false),
                    FileID = c.Int(nullable: false),
                    InstallationDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new {t.userID, t.ProductID, t.FileID})
                .ForeignKey("dbo.Files", t => t.FileID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: false)
                .ForeignKey("dbo.User", t => t.userID, cascadeDelete: false)
                .Index(t => t.ProductID);
        }
        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductID", "dbo.Products");
            DropForeignKey("dbo.User", "userID", "dbo.User");
            DropForeignKey("dbo.Files", "FileID", "dbo.Files");
            DropIndex("dbo.InstallationHistory", new[] { "ID" });
            DropTable("dbo.InstallationHistory");

        }
    }
}
