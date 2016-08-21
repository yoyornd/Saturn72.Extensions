#region

using NUnit.Framework;
using Saturn72.Extensions;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
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