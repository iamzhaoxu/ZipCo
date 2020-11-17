using FluentValidation.TestHelper;
using Xunit;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Requests.Accounts.Queries;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Application.Validators.Accounts;

namespace ZipCo.Users.Test.Unit.Core.Application.Validators.Accounts
{
    public class ListMembersValidatorTests
    {
        private readonly ListAccountsValidator _validator;

        public ListMembersValidatorTests()
        {
            _validator = new ListAccountsValidator();
        }

        [Theory]
        [InlineData(-1, 1, ValidationTokens.PageNumberTooSmall)]
        [InlineData(1, -1, ValidationTokens.PageSizeTooSmall)]
        public void GivenListAccountsValidator_WhenCallValidate_IfPaginationInvalid_ShouldFail(int pageNumber,
            int pageSize, string expectedResult)
        {
            // assign
            var query = new ListAccountsQuery
            {
                AccountNumber = string.Empty,
                AccountStatusId = null,
                Pagination = new PaginationRequest
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                }
            };

            // act
            var result = _validator.TestValidate(query);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Pagination)
                .WithErrorMessage(expectedResult);
        }

        [Fact]
        public void GivenListAccountsValidator_WhenCallValidate_IfPaginationValid_ShouldSuccess()
        {
            // assign
            var query = new ListAccountsQuery
            {
                AccountNumber = string.Empty,
                AccountStatusId = null,
                Pagination = new PaginationRequest
                {
                    PageSize = 1,
                    PageNumber = 1
                }
            };

            // act
            var result = _validator.TestValidate(query);

            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.Pagination);
        }

        [Theory]
        [InlineData("no1233111")]
        [InlineData("ZIP")]
        [InlineData("ZIPsads1231231123")]
        [InlineData("ZIP1231231123s")]
        [InlineData(" ")]
        public void GivenListAccountsValidator_WhenCallValidate_IfAccountNumberProvided_AndAccountNumberIsInvalid_ShouldFail(string accountNumber)
        {
            // assign
            var query = new ListAccountsQuery
            {
                AccountNumber = accountNumber,
                AccountStatusId = null,
                Pagination = new PaginationRequest
                {
                    PageSize = 1,
                    PageNumber = 1
                }
            };

            // act
            var result = _validator.TestValidate(query);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.AccountNumber)
                .WithErrorMessage(ValidationTokens.InvalidAccountNumber); 
        }

        [Theory]
        [InlineData("zip1233111")]
        [InlineData("ZIP001231231")]
        public void GivenListAccountsValidator_WhenCallValidate_IfAccountNumberProvided_AndAccountNumberIsValid_ShouldSuccess(string accountNumber)
        {
            // assign
            var query = new ListAccountsQuery
            {
                AccountNumber = accountNumber,
                AccountStatusId = null,
                Pagination = new PaginationRequest
                {
                    PageSize = 1,
                    PageNumber = 1
                }
            };

            // act
            var result = _validator.TestValidate(query);

            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.AccountNumber);
        }
    }
}
