using System.Runtime.Serialization;
using AutoMapper;
using Shouldly;
using Xunit;
using ZipCo.Users.Domain.Contracts;
using ZipCo.Users.WebApi.Mappers.Converters;

namespace ZipCo.Users.Test.Unit.Presentation.Mappers.Converters
{
    public class AccountStatusConverterTests
    {
        private readonly AccountStatusConverter _converter;
        public AccountStatusConverterTests()
        {
            _converter = new AccountStatusConverter();
        }

        [Theory]
        [InlineData("active")]
        [InlineData("ACTIVE")]
        [InlineData("Closed")]
        public void GivenAccountStatusConverter_WhenMapStringToAccountStatusId_ShouldConvertSuccessfully(string accountStatus)
        {
            // assign
            var resolutionContext = (ResolutionContext)FormatterServices.GetUninitializedObject(typeof(ResolutionContext));

            // act
            var accountStatusId = _converter.Convert(accountStatus, resolutionContext);

            //assert
            accountStatusId.ToString().ToLower().ShouldBe(accountStatus?.ToLower()??"");

        }

        [Fact]
        public void GivenAccountStatusConverter_WhenMapStringToAccountStatusId_IfAccountStatusIsInvalid_ShouldRaiseException()
        {
            // assign
            var resolutionContext = (ResolutionContext)FormatterServices.GetUninitializedObject(typeof(ResolutionContext));

            // act
            var ex = Should.Throw<BusinessException>(() => _converter.Convert("something_wrong", resolutionContext));

            //assert
            ex.IsBadRequest.ShouldBeTrue();
        }

        [Fact]
        public void GivenAccountStatusConverter_WhenMapStringToAccountStatusId_IfAccountStatusIsNull_ShouldReturnNull()
        {
            // assign
            var resolutionContext = (ResolutionContext)FormatterServices.GetUninitializedObject(typeof(ResolutionContext));

            // act
            var accountStatusIds = _converter.Convert(null, resolutionContext);

            //assert
            accountStatusIds.ShouldBeNull();
        }
    }
}
