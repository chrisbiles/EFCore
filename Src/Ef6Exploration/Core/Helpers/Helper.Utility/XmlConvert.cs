using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Xml;
using System.Xml.Linq;

namespace Helper.Utility;

public static class XmlConvert
{
	public static void Save(object obj, string path)
	{
		File.WriteAllText(path, SerializeObject(obj));
	}

	public static string SerializeObject(this object obj)
	{
		var json = Json.SerializeToString(obj);

		var xmlDoc = JsonConvert.DeserializeXmlNode(json);

		if (xmlDoc == null) return "No Data";

		var xDoc = XDocument.Parse(xmlDoc.OuterXml);

		return xDoc.ToString();
	}

	public static dynamic DeserializeXml(string xmlString)
	{
		var xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xmlString);

		var json = JsonConvert.SerializeXmlNode(xmlDoc);
		dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

		return obj;
	}
}
