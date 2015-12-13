using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using Open.GI.hypermart.Models;
using System.DirectoryServices.ActiveDirectory;
namespace Open.GI.hypermart.Helpers
{
    /// <summary>
    /// Active Directory Repository
    /// </summary>
    public class AD_Repository
    {
        private static Open.GI.hypermart.Models.User Resolved = new Models.User
        {
            Email = "",
            username = "User Not Found",
            Photo = Properties.Resources.ImageNotFound,
            JobTitle = "Not Found",
            PhoneNumnber = ""
        };
        private static string GetName(SearchResult item)
        {
            string x = "Unknown";
            foreach (System.Collections.DictionaryEntry propertitem in item.Properties)
            {
                var f = (string)propertitem.Key;
                if (f == "name")
                {
                    var res = (System.DirectoryServices.ResultPropertyValueCollection)(propertitem.Value);
                    x = (string)res[0];
                }
            }
            return x;
        }

        private static string GetValue(SearchResult item, string Setting)
        {
            string x = "Unknown";
            foreach (System.Collections.DictionaryEntry propertitem in item.Properties)
            {
                var f = (string)propertitem.Key;
                if (f == Setting)
                {
                    var res = (System.DirectoryServices.ResultPropertyValueCollection)(propertitem.Value);
                    x = (string)res[0];
                }
            }
            return x;
        }


        private static Image GetPhoto(SearchResult item)
        {
           
            Image x = Properties.Resources.ImageNotFound;
            foreach (System.Collections.DictionaryEntry propertitem in item.Properties)
            {
                var f = (string)propertitem.Key;
                if (f.Contains("photo"))
                {
                    var res = (System.DirectoryServices.ResultPropertyValueCollection)(propertitem.Value);
                    var ba = res[0];
                    x = (Bitmap)((new ImageConverter()).ConvertFrom(ba));
                }
            }
            return x;
        }
        /// <summary>
        /// Enumerates the domains.
        /// </summary>
        /// <returns></returns>
        public static ArrayList EnumerateDomains()
        {
            ArrayList alGCs = new ArrayList();
            Forest currentForest = Forest.GetCurrentForest();
            foreach (GlobalCatalog gc in currentForest.GlobalCatalogs)
            {
                alGCs.Add(gc.Name);
            }
            return alGCs;
        }

        /// <summary>
        /// Friendlies the domain to LDAP domain.
        /// </summary>
        /// <param name="friendlyDomainName">Name of the friendly domain.</param>
        /// <returns></returns>
        public static string FriendlyDomainToLdapDomain(string friendlyDomainName)
        {
            DirectoryContext objContext = new DirectoryContext(
                DirectoryContextType.Domain, friendlyDomainName);
            Domain objDomain = Domain.GetDomain(objContext);
            return objDomain.Name;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="partialName">The partial name.</param>
        /// <returns></returns>
        public List<User> GetUsers(string partialName)
        {
            List<User> results = new List<User>();

            //var Ldap = FriendlyDomainToLdapDomain("wnet");
            //var dcs = EnumerateDomains();
            //var ds = new DirectorySearcher();


            DirectoryEntry rootEntry = new DirectoryEntry("LDAP://RootDSE");
            String str = (string)rootEntry.Properties["defaultNamingContext"][0];
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + str);
            DirectorySearcher mySearcher = new DirectorySearcher(entry)
            {
                SearchScope = SearchScope.Subtree,
                Filter = "(&(objectClass=user)(name=*" + partialName + "*))"
            };


            SearchResultCollection result = mySearcher.FindAll();


            foreach (SearchResult item in result)
            {
                User p = new User()
                {
                    username = GetName(item),
                    Photo = GetPhoto(item)
                };
                results.Add(p);
            }
            return results;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="partialName">The partial name.</param>
        /// <returns>In the event that the user cannot be resolved, then an Empty user will be returned.</returns>
        public static User getUser(string partialName)
        {
            Resolved.username = partialName;
            try
            {
                if (partialName.Contains('\\'))
                {
                    partialName = partialName.Split('\\')[1];
                }
                List<User> results = new List<User>();
                //var Ldap = FriendlyDomainToLdapDomain("wnet");
                //var dcs = EnumerateDomains();
                //var ds = new DirectorySearcher();

                DirectoryEntry rootEntry = new DirectoryEntry("LDAP://RootDSE");
                String str = (string)rootEntry.Properties["defaultNamingContext"][0];
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + str);

                //DirectoryEntry entry = new DirectoryEntry("LDAP://OU=Users,OU=Development,OU=Departments,DC=wnet,DC=local");
                DirectorySearcher mySearcher = new DirectorySearcher(entry)
                {
                    SearchScope = SearchScope.Subtree,
                    Filter = "(&(objectClass=user)(sAMAccountName=" + partialName + "))"
                };
                
                SearchResultCollection result = mySearcher.FindAll();

                if (result.Count>=1 )
                {
                    return new User()
                    {
                        username = GetName(result[0]),
                        Photo = GetPhoto(result[0]),
                        PhoneNumnber = GetValue(result[0], "telephonenumber"),
                        Email = GetValue(result[0], "mail"),
                        JobTitle = GetValue(result[0], "title")
                    };

                    
                }

                return Resolved;
            }
            catch (Exception)
            {
                return Resolved;
            }







        }


    }
}