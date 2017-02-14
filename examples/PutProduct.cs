/*
Licensed under MIT license
(c) 2017 Reeleezee BV
*/
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Net;

namespace ReeleezeeAPI
{
	public partial class ProductsSample
	{
		public static void PutProduct(string uri, string userName, string password)
		{
			try
			{
				string resource = "/Products/";
				var client = new ApiClient(uri, userName, password);
				dynamic Product = new ExpandoObject();

				Guid guid = Guid.NewGuid();
				Product.id = guid;
				Product.Description = "New product from API";
				Product.SearchName = "New API product";
				Product.Comment = "This product is created by the C# API client with id: " + guid.ToString();
				Product.Price = 12.60D;

				Response response = client.PUT(resource + guid.ToString(), Product);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					var product  = JsonConvert.DeserializeObject<dynamic>(response.Content);
					if (product != null)
					{
						Console.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented));
						Console.WriteLine("{0,-38} {1,-40} {2,-20} {3:0.00}",
							product.id,
							MaxString(product.Description, 40),
							MaxString(product.SearchName, 20),
							product.Price);
					}
				}
				else
				{
					Console.WriteLine("Invalid response: {0}, {1}", response.StatusCode, response.Content);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
