#region

using System;
using System.Xml.Linq;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Data.Tests
{
    public class XElementExtensionsTests
    {
        [Test]
        public void GetAttributeValue_Throws_OnNullObjects()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => ((XElement) null).GetAttributeValue(null));
            //attribute not exist
            var elem = new XElement("elem", "This is a test");
            typeof(NullReferenceException).ShouldBeThrownBy(() => elem.GetAttributeValue("MissingAttributeName"));
        }

        [Test]
        public void GetAttributeValue_ReturnsAttributeValue()
        {
            var xAttribute = new XAttribute("attName", "attValue");
            var elem = new XElement("elem", "This is a test", xAttribute);
            elem.GetAttributeValue(xAttribute.Name.ToString()).ShouldEqual(xAttribute.Value);
        }

        [Test]
        public void GetAttributeValueOrDefault_Throws_OnNullObjects()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(
                () => ((XElement) null).GetAttributeValueOrDefault(null, null));
        }

        [Test]
        public void GetAttributeValueOrDefault_ReturnsAttributeValue()
        {
            var xAttribute = new XAttribute("attName", "attValue");
            var elem = new XElement("elem", "This is a test", xAttribute);
            elem.GetAttributeValueOrDefault(xAttribute.Name.ToString()).ShouldEqual(xAttribute.Value);
        }

        [Test]
        public void GetAttributeValueOrDefault_ReturnsDefaultValue()
        {
            var elem = new XElement("elem", "This is a test");
            elem.GetAttributeValueOrDefault("RRR").ShouldEqual(default(string));

            var defaultValue = "DefaultValue";
            elem.GetAttributeValueOrDefault("RRR", defaultValue).ShouldEqual(defaultValue);
        }
    }
}