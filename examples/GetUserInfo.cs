/*
Licensed under MIT license
(c) 2017 Reeleezee BV
*/
using Newtonsoft.Json;
using System;
using System.Net;

namespace ReeleezeeAPI
{
	public class UserInfoSample
	{
		public static void GetUserInfo(string uri, string userName, string password)
		{
			try
			{
				string resource = "/UserInfo?$expand=*";
				var client = new ApiClient(uri, userName, password);
				Response response = client.GET(resource);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					Console.WriteLine(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(response.Content), Formatting.Indented));
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
