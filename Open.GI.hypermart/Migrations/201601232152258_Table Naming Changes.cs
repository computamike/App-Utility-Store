namespace Open.GI.hypermart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// Renaming tables and columns
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    public partial class TableNamingChanges : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            RenameTable(name: "dbo.FilePlatform", newName: "PlatformFiles");
            RenameColumn(table: "dbo.PlatformFiles", name: "Files_ID", newName: "File_ID");
            RenameColumn(table: "dbo.PlatformFiles", name: "Platforms_ID", newName: "Platform_ID");
            RenameIndex(table: "dbo.PlatformFiles", name: "IX_Platforms_ID", newName: "IX_Platform_ID");
            RenameIndex(table: "dbo.PlatformFiles", name: "IX_Files_ID", newName: "IX_File_ID");
            DropPrimaryKey("dbo.PlatformFiles");
            AddPrimaryKey("dbo.PlatformFiles", new[] { "Platform_ID", "File_ID" });
        }
        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropPrimaryKey("dbo.PlatformFiles");
            AddPrimaryKey("dbo.PlatformFiles", new[] { "Files_ID", "Platforms_ID" });
            RenameIndex(table: "dbo.PlatformFiles", name: "IX_File_ID", newName: "IX_Files_ID");
            RenameIndex(table: "dbo.PlatformFiles", name: "IX_Platform_ID", newName: "IX_Platforms_ID");
            RenameColumn(table: "dbo.PlatformFiles", name: "Platform_ID", newName: "Platforms_ID");
            RenameColumn(table: "dbo.PlatformFiles", name: "File_ID", newName: "Files_ID");
            RenameTable(name: "dbo.PlatformFiles", newName: "FilePlatform");
        }
    }
}
