namespace Open.GI.hypermart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    /// <summary>
    /// Add Ratings Tables.
    /// </summary>
    /// <seealso cref="System.Data.Entity.Migrations.DbMigration" />
    public partial class AddRatings : DbMigration
    {
        /// <summary>
        /// Operations to be performed during the upgrade process.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        userID = c.String(nullable: false, maxLength: 128),
                        RatingCategory = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => new { t.userID, t.RatingCategory, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        /// <summary>
        /// Operations to be performed during the downgrade process.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "ProductID", "dbo.Products");
            DropIndex("dbo.Ratings", new[] { "ProductID" });
            DropTable("dbo.Ratings");
        }
    }
}
