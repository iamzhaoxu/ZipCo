using FluentValidation.TestHelper;
using Xunit;
using ZipCo.Users.Application.Requests.Members.Commands;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Application.Validators.Members;

namespace ZipCo.Users.Test.Unit.Core.Application.Validators.Members
{
    public class SignUpMemberValidatorTests
    {
        private readonly SignUpMemberValidator _validator;

        public SignUpMemberValidatorTests()
        {
            _validator = new SignUpMemberValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfMemberNameIsEmpty_ShouldFail(
            string memberName)
        {
            // assign
            var command = new SignUpMemberCommand()
            {
                Name = memberName,
                Email = "jack@jack.com",
                MonthlySalary = 100,
                MonthlyExpense = 300
            };
            
            // act
            var result = _validator.TestValidate(command);
            
            // assert
            result.ShouldHaveValidationErrorFor(c => c.Name)
                .WithErrorMessage(string.Format(ValidationTokens.IsRequire, ValidationTokens.MemberName));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfEmailIsRequired_ShouldFail(
            string email)
        {
            // assign
            var command = new SignUpMemberCommand()
            {
                Name = "jack",
                Email = email,
                MonthlySalary = 100,
                MonthlyExpense = 300
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
            var command = new SignUpMemberCommand()
            {
                Name = "jack",
                Email = email,
                MonthlySalary = 100,
                MonthlyExpense = 300
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Email)
                .WithErrorMessage(string.Format(ValidationTokens.InvalidEmailFormat, ValidationTokens.MemberEmail));
        }

        [Theory]
        [InlineData(-1, ValidationTokens.NegativeMemberSalary)]
        [InlineData(1000 * 1000 * 1000, ValidationTokens.MemberSalaryTooHigh)]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfMonthlySalaryIsInvalid_ShouldFail(
            decimal amount, string expectedMessage)
        {
            // assign
            var command = new SignUpMemberCommand()
            {
                Name = "jack",
                Email = "jack@zip.com",
                MonthlySalary = amount,
                MonthlyExpense = 300
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.MonthlySalary)
                .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData(-1, ValidationTokens.NegativeMemberExpense)]
        [InlineData(1000 * 1000 * 1000, ValidationTokens.MemberExpenseTooHigh)]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfMonthlyExpenseIsInvalid_ShouldFail(
            decimal amount, string expectedMessage)
        {
            // assign
            var command = new SignUpMemberCommand()
            {
                Name = "jack",
                Email = "jack@zip.com",
                MonthlySalary = 100,
                MonthlyExpense = amount
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.MonthlyExpense)
                .WithErrorMessage(expectedMessage);
        }

        [Fact]
        public void GivenSignUpMemberValidator_WhenCallValidate_IfInputValid_ShouldSuccess()
        {
            // assign
            var command = new SignUpMemberCommand()
            {
                Name = "jack",
                Email = "jack@zip.com",
                MonthlySalary = 100,
                MonthlyExpense = 300
            };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
