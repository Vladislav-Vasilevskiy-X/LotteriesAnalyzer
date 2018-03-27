using System.IO;

using Newtonsoft.Json;

using RestSharp.Serializers;

using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Lottery.Rest
{
	public class RestSharpJsonNetSerializer : ISerializer
	{
		private readonly JsonSerializer serializer;

		public RestSharpJsonNetSerializer()
		{
			this.ContentType = "application/json";
			this.serializer = new JsonSerializer
			{
				MissingMemberHandling = MissingMemberHandling.Ignore,
				NullValueHandling = NullValueHandling.Include,
				DefaultValueHandling = DefaultValueHandling.Include
			};
		}

		public RestSharpJsonNetSerializer(JsonSerializer serializer)
		{
			this.ContentType = "application/json";
			this.serializer = serializer;
		}

		public string DateFormat { get; set; }

		public string RootElement { get; set; }

		public string Namespace { get; set; }
		public string ContentType { get; set; }

		/// <summary>
		/// <see cref="Serialize"/> the object as JSON.
		/// </summary>
		/// <param name="obj">Object to serialize.</param>
		/// <returns>
		/// JSON as String.
		/// </returns>
		public string Serialize(object obj)
		{
			using (var stringWriter = new StringWriter())
			{
				using (var jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.Formatting = Formatting.Indented;
					jsonTextWriter.QuoteChar = '"';

					this.serializer.Serialize(jsonTextWriter, obj);

					var result = stringWriter.ToString();
					return result;
				}
			}
		}
	}
}