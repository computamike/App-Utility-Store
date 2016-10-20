namespace Open.GI.hypermart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// Installation History Migration
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    public partial class Unknown : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.InstallationHistories",
                c => new
                    {
                        userID = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                        InstallationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.userID, t.ProductID, t.FileID })
                .ForeignKey("dbo.Files", t => t.FileID, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: false);
            
        }
        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Files", "FileID", "dbo.Files");
            DropTable("dbo.InstallationHistories");
        }
    }
}
