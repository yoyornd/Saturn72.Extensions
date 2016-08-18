using Saturn72.Extensions.Common;
using Saturn72.Extensions.TestSdk;
using NUnit.Framework;

namespace Saturn72.Extensions.Tests.Utils
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