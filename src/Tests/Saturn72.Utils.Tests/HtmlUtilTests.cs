using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Utils.Tests
{
    public class HtmlUtilTests
    {
        [Test]
        public void RemoveHtmlEscapes()
        {
            HtmlUtil.RemoveHtmlEscapes("br<br>   img<img />  ul<ul><li></li><li/></ul>").ShouldEqual("br img ul");
        }
    }
}