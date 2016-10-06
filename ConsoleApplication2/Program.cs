using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            DoCall();
            Console.ReadLine();
        }

        private static async Task DoCall()
        {
             string token = "Fail";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // get the list of roles
                using (var response = await client.GetAsync("http://localhost:50699/api/values"))
                {
                    try
                    {
            response.EnsureSuccessStatusCode();

                    var roles = await response.Content.ReadAsAsync<JToken[]>();
                    foreach (var role in roles)
                    {
                        Console.WriteLine(role.Value<string>());
                    }
                    }
                    catch (Exception ex )
                    {
                        Console.WriteLine("Whoops" + ex.ToString());
                        throw;
                    }
        
                }
            }
        }

    }
}
