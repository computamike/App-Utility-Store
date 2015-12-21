namespace Open.GI.hypermart.Migrations
{
    using Open.GI.hypermart.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Open.GI.hypermart.DAL.HypermartContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Open.GI.hypermart.DAL.HypermartContext";
        }

        protected override void Seed(Open.GI.hypermart.DAL.HypermartContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            // Add platforms - at this stage add generic (such as WINDOWS) and specific (such as WINDOWS 32 BIT) - this might change
            var platforms = new List<Platform>
            {
                new Platform{ID = "Windows",Platform1 = "Windows"},
                new Platform{ID = "Win_32",Platform1 = "Windows (32 bit)"},
                new Platform{ID = "Win_64",Platform1 = "Windows (64 bit)"},
                                
                new Platform{ID = "Win_8_32",Platform1 = "Windows 8 (32 bit)"},
                new Platform{ID = "Win_8_64",Platform1 = "Windows 8 (64 bit)"},
                                
                new Platform{ID = "Win_10_32",Platform1 = "Windows 10 (32 bit)"},
                new Platform{ID = "Win_10_64",Platform1 = "Windows 10 (64 bit)"},
                
                new Platform{ID = "Linux",Platform1 = "Linux"},
                new Platform{ID = "Linux32",Platform1 = "Linux (32 bit)"},
                new Platform{ID = "Linux64",Platform1 = "Linux (32 bit)"},
               
                new Platform{ID = "Apple",Platform1 = "Apple"}


            };
            platforms.ForEach(platform => context.Platforms.Add(platform));
            
        }
    }
}
