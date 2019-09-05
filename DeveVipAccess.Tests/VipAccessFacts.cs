using DeveVipAccess.Tests.Tokens;
using System;
using Xunit;

namespace DeveVipAccess.Tests
{
    public class VipAccessFacts
    {
        [Fact]
        public void GeneratesCorrectToken()
        {
            //Arrange
            var currentToken = new TestToken1();
            var testTie = new DateTime(2019, 9, 5, 11, 40, 40, DateTimeKind.Utc);

            //Act
            var generatedAccessToken = VipAccess.CreateCurrentTotpKey(currentToken.Secret, testTie);

            //Assert
            Assert.Equal("398590", generatedAccessToken);
        }
    }
}
