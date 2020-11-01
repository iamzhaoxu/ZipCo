using FluentValidation.TestHelper;
using Xunit;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Requests.Members.Queries;
using ZipCo.Users.Application.Validators;
using ZipCo.Users.Application.Validators.Members;

namespace ZipCo.Users.Test.Unit.Core.Application.Validators.Members
{
    public class ListMembersValidatorTests
    {
        private readonly ListMembersValidator _validator;

        public ListMembersValidatorTests()
        {
            _validator = new ListMembersValidator();
        }

        [Theory]
        [InlineData(-1, 1, ValidationTokens.InvalidPageNumber)]
        [InlineData(1, -1, ValidationTokens.InvalidPageSize)]
        public void GivenListMembersValidator_WhenCallValidate_IfPaginationInvalid_ShouldFail(int pageNumber,
            int pageSize, string expectedResult)
        {
            // assign
            var query = new ListMembersQuery
            {
                MemberName = string.Empty,
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
        public void GivenListMembersValidator_WhenCallValidate_IfPaginationValid_ShouldSuccess()
        {
            // assign
            var query = new ListMembersQuery
            {
                MemberName = null,
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
    }
}
