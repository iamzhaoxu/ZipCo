using FluentValidation.TestHelper;
using Xunit;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Application.Validators.Members;

namespace ZipCo.Users.Test.Unit.Core.Application.Validators.Members
{
    public class GetMemberByEmailValidatorTests
    {
        private readonly GetMemberByEmailValidator _validator;

        public GetMemberByEmailValidatorTests()
        {
            _validator = new GetMemberByEmailValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfEmailIsRequired_ShouldFail(
            string email)
        {
            // assign
            var command = new GetMemberByEmailQuery
            {
                Email = email
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Email)
                .WithErrorMessage(string.Format(ValidationTokens.IsRequire, ValidationTokens.MemberEmail));
        }

        [Theory]
        [InlineData("asdsad")]
        [InlineData("ssssss.")]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfEmailHasInvalidFormat_ShouldFail(
            string email)
        {
            // assign
            var command = new GetMemberByEmailQuery()
            {
                Email = email
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Email)
                .WithErrorMessage(string.Format(ValidationTokens.InvalidEmailFormat, ValidationTokens.MemberEmail));
        }
    }
}
