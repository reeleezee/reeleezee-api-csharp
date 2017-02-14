/*
Licensed under MIT license
(c) 2017 Reeleezee BV
*/
using Newtonsoft.Json;
using System;
using System.Net;

namespace ReeleezeeAPI
{
	public partial class ProductsSample
	{
		public static void GetProducts(string uri, string userName, string password)
		{
			try
			{
				string resource = "/Products";
				var client = new ApiClient(uri, userName, password);
				Response response;

				do
				{
					response = client.GET(resource);
					if (response.StatusCode == HttpStatusCode.OK)
					{
						var products  = JsonConvert.DeserializeObject<dynamic>(response.Content).value;
						if (products != null)
						{
							foreach (dynamic product in products)
							{
								Console.WriteLine("{0,-38} {1,-40} {2,-20} {3:0.00}",
									product.id,
									MaxString(product.Description, 40),
									MaxString(product.SearchName, 20),
									product.Price);

							}
							resource = response.NextLink;
						}
					}
					else
					{
						Console.WriteLine("Invalid response: {0}, {1}", response.StatusCode, response.Content);
					}
				} while (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.NextLink));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static string MaxString(object data, int length)
		{
			if (data != null)
			{
				string s = data.ToString();
				return s.Substring(0, Math.Min(s.Length, length));
			}
			return "";
		}
	}
}
