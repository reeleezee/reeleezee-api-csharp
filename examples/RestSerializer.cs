using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace ReeleezeeAPI
{
	internal sealed class RestSerializer : ISerializer
	{
		#region Fields

		private readonly Newtonsoft.Json.JsonSerializer _serializer;

		#endregion

		#region Life-cycle

		public RestSerializer(Newtonsoft.Json.NullValueHandling nullValueHandling, bool logJson = false)
		{
			ContentType = "application/json";
			_serializer = new Newtonsoft.Json.JsonSerializer
				{
					MissingMemberHandling = MissingMemberHandling.Ignore,
					NullValueHandling = nullValueHandling,
					DefaultValueHandling = DefaultValueHandling.Include
				};
		}

		/// <summary>
		/// Default serializer with overload for allowing custom Json.NET settings
		/// </summary>
		public RestSerializer(Newtonsoft.Json.JsonSerializer serializer)
		{
			ContentType = "application/json";
			_serializer = serializer;
		}

		#endregion

		#region Interface ISerializer

		/// <summary>
		/// Serialize the object as JSON
		/// </summary>
		/// <param name="obj">Object to serialize</param>
		/// <returns>JSON as String</returns>
		public string Serialize(object obj)
		{
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = Formatting.Indented;
				jsonTextWriter.QuoteChar = '"';
				_serializer.Serialize(jsonTextWriter, obj);
				string result = stringWriter.ToString();

				return result;
			}
		}

		/// <summary>
		/// Unused for JSON Serialization
		/// </summary>
		public string DateFormat { get; set; }
		/// <summary>
		/// Unused for JSON Serialization
		/// </summary>
		public string RootElement { get; set; }
		/// <summary>
		/// Unused for JSON Serialization
		/// </summary>
		public string Namespace { get; set; }
		/// <summary>
		/// Content type for serialized content
		/// </summary>
		public string ContentType { get; set; }

		#endregion
	}
}