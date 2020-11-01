using AutoMapper;
using ZipCo.Users.WebApi.Mappers;

namespace ZipCo.Users.Test.Shared
{
   public static class AutoMapperProvider
   {
       public static  IMapper Get() {
           var mappingConfig = new MapperConfiguration(mc =>
           {
               mc.AddProfile<CommonMapper>();
               mc.AddProfile<AccountMapper>();
               mc.AddProfile<MemberMapper>();
           });
           return mappingConfig.CreateMapper();
       }
   }
}
