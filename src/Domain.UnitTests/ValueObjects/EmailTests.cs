using System.Globalization;
using System.Linq;
using Domain.Aggregates.Users;
using Domain.Core.Primitives;
using Shouldly;
using Xunit;

namespace Domain.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData(@"NotAnEmail", false)]
        [InlineData(@"@NotAnEmail", false)]
        [InlineData(@"""test\test""@example.com", false)]
        [InlineData("\"test\rtest\"@example.com", false)]
        [InlineData(@"""test""test""@example.com", false)]
        [InlineData(@".test@example.com", false)]
        [InlineData(@"te..st@example.com", false)]
        [InlineData(@"teeest.@example.com", false)]
        [InlineData(@".@example.com", false)]
        [InlineData(@"Tes T@example.com", false)]
        [InlineData(@"""test\\test""@example.com", true)]
        [InlineData("\"test\\\rtest\"@example.com", true)]
        [InlineData(@"""test\""test""@example.com", true)]
        [InlineData(@"test/test@example.com", true)]
        [InlineData(@"$A12345@example.com", true)]
        [InlineData(@"!def!xyz%abc@example.com", true)]
        [InlineData(@"_Test.Test@example.com", true)]
        [InlineData(@"~@example.com", true)]
        [InlineData(@"""Test@Test""@example.com", true)]
        [InlineData(@"Test.Test@example.com", true)]
        [InlineData(@"""Test.Test""@example.com", true)]
        [InlineData(@"""Test Test""@example.com", true)]
        public void EmailCreateResultShouldMatchExpected(string email, bool expected)
        {
            Result<Email> emailResult = Email.Create(email);

            expected.ShouldBe(emailResult.IsSuccess);

            if (expected)
            {
                email.ShouldBe(emailResult.Value());
            }
        }

        [Fact]
        public void EmailShouldConvertToStringImplicitly()
        {
            Email email = Email.Create("test@email.com").Value();

            string emailString = email;

            emailString.ShouldBe(email.Value);
        }

        [Fact]
        public void EmailShouldBeConvertedFromStringExplicitly()
        {
            const string emailString = "test@email.com";

            var email = (Email)emailString;

            email.Value.ShouldBe(emailString);
        }

        [Fact]
        public void EmailsShouldBeEqualIfValuesAreEqual()
        {
            Email email1 = Email.Create("test@email.test").Value();
            Email email2 = Email.Create("test@email.test").Value();

            email1.ShouldBe(email2);

            email1.Value.ShouldBe(email2.Value);
        }

        [Fact]
        public void EmailCreateShouldFailIfEmailIsNull()
        {
            Result<Email> emailResult = Email.Create(null);

            emailResult.IsFailure.ShouldBeTrue();
            emailResult.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void EmailCreateShouldFailIfEmailIsEmpty()
        {
            Result<Email> emailResult = Email.Create(string.Empty);

            emailResult.IsFailure.ShouldBeTrue();
            emailResult.IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void EmailCreateShouldFailIfEmailIsMoreThan255CharactersLong()
        {
            string emailString = string.Concat(Enumerable.Range(0, 256).SelectMany(num => num.ToString(CultureInfo.InvariantCulture)));

            Result<Email> emailResult = Email.Create(emailString);

            emailResult.IsFailure.ShouldBeTrue();
            emailResult.IsSuccess.ShouldBeFalse();
        }
    }
}
