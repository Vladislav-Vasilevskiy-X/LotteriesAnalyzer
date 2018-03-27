using Newtonsoft.Json;

using RestSharp;
using RestSharp.Deserializers;

namespace Lottery.Rest
{
	public class RestSharpJsonNetDeserializer : IDeserializer
	{
		/// <summary>
		/// Gets or sets the root element.
		/// </summary>
		/// <value>
		/// The root element.
		/// </value>
		public string RootElement { get; set; }

		/// <summary>
		/// Gets or sets the namespace.
		/// </summary>
		/// <value>
		/// The namespace.
		/// </value>
		public string Namespace { get; set; }

		/// <summary>
		/// Gets or sets the date format.
		/// </summary>
		/// <value>
		/// The date format.
		/// </value>
		public string DateFormat { get; set; }

		/// <summary>
		/// Deserializes the specified response.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="response">The response.</param>
		/// <returns>The Deserialize value.</returns>
		public T Deserialize<T>(IRestResponse response)
		{
			return JsonConvert.DeserializeObject<T>(response.Content);
		}
	}
}
