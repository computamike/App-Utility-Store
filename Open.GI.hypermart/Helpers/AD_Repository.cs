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
    public class AD_Repository
    {
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

        public static string FriendlyDomainToLdapDomain(string friendlyDomainName)
        {
            string ldapPath = null;
            try
            {
                DirectoryContext objContext = new DirectoryContext(
                    DirectoryContextType.Domain, friendlyDomainName);
                Domain objDomain = Domain.GetDomain(objContext);
                ldapPath = objDomain.Name;
            }
            catch (DirectoryServicesCOMException e)
            {
                ldapPath = e.Message.ToString();
            }
            return ldapPath;
        }
        public List<User> GetUsers(string partialName)
        {
            List<User> results = new List<User>();

            var Ldap = FriendlyDomainToLdapDomain("wnet");

            var dcs = EnumerateDomains();

            var ds = new DirectorySearcher();

            DirectoryEntry entry = new DirectoryEntry("LDAP://OU=Users,OU=Development,OU=Departments,DC=wnet,DC=local");
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

        public static User getUser(string partialName)
        {
            if (partialName.Contains('\\'))
            {
                partialName = partialName.Split('\\')[1];
            }
            List<User> results = new List<User>();
            var Ldap = FriendlyDomainToLdapDomain("wnet");
            var dcs = EnumerateDomains();
            var ds = new DirectorySearcher();

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



            foreach (SearchResult item in result)
            {
                User p = new User()
                {
                    username = GetName(item),
                    Photo = GetPhoto(item),
                    PhoneNumnber = GetValue(item, "telephonenumber"),
                    Email = GetValue(item, "mail"),
                    JobTitle = GetValue(item, "title")
                };
                results.Add(p);
            }
            //if (partialName == "mhingley")
            //{
            //    results.Clear();
            //    var mhPhoto = Resources.mhingley;
            //    results.Add(new User() { PhoneNumnber = "07791751829", Photo = mhPhoto, username = "mike hingley" });
            //}


            return results.FirstOrDefault<User>();



        }


    }
}