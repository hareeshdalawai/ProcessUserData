using AutoMapper;
using Process.UserData.FunctionApp.Domain.Models;

namespace Process.UserData.FunctionApp.Domain.Mapping
{
    /// <summary>
    /// Mapping configuration for <c>User</c> and <c>NotificationMessage</c>> classes.
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, NotificationMessage>();
        }
    }
}
