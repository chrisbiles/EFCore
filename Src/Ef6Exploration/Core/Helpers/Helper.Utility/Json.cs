using Helper.Utility.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using HttpMethod = Helper.Utility.Enums.HttpMethod;

namespace Helper.Utility;

public static class Json
{
	#region Get Methods
	public static string Get(string url)
	{
		return ProcessJson(url, HttpMethod.GET);
	}

	public static string Get(string url, string userName, string password, bool preAuthenticate = true)
	{
		return ProcessJson(url, HttpMethod.GET, null, userName, password, preAuthenticate);
	}

	public static string Get(string url, IDictionary<string, string> headerValues)
	{
		return ProcessJson(url, HttpMethod.GET, null, null, null, false, headerValues);
	}

	public static T GetJsonObject<T>(T entity, string url) where T : class
	{
		return JsonConvert.DeserializeObject<T>(ProcessJson(url, HttpMethod.GET));
	}

	public static T GetJsonObject<T>(T entity, string url, string userName, string password, bool preAuthenticate = true) where T : class
	{
		return JsonConvert.DeserializeObject<T>(ProcessJson(url, HttpMethod.GET, null, userName, password, preAuthenticate));
	}
	#endregion

	#region Put Methods
	public static string Put(string json, string url)
	{
		return ProcessJson(url, HttpMethod.PUT, json);
	}

	public static string Put(string json, string url, IDictionary<string, string> headerValues)
	{
		return ProcessJson(url, HttpMethod.PUT, json, null, null, false, headerValues);
	}

	public static string Put(string json, string url, string userName, string password, bool preAuthenticate = true)
	{
		return ProcessJson(url, HttpMethod.PUT, json, userName, password, preAuthenticate);
	}

	public static string Put(object entity, string url)
	{
		return ProcessJson(url, HttpMethod.PUT, Json.SerializeToString(entity));
	}

	public static string Put(object entity, string url, IDictionary<string, string> headerValues)
	{
		return ProcessJson(url, HttpMethod.PUT, Json.SerializeToString(entity), null, null, false, headerValues);
	}

	public static string Put(object entity, string url, string userName, string password, bool preAuthenticate = true)
	{
		return ProcessJson(Json.SerializeToString(entity), HttpMethod.PUT, Json.SerializeToString(entity), userName, password, preAuthenticate);
	}
	#endregion

	#region Post Methods
	public static string Post(string json, string url)
	{
		return ProcessJson(url, HttpMethod.POST, json);
	}

	public static string Post(string json, string url, IDictionary<string, string> headerValues)
	{
		return ProcessJson(url, HttpMethod.POST, json, null, null, false, headerValues);
	}

	public static string Post(string json, string url, string userName, string password, bool preAuthenticate = true)
	{
		return ProcessJson(url, HttpMethod.POST, json, userName, password, preAuthenticate);
	}

	public static string Post(object entity, string url)
	{
		return ProcessJson(url, HttpMethod.POST, Json.SerializeToString(entity));
	}

	public static string Post(object entity, string url, IDictionary<string, string> headerValues)
	{
		return ProcessJson(url, HttpMethod.POST, Json.SerializeToString(entity), null, null, false, headerValues);
	}

	public static string Post(object entity, string url, string userName, string password, bool preAuthenticate = true)
	{
		return ProcessJson(Json.SerializeToString(entity), HttpMethod.POST, Json.SerializeToString(entity), userName, password, preAuthenticate);
	}
	#endregion

	#region Delete Methods
	public static string Delete(string url)
	{
		return ProcessJson(url, HttpMethod.DELETE);
	}

	public static string Delete(string url, IDictionary<string, string> headerValues)
	{
		return ProcessJson(url, HttpMethod.DELETE, null, null, null, false, headerValues);
	}

	public static string Delete(string url, string userName, string password, bool preAuthenticate = true)
	{
		return ProcessJson(url, HttpMethod.DELETE, null, userName, password, preAuthenticate);
	}
	#endregion

	#region Private Methods
	private static CredentialCache GetCredential(string url, string userName, string password, string authType = "Basic")
	{
		var credentialCache = new CredentialCache
		{
			{new Uri(url),authType, new NetworkCredential(userName, password)}
		};

		return credentialCache;
	}

	private static string ProcessJson(string url, HttpMethod httpMethod, string json = null,
			string userName = null, string password = null, bool preAuthenticate = true,
			IDictionary<string, string> headerValues = null)
	{
		var returnString = string.Empty;
		var req = (HttpWebRequest)WebRequest.Create(url);

		req.Method = httpMethod.ToString().ToUpper();

		if (httpMethod != HttpMethod.DELETE)
		{
			req.ContentType = "application/json";
		}

		if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
		{
			req.Credentials = GetCredential(url, userName, password);
			req.PreAuthenticate = preAuthenticate;
		}

		if (headerValues != null)
		{
			foreach (var (headerKey, headerValue) in headerValues)
			{
				req.Headers.Add(headerKey, headerValue);
			}
		}

		// ReSharper disable once SwitchStatementMissingSomeCases
		switch (httpMethod)
		{
			case HttpMethod.GET:
				using (var resp = (HttpWebResponse)req.GetResponse())
				{
					if (resp.StatusCode != HttpStatusCode.OK)
					{
						throw new CustomJsonException($"Call failed. Received HTTP {resp.StatusCode}",
							(int)resp.StatusCode);
					}

					var sr = new StreamReader(resp.GetResponseStream() ?? throw new InvalidOperationException());

					returnString = sr.ReadToEnd();
				}
				break;

			case HttpMethod.DELETE:
				req.AllowAutoRedirect = false;

				using (var resp = (HttpWebResponse)req.GetResponse())
				{
					if (resp.StatusCode != HttpStatusCode.OK)
					{
						throw new CustomJsonException($"Call failed. Received HTTP {resp.StatusCode}",
							(int)resp.StatusCode);
					}

					var sr = new StreamReader(resp.GetResponseStream() ?? throw new InvalidOperationException());

					returnString = sr.ReadToEnd();
				}
				break;

			case HttpMethod.PUT:
					//Until PUT has any specifics that differ from POST, use POST for the process.
					goto case HttpMethod.POST;

			case HttpMethod.POST:
				//Ensure passed in json exists - no, error...
				if (string.IsNullOrWhiteSpace(json))
				{
					throw new ArgumentNullException(nameof(json), "Json payload cannot be null or empty for a PUT or POST.");
				}

				var byteArray = Encoding.UTF8.GetBytes(json);
				req.ContentLength = byteArray.Length;

				var dataStream = req.GetRequestStream();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();

				try
				{
					using var resp = (HttpWebResponse) req.GetResponse();

					if (resp.StatusCode == HttpStatusCode.Created ||
						resp.StatusCode == HttpStatusCode.OK ||
						resp.StatusCode == HttpStatusCode.BadRequest)
					{
						var sr = new StreamReader(resp.GetResponseStream() ??
							                      throw new InvalidOperationException());
						returnString = sr.ReadToEnd();
						return returnString;
					}
				}
				catch (WebException webEx)
				{
					var webExResp = new StreamReader(webEx.Response.GetResponseStream() ?? throw new InvalidOperationException()).ReadToEnd();

					throw new ApplicationException($"Json call failed.  Details: {webExResp}", webEx);
				}
				catch (Exception ex)
				{
					if (ex.HResult.Equals(-2146233079)) return returnString;		//500 error in trying to make the call.

					throw new ApplicationException($"Json call failed. Received HTTP - {ex.Message}");
				}

				break;
		}

		return returnString;
	}
	#endregion

	#region Serialization
	public static string SerializeToString(object obj)
    {
		return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
	}
	#endregion
}