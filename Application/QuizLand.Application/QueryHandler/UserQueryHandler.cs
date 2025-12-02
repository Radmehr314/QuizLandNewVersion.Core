using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.DTOs;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Framework.Utils;
using QuizLand.Application.Contract.Queries.Login;
using QuizLand.Application.Contract.Queries.User;
using QuizLand.Application.Contract.QueryResults.Login;
using QuizLand.Application.Contract.QueryResults.User;
using QuizLand.Domain;
using QuizLand.Application.Mapper;

namespace QuizLand.Application.QueryHandler;

public class UserQueryHandler : IQueryHandler<LoginRequestDto,LoginDto>,IQueryHandler<GetCodeQuery,GetCodeQueryResult>,IQueryHandler<CountOfOnlineUserQuery,CountOfOnlineUserQueryResult>,IQueryHandler<CountAllUserQuery,CountAllUserQueryResult>,IQueryHandler<GetCodeForForgetPasswordQuery,GetCodeForForgetPasswordQueryResult>
,IQueryHandler<GetLoginUserInfoQuery,GetLoginUserInfoQueryResult>,IQueryHandler<GetCodeForUserValidationQuery,GetCodeForUserValidationQueryResult>,IQueryHandler<ExistUsernameQuery,ExistUsernameQueryResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IUserInfoService _userInfoService;
    private readonly IConfiguration _config;
    private readonly TimeProvider _time;
    private readonly ISmsService _smsService;



    public UserQueryHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IUserInfoService userInfoService, IConfiguration config, TimeProvider time, ISmsService smsService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userInfoService = userInfoService;
        _config = config;
        _time = time;
        _smsService = smsService;
    }
    /*
    public async Task<GetUserByIdQueryResult> Handle(GetUserByIdQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetById(query.Id);
        return user.GetByIdMapper();
    }
    */

    /*
    public async Task<List<GetAllUserQueryResult>> Handle(GetAllUserQuery query)
    {
        var users = await _unitOfWork.UserRepository.All();
        return users.GetAllMapper();
    }
    */



    /*
    public async Task<GetAllUserPaginationQueryResult> Handle(GetAllUserPaginationQuery query)
    {
        var users = await _unitOfWork.UserRepository.AllPagination(query.pageNumber,query.size);
        var count = await _unitOfWork.UserRepository.Count();
        return users.AllPagination(query.size,count);
    }
    */

    /*public async Task<GetLoginUserQueryResult> Handle(GetLoginUserQuery query)
    {
        throw new NotImplementedException();

        /*var user = await _unitOfWork.UserRepository.GetById(_userInfoService.GetUserIdByToken());
        return user.GetLoginUserMapper();#1#
    }*/

    public async Task<LoginDto> Handle(LoginRequestDto query)
    {
        // 1) پیدا کردن کاربر و اعتبارسنجی رمز
        var user = await _unitOfWork.UserRepository.GetByUsername(query.Username);
        if (user is null)
            throw new NotFoundException("نام کاربری  یا رمز عبور نامعتبر میباشد!!!");
        if (!user.IsVerified) throw new UserAccessException("کاربر مورد نظر معتبر نیست است!!!");

        var pepper = _config["Security:Pepper"]
                     ?? throw new InvalidOperationException("Missing Security:Pepper");

        var isValidUser = HashMaker.Verify(query.Password, pepper, user.Salt, user.Password);
        if (!isValidUser)
            throw new NotFoundException("نام کاربری  یا رمز عبور نامعتبر میباشد!!!");

        if (user.IsBan) throw new UserAccessException("کاربر مورد نظر مسدود شده است!!!");

        if (user.ActiveDeviceId != query.ActiveDeviceId) user.TokenVersion++;
        user.ActiveDeviceId = query.ActiveDeviceId;

        // 2) کلید عمومی (اختیاری): تلاش مینیمال برای تبدیل به بایت
        //    ورودی می‌تواند یکی از این‌ها باشد:
        //    - query.PublicKeyB64u  (Base64Url بدون padding)  ← پیشنهادی برای موبایل
        //    - query.PublicKeyBase64 (Base64 استاندارد با padding)
        

        await _unitOfWork.Save();

        // 4) صدور توکن
        var token = _tokenService.Generate(
            userId: user.Id,
            deviceId:user.ActiveDeviceId,
            tokenVersion: user.TokenVersion);

        return new LoginDto
        {
            AccessToken = token.Value,
            TokenType   = "Bearer",
            ExpiresIn   = (int)token.ExpiresIn.TotalSeconds
        };

        // --- Local helper: Base64Url → bytes (بدون ولیدیشن اضافه)
    
}

    public async Task<GetCodeQueryResult> Handle(GetCodeQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetByUsername(query.Username);
        if (user is null) throw new NotFoundException("همچین کاربری وجود ندارد!!!");
        var code = await GenerateUniqueCode(query.Username);
        await _unitOfWork.CodeLogsRepository.Add(query.Factory(code.ToString()));
        await _unitOfWork.Save();
        //ToDo Send SMS 
        return new GetCodeQueryResult()
        {
            IsSmsProviderEnabled = false,
        };
    }
    public LevelInfoDto CalculateLevelInfo(long totalXp)
    {
        var level = 1;
        var xpLeft = totalXp;

        var BaseXp = Convert.ToInt32(_config["Leveling:BaseXp"]
                     ?? throw new InvalidOperationException("Missing Leveling:BaseXp"));
        var GrowXp =Convert.ToInt32(_config["Leveling:GrowXp"]
                     ?? throw new InvalidOperationException("Missing Leveling:GrowXp"));
        while (true)
        {
            // XP لازم برای رفتن از این لول به لول بعد
            var costForThisLevel = BaseXp + GrowXp * (level - 1);

            if (xpLeft < costForThisLevel)
            {
                // اینجا دیگه نمی‌تونه بره لول بعد، پس این لول فعلیه
                var xpInCurrentLevel = xpLeft;
                var xpNeedForNextLevel = costForThisLevel;
                var remainingForNextLevel = xpNeedForNextLevel - xpInCurrentLevel;

                var progress = xpNeedForNextLevel == 0
                    ? 0
                    : (double)xpInCurrentLevel / xpNeedForNextLevel;

                return new LevelInfoDto
                {
                    TotalXp = totalXp,
                    Level = level,
                    XpInCurrentLevel = xpInCurrentLevel,
                    XpNeedForNextLevel = xpNeedForNextLevel,
                    RemainingForNextLevel = remainingForNextLevel,
                };
            }

            // هنوز XP کافی دارد، می‌ره لول بعد
            xpLeft -= costForThisLevel;
            level++;
        }
    }
    
    public async Task<int> GenerateUniqueCode(string usernameOrPhoneNumber)
    {
        int code;
        while (true)
        {
            code = RandomNumberGenerator.GetInt32(10000, 100000);
            var exist  =await _unitOfWork.CodeLogsRepository.GetByUsernameOrPhoneNumber(usernameOrPhoneNumber, code.ToString());
            if (exist is null)
                break;
        }
        return code;
    }

    public async Task<CountOfOnlineUserQueryResult> Handle(CountOfOnlineUserQuery query)
    {
       var count   = await _unitOfWork.UserRepository.CountByOnlineUsers();
       return count.CountOnline();
    }

    public async Task<CountAllUserQueryResult> Handle(CountAllUserQuery query)
    {
        var count   = await _unitOfWork.UserRepository.Count();
        return count.CountAllUser();
    }

    /*public async Task<ForgetPhoneNumberQueryResult> Handle(ForgetPhoneNumberQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetByPersonelCode(query.PersonelCode);
        if (user is null) throw new NotFoundException("همچین کاربری یافت نشد!!!");
        return new ForgetPhoneNumberQueryResult(){PhoneNumber = user.PhoneNumber.MaskMobilePhone()};
    }*/

    public async Task<GetCodeForForgetPasswordQueryResult> Handle(GetCodeForForgetPasswordQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetByPhoneNumber(query.PhoneNumber);
        if (user is null) throw new NotFoundException("همچین کاربری وجود ندارد!!!");
        var code = await GenerateUniqueCode(user.Username);
        await _unitOfWork.CodeLogsRepository.Add(query.ForgetPasswordMapper(user.Username,code.ToString()));
        await _unitOfWork.Save();
        
        var send = await _smsService.SendCode(query.PhoneNumber, code.ToString());
        return new GetCodeForForgetPasswordQueryResult()
        {
            Sent = send,
        };
    }

    /*public async Task<GetUserByPersonelCodeQueryResult> Handle(GetUserByPersonelCodeQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetByPersonelCode(query.PersonelCode);
        if (user is null) throw new NotFoundException("کاربر یافت نشد!!!");
        return user.GetByPersonelCodeMapper();
    }*/


    public async Task<GetLoginUserInfoQueryResult> Handle(GetLoginUserInfoQuery query)
    {
        var user = await _unitOfWork.UserRepository.GetById(_userInfoService.GetUserIdByToken());
        var levelInfo = CalculateLevelInfo(user.XP);
        return user.LoginUserInfoMapper(levelInfo);
    }

    public async Task<GetCodeForUserValidationQueryResult> Handle(GetCodeForUserValidationQuery query)
    {
        /*var user = await _unitOfWork.UserRepository.GetByPhoneNumber(query.PhoneNumber);
        if (user is null) throw new NotFoundException("همچین کاربری وجود ندارد!!!");*/
        var phoneExist = await _unitOfWork.UserRepository.UserExistsByPhoneNumber(query.PhoneNumber);
        if (phoneExist) throw new UserAccessException("با این شماره قبلا اکانت ایجاد شده است.");
        
        var code = await GenerateUniqueCode(query.PhoneNumber);
        await _unitOfWork.CodeLogsRepository.Add(query.UserValidationMapper(code.ToString()));
        await _unitOfWork.Save();
        
        var send = await _smsService.SendCode(query.PhoneNumber, code.ToString());
        return new GetCodeForUserValidationQueryResult()
        {
            IsSmsProviderEnabled = send,
        };
    }

    public async Task<ExistUsernameQueryResult> Handle(ExistUsernameQuery query)
    {
        var data = await _unitOfWork.UserRepository.UserExists(query.Username);
        if (data) return new ExistUsernameQueryResult() { ExistUsername = true };
        
        return new ExistUsernameQueryResult() { ExistUsername = false };
    }
}