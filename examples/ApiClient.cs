using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System.Text.RegularExpressions;

namespace ReeleezeeAPI
{
	internal sealed class ApiClient
	{
		private string _uri;
		private string _userName;
		private string _pasword;

		private RestClient _restClient = null;
		private RestSerializer _restSerializer => new RestSerializer(NullValueHandling.Ignore);

		public ApiClient(string uri, string userName, string password)
		{
			_uri = uri;
			_userName = userName;
			_pasword = password;
		}

		public Response GET(string resource)
		{
			var request = new RestRequest(resource, Method.GET);
			return new Response(Client.Execute(request));
		}

		public Response PUT(string resource, dynamic data)
		{
			RestRequest request = new RestRequest(resource, Method.PUT);
			request.JsonSerializer = _restSerializer;
			request.RequestFormat = DataFormat.Json;
			request.AddBody(data);
			return new Response(Client.Execute(request));
		}

		private RestClient Client
		{
			get
			{
				InitClient();
				return _restClient;
			}
		}

		private void InitClient()
		{
			if (_restClient == null)
			{
				_restClient = new RestClient(_uri);
				_restClient.ClearHandlers();

				_restClient.AddHandler("application/json", new JsonDeserializer());
				_restClient.Authenticator = new HttpBasicAuthenticator(_userName, _pasword);
				_restClient.AddDefaultHeader("Accept-Language", "en");
				_restClient.AddDefaultHeader("Prefer", "return=representation");
			}
		}
	}

	internal sealed class Response : IRestResponse
	{
		private IRestResponse _response;
		private string _nextLink;

		public Response (IRestResponse response)
		{
			_response = response;
			// Paging support
			var nextlink  = JsonConvert.DeserializeObject<dynamic>(response.Content)["@odata.nextLink"];
			if (nextlink != null)
			{
				Regex r = new Regex("/api/[^/]+(.*)");
				Match m = r.Match(nextlink.ToString());
				if (m.Success)
				{
					_nextLink = m.Groups[1].Value;
				}
			}
		}

		public string NextLink
		{
			get { return _nextLink; }
		}

		public string Content
		{
			get { return _response.Content; }
			set { throw new NotImplementedException(); }
		}

		public string ContentEncoding
		{
			get { return _response.ContentEncoding; }
			set { throw new NotImplementedException(); }
		}

		public long ContentLength
		{
			get { return _response.ContentLength; }
			set { throw new NotImplementedException(); }
		}

		public string ContentType
		{
			get { return _response.ContentType; }
			set { throw new NotImplementedException(); }
		}

		public IList<RestResponseCookie> Cookies
		{
			get { return _response.Cookies; }
		}

		public Exception ErrorException
		{
			get { return _response.ErrorException; }
			set { throw new NotImplementedException(); }
		}

		public string ErrorMessage
		{
			get { return _response.ErrorMessage; }
			set { throw new NotImplementedException(); }
		}

		public IList<Parameter> Headers
		{
			get { return _response.Headers; }
		}

		public byte[] RawBytes
		{
			get { return _response.RawBytes; }
			set { throw new NotImplementedException(); }
		}

		public IRestRequest Request
		{
			get { return _response.Request; }
			set { throw new NotImplementedException(); }
		}

		public ResponseStatus ResponseStatus
		{
			get { return _response.ResponseStatus; }
			set { throw new NotImplementedException(); }
		}

		public Uri ResponseUri
		{
			get { return _response.ResponseUri; }
			set { throw new NotImplementedException(); }
		}

		public string Server
		{
			get { return _response.Server; }
			set { throw new NotImplementedException(); }
		}

		public HttpStatusCode StatusCode
		{
			get { return _response.StatusCode; }
			set { throw new NotImplementedException(); }
		}

		public string StatusDescription
		{
			get { return _response.StatusDescription; }
			set { throw new NotImplementedException(); }
		}
	}
}
