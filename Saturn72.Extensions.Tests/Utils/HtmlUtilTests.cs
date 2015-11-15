using Saturn72.Extensions.UnitTesting;
using Xunit;

namespace Saturn72.Extensions.Tests
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