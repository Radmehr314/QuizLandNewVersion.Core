using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Contract.Queries.Login;
using QuizLand.Application.Contract.Queries.Supporter;
using QuizLand.Application.Contract.QueryResults.Login;
using QuizLand.Application.Contract.QueryResults.Suporter;
using QuizLand.Application.Mapper;
using QuizLand.Domain;

namespace QuizLand.Application.QueryHandler;

public class SupportQueryHandler : IQueryHandler<GetSupporterByIdQuery,GetSupporterByIdQueryResult>,IQueryHandler<GetAllSupporterQuery,List<GetAllSupporterQueryResult>>
                                ,IQueryHandler<LoginSupporterRequestDto,LoginDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;
    public SupportQueryHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IConfiguration config)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _config = config;
    }
    public async Task<GetSupporterByIdQueryResult> Handle(GetSupporterByIdQuery query)
    {
        var supporter = await _unitOfWork.SupporterRepository.GetById(query.Id);
        if (supporter is null) throw new NotFoundException("کاربر یافت نشد!!!");
        return supporter.GetByIdMapper();
    }

    public async Task<List<GetAllSupporterQueryResult>> Handle(GetAllSupporterQuery query)
    {
        var supporters = await _unitOfWork.SupporterRepository.All();
        return supporters.AllMapper();
    }

    public async Task<LoginDto> Handle(LoginSupporterRequestDto query)
    {
        // 1) پیدا کردن کاربر و اعتبارسنجی رمز
        var supporter = await _unitOfWork.SupporterRepository.GetByUsername(query.Username);
        if (supporter is null)
            throw new NotFoundException("نام کاربری  یا رمز عبور نامعتبر میباشد!!!");
        
        var pepper = _config["Security:Pepper"]
                     ?? throw new InvalidOperationException("Missing Security:Pepper");

        var isValidUser = HashMaker.Verify(query.Password, pepper, supporter.Salt, supporter.Password);
        if (!isValidUser)
            throw new NotFoundException("نام کاربری  یا رمز عبور نامعتبر میباشد!!!");

        if (supporter.IsBan) throw new UserAccessException("کاربر مورد نظر مسدود شده است!!!");
      

        // 2) کلید عمومی (اختیاری): تلاش مینیمال برای تبدیل به بایت
        //    ورودی می‌تواند یکی از این‌ها باشد:
        //    - query.PublicKeyB64u  (Base64Url بدون padding)  ← پیشنهادی برای موبایل
        //    - query.PublicKeyBase64 (Base64 استاندارد با padding)
      

        await _unitOfWork.Save();

        // 4) صدور توکن
        var token = _tokenService.GenerateSupportToken(
            userId: supporter.Id
        );

        return new LoginDto
        {
            AccessToken = token.Value,
            TokenType   = "Bearer",
            ExpiresIn   = (int)token.ExpiresIn.TotalSeconds
        };
    }
}