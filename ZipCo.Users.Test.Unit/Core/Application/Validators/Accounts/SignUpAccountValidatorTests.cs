using FluentValidation.TestHelper;
using Xunit;
using ZipCo.Users.Application.Requests.Accounts.Commands;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Application.Validators.Accounts;

namespace ZipCo.Users.Test.Unit.Core.Application.Validators.Accounts
{
    public class SignUpMemberValidatorTests
    {
        private readonly SignUpAccountValidator _validator;

        public SignUpMemberValidatorTests()
        {
            _validator = new SignUpAccountValidator();
        }

        [Fact]
        public void GivenSignUpAccountValidator_WhenCallValidate_IfMemberIdIsZero_ShouldFail()
        {
            // assign
            var command = new SignUpAccountCommand
            {
                MemberId = 0
            };
            
            // act
            var result = _validator.TestValidate(command);
            
            // assert
            result.ShouldHaveValidationErrorFor(c => c.MemberId)
                .WithErrorMessage(ValidationTokens.InvalidMemberId);
        }


        [Fact]
        public void GivenSignUpAccountValidator_WhenCallValidate_IfMemberIdIsNotZero_ShouldSuccess()
        {
            // assign
            var command = new SignUpAccountCommand
            {
                MemberId = 1
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.MemberId);
        }
    }
}
