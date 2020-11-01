using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shouldly;
using Xunit;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Test.Shared;
using ZipCo.Users.Test.Shared.ApiRequestBuilders;
using ZipCo.Users.Test.Shared.DataBuilders;
using ZipCo.Users.WebApi.Models;
using ZipCo.Users.WebApi.Responses;

namespace ZipCo.Users.Test.Unit.Presentation.Mappers
{
    public class CommonMapperTests
    {
        private static IMapper _mapper;

        public CommonMapperTests()
        {
            _mapper ??= AutoMapperProvider.Get();
        }

        [Fact] 
        public void GivenCommonMapper_WhenMapSimpleResponseToApiSimpleResponse_ShouldMapSuccessfully()
        {
            // assign
            var account = AccountDataBuilder.CreateAccount(1, null);
            var simpleResponse = SimpleResponse<Account>.Create(account);

            // act
            var apiSimpleResponse = _mapper.Map<ApiSimpleResponse<AccountModel>>(simpleResponse);

            //assert
            apiSimpleResponse.ShouldSatisfyAllConditions(
                () => apiSimpleResponse.ShouldNotBeNull(),
                () => apiSimpleResponse.Data.ShouldNotBeNull());

        }

        [Fact]
        public void GivenCommonMapper_WhenMapApiPaginationRequestToPaginationRequest_ShouldMapSuccessfully()
        {
            // assign
            var apiPaginationRequest = CommonRequestBuilder.CreateApiPaginationRequest(3, 30);
            
            // act
            var paginationRequest = _mapper.Map<PaginationRequest>(apiPaginationRequest);

            //assert
            paginationRequest.ShouldSatisfyAllConditions(
                () => paginationRequest.ShouldNotBeNull(),
                () => paginationRequest.PageNumber.ShouldBe(apiPaginationRequest.PageNumber),
                () => paginationRequest.PageSize.ShouldBe(apiPaginationRequest.PageSize));

        }

        [Fact]
        public void GivenCommonMapper_WhenMapPaginationResponseToApiPaginationResponse_ShouldMapSuccessfully()
        {
            // assign
            var account = AccountDataBuilder.CreateAccount(1, null);
            var paginationResponse = new PaginationResponse<Account>
            {
                TotalPageNumber = 30,
                Data = new List<Account> { account }
            };

            // act
            var apiPaginationResponse = _mapper.Map<ApiPaginationResponse<Account>>(paginationResponse);

            //assert
            apiPaginationResponse.ShouldSatisfyAllConditions(
                () => apiPaginationResponse.ShouldNotBeNull(),
                () => apiPaginationResponse.TotalPageNumber.ShouldBe(paginationResponse.TotalPageNumber),
                () => apiPaginationResponse.Data.ShouldNotBeNull(),
                () => apiPaginationResponse.Data.Count().ShouldBe(paginationResponse.Data.Count()));
        }
    }
}
