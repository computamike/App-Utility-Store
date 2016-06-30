﻿using Open.GI.hypermart.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_TestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceReference1.StatusSoapClient sc = new ServiceReference1.StatusSoapClient();
            //var s = sc.HelloWorld();
            // Test that the All Products call returns all products
            TestGetAllProducts();

            // Test that a new Product can be called.
            AddProduct();

            // Test that a Rating can be provided.

            Console.ReadLine();
           
        }

        private static void AddProduct()
        {

            var client = new RestClient("http://localhost:12672/api/StoreContent");
            client.Authenticator = new NtlmAuthenticator(System.Net.CredentialCache.DefaultNetworkCredentials);

            Product itemToAdd = new Product();
            itemToAdd.Title = "Open GI Application Store";
            itemToAdd.Description = "A discoverable way to find applications in Open GI";
            itemToAdd.Lead = @"wnet\mhingley";
            itemToAdd.Tagline = "Approved software store";


            var request = new RestRequest("PostProduct", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddObject(itemToAdd);

            IRestResponse response = client.Execute(request);


            Console.WriteLine("Posting to AddProduct");

            Console.WriteLine("response=");
            Console.WriteLine(response.Content);
        }

        private static void TestGetAllProducts()
        {
            var client = new RestClient("http://localhost:12672/api/StoreContent");
            client.Authenticator = new NtlmAuthenticator(System.Net.CredentialCache.DefaultNetworkCredentials);
            var request = new RestRequest("GetAllProducts", Method.GET);
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
