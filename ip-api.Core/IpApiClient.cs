using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ip_api.Core.Models;
using RestSharp;
using ip_api.Core.AppCode;
using RestSharp.Serialization.Json;
using RestSharp.Deserializers;

namespace ip_api.Core
{
	public class IpApiClient
	{
		string baseUri = "http://ip-api.com/";

		readonly bool _https;

		public LanguageOpition _languageOpition;
		public FieldOption[] _fields;

		public IpApiClient([Optional] bool https, [Optional] LanguageOpition languageOpition, [Optional] FieldOption[] fields)
		{
			_https = https;
			_languageOpition = languageOpition;
			_fields = fields;
		}

		public T Execute<T>(RestRequest request) where T : new()
		{
			var client = new RestClient();

			if (_https)
			{
				client.BaseUrl = new UriBuilder(baseUri) { Scheme = Uri.UriSchemeHttps, Port = -1 }.Uri;
			}
			else
			{
				client.BaseUrl = new Uri(baseUri);
			}

			var response = client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response. Check inner details for more info.";
				throw new ApplicationException(message, response.ErrorException);
			}
			IDeserializer deserializer = new JsonDeserializer();
			if (typeof(T).Name == typeof(List<>).Name)
			{
				// Check for error object
				List<ErrorResponse> error = deserializer.Deserialize<List<ErrorResponse>>(response);

				if (error == null || error.Count == 0 || error[0].Status != "success")
				{
					throw new ApplicationException(error[0].Message);
				}
			}
			else
			{
				// Check for error object
				ErrorResponse error = deserializer.Deserialize<ErrorResponse>(response);

				if (error.Status != "success")
				{
					throw new ApplicationException(error.Message);
				}
			}
			return deserializer.Deserialize<T>(response);
		}

		public IpAddressDetails GetIpAddressDetails(string ipAddress, [Optional] LanguageOpition language, [Optional] params FieldOption[] fields)
		{
			var request = new RestRequest();
			request.AddParameter("IpAddress", ipAddress, ParameterType.UrlSegment);
			request.Resource = "json/{IpAddress}";
			// Add optional parameters
			if (fields != null && fields.Length > 0)
			{
				string fieldsString = "";
				foreach (var item in fields)
				{
					fieldsString += ((fieldsString != "") ? "," : "") + item.GetDescription();
				}
				if (!fieldsString.Contains(FieldOption.Status.GetDescription()))
					fieldsString += "," + FieldOption.Status.GetDescription();
				request.AddParameter("fields", fieldsString);
			}
			else if (_fields != null && _fields.Length > 0)
			{
				string fieldsString = "";
				foreach (var item in _fields)
				{
					fieldsString += ((fieldsString != "") ? "," : "") + item.GetDescription();
				}
				if (!fieldsString.Contains(FieldOption.Status.GetDescription()))
					fieldsString += "," + FieldOption.Status.GetDescription();
				request.AddParameter("fields", fieldsString);
			}

			if (language != LanguageOpition.English)
			{
				request.AddParameter("lang", language.GetDescription());
			}
			else if (_languageOpition != LanguageOpition.English)
			{
				request.AddParameter("lang", _languageOpition.GetDescription());
			}

			return Execute<IpAddressDetails>(request);
		}

		public List<IpAddressDetails> GetIpAddressDetails(List<string> ipAddresses, [Optional] LanguageOpition language, [Optional] params FieldOption[] fields)
		{
			if (ipAddresses.Count > 100)
			{
				const string message = "Error calling api. Total 100 IP Addressess allowed in a single batch.";
				throw new ApplicationException(message);
			}
			var request = new RestRequest();
			request.Method = Method.POST;
			request.RequestFormat = DataFormat.Json;
			request.AddBody(ipAddresses.ToArray());
			request.Resource = "batch";

			// Add optional parameters
			if (fields != null && fields.Length > 0)
			{
				string fieldsString = "";
				foreach (var item in fields)
				{
					fieldsString += ((fieldsString != "") ? "," : "") + item.GetDescription();
				}
				if (!fieldsString.Contains(FieldOption.Status.GetDescription()))
					fieldsString += "," + FieldOption.Status.GetDescription();
				request.AddParameter("fields", fieldsString);
			}
			else if (_fields != null && _fields.Length > 0)
			{
				string fieldsString = "";
				foreach (var item in _fields)
				{
					fieldsString += ((fieldsString != "") ? "," : "") + item.GetDescription();
				}
				if (!fieldsString.Contains(FieldOption.Status.GetDescription()))
					fieldsString += "," + FieldOption.Status.GetDescription();
				request.AddParameter("fields", fieldsString);
			}

			if (language != LanguageOpition.English)
			{
				request.AddParameter("lang", language.GetDescription());
			}
			else if (_languageOpition != LanguageOpition.English)
			{
				request.AddParameter("lang", _languageOpition.GetDescription());
			}
			return Execute<List<IpAddressDetails>>(request);
		}

		public IpAddressDetails GetRequesterIpAddressDetails([Optional] LanguageOpition language, [Optional] params FieldOption[] fields)
		{
			var request = new RestRequest();
			request.Resource = "json";
			// Add optional parameters
			if (fields != null && fields.Length > 0)
			{
				string fieldsString = "";
				foreach (var item in fields)
				{
					fieldsString += ((fieldsString != "") ? "," : "") + item.GetDescription();
				}
				if (!fieldsString.Contains(FieldOption.Status.GetDescription()))
					fieldsString += "," + FieldOption.Status.GetDescription();
				request.AddParameter("fields", fieldsString);
			}
			else if (_fields != null && _fields.Length > 0)
			{
				string fieldsString = "";
				foreach (var item in _fields)
				{
					fieldsString += ((fieldsString != "") ? "," : "") + item.GetDescription();
				}
				if (!fieldsString.Contains(FieldOption.Status.GetDescription()))
					fieldsString += "," + FieldOption.Status.GetDescription();
				request.AddParameter("fields", fieldsString);
			}

			if (language != LanguageOpition.English)
			{
				request.AddParameter("lang", language.GetDescription());
			}
			else if (_languageOpition != LanguageOpition.English)
			{
				request.AddParameter("lang", _languageOpition.GetDescription());
			}
			return Execute<IpAddressDetails>(request);
		}
	}
}
