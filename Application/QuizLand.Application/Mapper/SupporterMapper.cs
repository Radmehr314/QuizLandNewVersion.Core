using System.Collections.Generic;
using System.Linq;
using QuizLand.Application.Contract.Commands.Supporter;
using QuizLand.Application.Contract.QueryResults.Suporter;
using QuizLand.Domain.Models.Supporters;

namespace QuizLand.Application.Mapper;

public static class SupporterMapper
{
    public static Supporter Factory(this AddSupporterCommand command,string hashPassword,string salt)
    {
        return new Supporter()
        {
             Username = command.Username,
             Password = hashPassword,
             Salt = salt,
             IsBan = false
        };
    }
    
    public static GetSupporterByIdQueryResult GetByIdMapper(this Supporter supporter)
    {
        return new GetSupporterByIdQueryResult()
        {
            Id = supporter.Id,
            Username = supporter.Username,
            IsBan = supporter.IsBan
        };
    }
    
    public static List<GetAllSupporterQueryResult> AllMapper(this List<Supporter> supporters)
    {
        return supporters.Select(f => new GetAllSupporterQueryResult()
            { Id = f.Id, Username = f.Username, IsBan = f.IsBan }).ToList();
    }

}