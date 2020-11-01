using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZipCo.Users.Test.Shared.ApiRequestBuilders;
using ZipCo.Users.WebApi.Models;
using ZipCo.Users.WebApi.Responses;

namespace ZipCo.Users.Test.Integration
{
    public static class HttpClientTestHelpers
    {
        public static async Task<MemberModel> SignUpMember(this HttpClient client, string name)
        {
            var request =
                MemberRequestBuilder.CreateSignUpMemberRequest(name, $"{Guid.NewGuid().ToString()}@gmail.com", 3000, 1000);
            var response = await client.PostAsync(TestUrls.Member, JsonContent.Create(request));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiSimpleResponse<MemberModel>>(content).Data;
        }


        public static async Task<MemberModel> GetMemberByEmail(this HttpClient client, string email)
        {
            var url = $"{TestUrls.Member}/{email}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiSimpleResponse<MemberModel>>(content).Data;

        }


        public static async Task<ApiPaginationResponse<MemberModel>> ListMembers(this HttpClient client, Dictionary<string, string> parameters)
        {
            var url = parameters.CreateUrlWithQueryString(TestUrls.ListMembers);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var data = await response.Content.ReadAsStringAsync();
            return  JsonConvert.DeserializeObject<ApiPaginationResponse<MemberModel>>(data);
        }

        public static async Task<AccountModel> SignUpAccount(this HttpClient client, long memberId)
        {
            var request = AccountRequestBuilder.CreateSignUpAccountRequest(memberId);
            var response = await client.PostAsync(TestUrls.Account, JsonContent.Create(request));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiSimpleResponse<AccountModel>>(content).Data;

        }

        public static async Task<ApiPaginationResponse<AccountModel>> ListAccounts(this HttpClient client, Dictionary<string, string> parameters)
        {
            var url = parameters.CreateUrlWithQueryString(TestUrls.ListAccounts);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiPaginationResponse<AccountModel>>(data);
        }
    }
}
