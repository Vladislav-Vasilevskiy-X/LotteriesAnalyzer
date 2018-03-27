using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lottery.Rest
{
	public class Connector
	{
		private RestClient RestClient
		{
			get
			{
				var client = new RestClient("https://www.stoloto.ru/draw-results/5x36plus/load");
				client.AddHandler("application/json", new RestSharpJsonNetDeserializer());
				return client;
			}
		}

		public string GetStoloto5x36HisttoryPageData(int number)
		{
			var request = CreateRestRequest("?page={page}", Method.POST);
			request.AddParameter("page", number.ToString(), ParameterType.UrlSegment);
			var response = this.ExecuteRequest(request);

			//var client = new RestClient("");
			//var jsonDeserializer = new JsonDeserializer();
			//client.AddHandler("application/json", jsonDeserializer);
			//var request = new RestRequest("?page={page}", Method.POST);
			//request.AddParameter("page", number.ToString(), ParameterType.UrlSegment);
			//request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
			//var queryResult = client.Execute(request).Content;
			//var xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(queryResult), new XmlDictionaryReaderQuotas()));
			//var xml = XDocument.Parse(queryResult);


			//var xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(queryResult), new XmlDictionaryReaderQuotas()));
			//var xml = XDocument.Parse(queryResult);

			return response.Content;
		}

		public IRestResponse ExecuteRequest(IRestRequest request, HttpStatusCode expectedHttpStatusCode = HttpStatusCode.OK)
		{
			var response = this.RestClient.Execute(request);
			
			Assert.AreNotEqual(0, response.StatusCode);
			Assert.AreEqual(expectedHttpStatusCode, response.StatusCode);
			return response;
		}

		public IRestResponse ExecuteRequest(IRestRequest request)
		{
			var response = this.RestClient.Execute(request);

			Assert.AreNotEqual(0, response.StatusCode);
			return response;
		}

		/// <summary>
		/// Creates the rest request.
		/// </summary>
		/// <param name="resource">The resource path.</param>
		/// <param name="method">The method.</param>
		/// <returns>
		/// Created <see cref="RestRequest" /> instance.
		/// </returns>
		public RestRequest CreateRestRequest(string resource, Method method)
		{
			var restRequest = new RestRequest(resource, method) { JsonSerializer = new RestSharpJsonNetSerializer()};
			
			return restRequest;
		}
	}
}
