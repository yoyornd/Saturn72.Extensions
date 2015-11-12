using System.IO;
using System.Xml.Linq;

namespace Saturn72.Extensions.Xml
{
    public class XmlLoader
    {
        public static XElement GetRootElement(string filePath)
        {
            Guard.NotEmpty(filePath);
            Guard.MustFollow(() => File.Exists(filePath));

            return XDocument.Load(filePath).Root;
        }
    }
}