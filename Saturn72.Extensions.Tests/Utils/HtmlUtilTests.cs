using Saturn72.Extensions.UnitTesting;
using Saturn72.Extensions.Utils;
using Xunit;

namespace Saturn72.Extensions.Tests.Utils
{
    public class HtmlUtilTests
    {
        [Fact]
        public void RemoveHtmlEscapes()
        {
            HtmlUtil.RemoveHtmlEscapes("br<br>   img<img />  ul<ul><li></li><li/></ul>").ShouldEqual("br img ul");
        }
    }
}