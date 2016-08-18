using Saturn72.Extensions.TestSdk;
using NUnit.Framework;

namespace Saturn72.Extensions.AspNet.Tests
{
    [TestFixture]
    public class NetCommonHelperEmailValidatorTests
    {
        [Test]
        public void This_should_not_hang()
        {
            var email = "thisisaverylongstringcodeplex.com";
            var result = NetCommonHelper.IsValidEmail(email);
            result.ShouldBeFalse();
        }

        [Test]
        public void When_email_address_contains_upper_cases_then_the_validator_should_pass()
        {
            var email = "testperson@gmail.com";
            var result = NetCommonHelper.IsValidEmail(email);
            result.ShouldEqual(true);

            email = "TestPerson@gmail.com";
            result = NetCommonHelper.IsValidEmail(email);
            result.ShouldBeTrue();
        }

        [Test]
        public void When_the_text_is_a_valid_email_address_including_plus_validator_should_pass()
        {
            var email = "testperson+label@gmail.com";
            var result = NetCommonHelper.IsValidEmail(email);
            result.ShouldEqual(true);
        }

        [Test]
        public void When_the_text_is_a_valid_email_address_then_the_validator_should_pass()
        {
            var email = "testperson@gmail.com";
            var result = NetCommonHelper.IsValidEmail(email);
            result.ShouldEqual(true);
        }

        [Test]
        public void When_the_text_is_empty_then_the_validator_should_fail()
        {
            var email = string.Empty;
            var result = NetCommonHelper.IsValidEmail(email);
            result.ShouldEqual(false);
        }

        [Test]
        public void When_the_text_is_not_a_valid_email_address_then_the_validator_should_fail()
        {
            var email = "testperso";
            var result = NetCommonHelper.IsValidEmail(email);
            result.ShouldEqual(false);
        }

        [Test]
        public void When_the_text_is_null_then_the_validator_should_fail()
        {
            var result = NetCommonHelper.IsValidEmail(null);
            result.ShouldEqual(false);
        }
    }
}