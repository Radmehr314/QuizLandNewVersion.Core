using System.Collections.Generic;
using System.Linq;
using QuizLand.Application.Contract.Commands.User;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.User;
using QuizLand.Domain.Models.CodeLogs;
using QuizLand.Domain.Models.Users;

namespace QuizLand.Application.Mapper;

public static class UserMapper
{
    /*public static User Factory(this AddUserCommand command)
    {
        return new User()
        {
            PersonelCode = command.PersonelCode,
            FullName = command.FullName,
            Position = command.Position,
            WorkArea = command.WorkArea,      
            BirthDate = command.BirthDate,
            HireDate =  command.HireDate,
            PhoneNumber = command.PhoneNumber,
        };
    }*/

    public static User RegisterMapper(this RegisterUserCommand command,long avatarId)
    {
        return new User()
        {
            Username = command.Username,
            PhoneNumber = command.PhoneNumber,
            ActiveDeviceId = command.DeviceId,
            IP = command.IP,
            AvatarId = avatarId
        };
    }

    /*public static GetUserByIdQueryResult GetByIdMapper(this User user)
    {
        return new GetUserByIdQueryResult()
        {
            Id = user.Id,
            PersonelCode = user.PersonelCode,
            FullName = user.FullName,
            Position = user.Position,
            WorkArea = user.WorkArea,      
            BirthDate = user.BirthDate,
            HireDate =  user.HireDate,
            PhoneNumber = user.PhoneNumber,
            IsBan = user.IsBan
        };
    }*/

    public static GetLoginUserQueryResult GetLoginUserMapper(this User user)
    {
        return new GetLoginUserQueryResult()
        {
            Id = user.Id,
            Username = user.Username,
            Phone = user.PhoneNumber
        };
    }



    /*public static List<GetAllUserQueryResult> GetAllMapper(this List<User> users)
    {
        return users
            .Select(f => new GetAllUserQueryResult() { Id = f.Id, FullName = f.FullName,PersonelCode = f.PersonelCode,Position = f.Position,PhoneNumber = f.PhoneNumber ,BirthDate = f.BirthDate,HireDate = f.HireDate,WorkArea = f.WorkArea,IsBan = f.IsBan })
            .ToList();
    }*/
    
    
    /*public static GetAllUserPaginationQueryResult AllPagination(this List<User> admins, int pageSize,
        long count)
    {
        return new GetAllUserPaginationQueryResult()
        {
            AllUsers = admins.GetAllMapper(),
            Count = count,
            PageSize = pageSize
        };
    }*/


    public static CountOfOnlineUserQueryResult CountOnline(this long count)
    {
        return new CountOfOnlineUserQueryResult()
        {
            Count = count
        };
    }
    
    public static CountAllUserQueryResult CountAllUser(this long count)
    {
        return new CountAllUserQueryResult()
        {
            Count = count
        };
    }
    
    /*public static GetUserByPersonelCodeQueryResult GetByPersonelCodeMapper(this User user)
    {
        return new GetUserByPersonelCodeQueryResult()
        {
            Id = user.Id,
            PersonelCode = user.PersonelCode,
            FullName = user.FullName,
            Position = user.Position,
            WorkArea = user.WorkArea,      
            BirthDate = user.BirthDate,
            HireDate =  user.HireDate,
            PhoneNumber = user.PhoneNumber,
            IsBan = user.IsBan
        };
    }*/
    
}