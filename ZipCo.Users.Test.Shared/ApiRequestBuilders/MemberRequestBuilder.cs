using ZipCo.Users.WebApi.Requests;

namespace ZipCo.Users.Test.Shared.ApiRequestBuilders
{
    public static class MemberRequestBuilder
    {
        public static ListMemberRequest CreateListMembersRequest(string memberName,
            ApiPaginationRequest pagination)
        {
            return new ListMemberRequest
            {
                Pagination = pagination,
                MemberName = memberName
            };
        }

        public static SignUpMemberRequest CreateSignUpMemberRequest(string memberName, 
            string email, decimal monthlySalary, decimal monthlyExpense)
        {
            return new SignUpMemberRequest
            {
                Name = memberName,
                Email = email,
                MonthlySalary = monthlySalary,
                MonthlyExpense = monthlyExpense
            };
        }
    }
}
