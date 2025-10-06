using System;
using System.Collections.Generic;
using System.Linq;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.CodeLog;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.CodeLog;
using QuizLand.Domain.Models.CodeLogs;

namespace QuizLand.Application.Mapper;

public static class CodeLogMapper
{
    public static CodeLogs Factory(this GetCodeQuery query,string otp)
    {
        return new CodeLogs()
        {
            Device = query.Device,
            Otp = otp,
            State = State.Register,
            Username = query.Username,      
            SendedAt = DateTime.Now,
            IsUsed =  true,
        };
    }
    
    
    public static CodeLogs ForgetPasswordMapper(this GetCodeForForgetPasswordQuery query,string username,string otp)
    {
        return new CodeLogs()
        {
            Device = query.Device,
            Otp = otp,
            State = State.Forgot,
            Username = username,      
            SendedAt = DateTime.Now,
            IsUsed = false,
        };
    }

    
    public static List<GetAllCodeQueryResult> GetAllMapper(this List<CodeLogs> codeLogs)
    {
        return codeLogs
            .Select(f => new GetAllCodeQueryResult() {Id = f.Id, Device = f.Device,Otp = f.Otp,Username = f.Username,IsUsed = f.IsUsed ,SendedAt = f.SendedAt,PersianSendedAt = f.SendedAt.ToShamsiString(),State = f.State.ToFa()})
            .ToList();
    }
    
    
    public static GetAllCodeLogPaginationQueryResult AllPagination(this List<CodeLogs> codeLogs, int pageSize,
        long count)
    {
        return new GetAllCodeLogPaginationQueryResult()
        {
            AllCodeLog = codeLogs.GetAllMapper(),
            Count = count,
            PageSize = pageSize
        };
    }

    public static GetAllCodeLogByPersonalCodePaginationQueryResult AllByPersonalcodePagination(this List<CodeLogs> codeLogs, int pageSize,
        long count)
    {
        return new GetAllCodeLogByPersonalCodePaginationQueryResult()
        {
            AllCodeLog = codeLogs.GetAllMapper(),
            Count = count,
            PageSize = pageSize
        };
    }

    
    

}