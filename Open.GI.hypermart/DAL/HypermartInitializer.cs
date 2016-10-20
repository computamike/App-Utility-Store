using Open.GI.hypermart.Models;
using Open.GI.hypermart.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Open.GI.hypermart.DAL
{
    /// <summary>
    /// Hypermart Initializer
    /// </summary>
    public class HypermartInitializer :DropCreateDatabaseIfModelChanges<HypermartContext>
    {
        /// <summary>
        /// Seeds the database with content.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(HypermartContext context)
        {
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
            platforms.ForEach(platform =>context.Platforms.Add(platform ));

 
            var findWindowsPlatform = platforms.Where(f=>f.Platform1 =="Windows").First();

            var OSC1 = new File{
                StorageType = storageType.RemoteShare ,
                FileName = "OpenSuiteClient.msi", 
                Link=@"\\bsdrel\thearchives\OpenSuiteClient\5.1.0\Cut03\OpenSuiteClient.msi",
                Platforms = new List<Platform>{platforms.Where(f=>f.Platform1 =="Windows").First(),platforms.Where(f=>f.Platform1 =="Linux").First()}
            };

            context.SaveChanges();


            var testProduct = new Product()
            {   ID = 1,
                Lead = "mhingley",
                Description = "Desc",
                Tagline = "Teg line",
                Title = "Title",
                SourceCode = "http://github.com"
            };

            
            context.Products.Add(testProduct);

            var products = new List<Product>
            {
                new Product
                { 
                    Title = "Open Suite Client", 
                    Tagline="Access and integrate OGI and Office", 
                    Description = "THIS IS A COOL APP - EVERYONE SHOULD DOWNLOAD IT",
                    Lead="mhingley",
                    MyRating = new List<RatingDetails>(),
                    AverageRating = new List<RatingDetails>(),
                    Screenshots = 
                    {
                        new Screenshot{ScreenShot1  =Properties.Resources.Download1.ImageToByteArray()},
                        new Screenshot{ScreenShot1  =Properties.Resources.Download2.ImageToByteArray()},
                        new Screenshot{ScreenShot1  =Properties.Resources.download3.ImageToByteArray()},

                    },
                    Files = 
                    {
                        new File
                        {
                            StorageType = storageType.RemoteShare,
                            FileName = "OpenSuiteClient.msi", 
                            Link=@"\\bsdrel\thearchives\OpenSuiteClient\5.1.0\Cut03\OpenSuiteClient.msi",
                            Platforms = new List<Platform>
                            {
                                platforms.Where(f=>f.Platform1 =="Windows").First(),
                                platforms.Where(f=>f.Platform1 =="Linux").First()
                            }
                        },
                        new File
                        {
                            StorageType = storageType.RemoteShare,
                            FileName = "OpenSuiteClient.msi", 
                            Link=@"\\bsdrel\thearchives\OpenSuiteClient\5.1.0\Cut02\OpenSuiteClient.msi",
                            Platforms = new List<Platform>
                            {
                                platforms.Where(f=>f.Platform1 =="Windows").First(),
                            }
                        }
                    }
                }
            };

            products.ForEach(p => context.Products.Add(p));
       
 
        }
    }
}