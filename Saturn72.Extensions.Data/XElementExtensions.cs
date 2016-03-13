using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Saturn72.Extensions
{
    public static class XElementExtensions
    {
        public static string GetInnerElementValue(this XElement source, string innerElementName)
        {
            if (source == null)
                return string.Empty;

            var innerElement = source.Element(innerElementName);
            return innerElement == null ? string.Empty : innerElement.Value;
        }

        public static XmlNode ToXmlNode(this XElement source, XmlDocument xmlDoc = null)
        {
            using (var xmlReader = source.CreateReader())
            {
                if (xmlDoc == null) xmlDoc = new XmlDocument();
                return xmlDoc.ReadNode(xmlReader);
            }
        }

        public static string ToJson(this XElement source)
        {
            return JsonConvert.SerializeXmlNode(ToXmlNode(source));
        }

        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairCollection(this XElement source)
        {
            Guard.NotNull(source);
            var elements = source.Elements();
            if (elements.IsEmpty())
            {
                var attToElemList = new List<XElement>();
                foreach (var attribute in source.Attributes())
                    attToElemList.Add(new XElement(attribute.Name, attribute.Value));
                elements = attToElemList;
            }
            Guard.NotEmpty(elements);

            var result = new List<KeyValuePair<string, string>>(elements.Count());
            elements.ForEachItem(e=>
                result.Add(new KeyValuePair<string, string>(e.Name.ToString(), e.Value)));

            return result;
        }
    }
}