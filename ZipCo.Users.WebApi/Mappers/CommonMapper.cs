using AutoMapper;
using ZipCo.Users.Application.Requests;
using ZipCo.Users.Application.Responses;
using ZipCo.Users.WebApi.Requests;
using ZipCo.Users.WebApi.Responses;

namespace ZipCo.Users.WebApi.Mappers
{
    public class CommonMapper: Profile
    {
        public CommonMapper()
        {
            #region Request

            CreateMap<ApiPaginationRequest, PaginationRequest>();

            #endregion

            #region Response

            CreateMap(typeof(PaginationResponse<>), typeof(ApiPaginationResponse<>));
            CreateMap(typeof(SimpleResponse<>), typeof(ApiSimpleResponse<>));
            #endregion


        }
    }
}
