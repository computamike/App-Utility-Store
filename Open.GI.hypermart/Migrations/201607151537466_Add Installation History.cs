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
                    ID = c.Int(nullable: false, identity: true),
                    InstallationDate = c.DateTime(nullable: false),
                    FileID = c.Int(nullable : false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Files", t => t.FileID, cascadeDelete: true)
                .Index(t => t.ID);
        }
        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Files", "FileID", "dbo.Files");
            DropIndex("dbo.InstallationHistory", new[] { "ID" });
            DropTable("dbo.InstallationHistory");

        }
    }
}
