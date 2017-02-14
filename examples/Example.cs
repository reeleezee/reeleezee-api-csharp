/*
Licensed under MIT license
(c) 2017 Reeleezee BV
*/

namespace ReeleezeeAPI
{
	class Example
	{
		static void Main(string[] args)
		{
			bool getUserInfo = true;
			bool getProducts = false;
			bool putProduct = false;

			if (getUserInfo)
			{
				UserInfoSample.GetUserInfo(Settings.Default.Uri, Settings.Default.UserName, Settings.Default.Password);
			}

			if (getProducts)
			{
				ProductsSample.GetProducts(Settings.Default.Uri, Settings.Default.UserName, Settings.Default.Password);
			}

			if (putProduct)
			{
				ProductsSample.PutProduct(Settings.Default.Uri, Settings.Default.UserName, Settings.Default.Password);
			}
		}
	}
}
