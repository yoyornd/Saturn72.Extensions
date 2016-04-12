using System.Collections.Generic;

namespace Saturn72.Extensions.Data.Tests
{
    public class TestClass
    {
        public int IntValue { get; set; }

        public ICollection<TestSubClass> TestSubClasses { get; set; }
    }

    public class TestSubClass
    {
        public int IntValue { get; set; }

        public string[] StringArray { get; set; }
    }
}
