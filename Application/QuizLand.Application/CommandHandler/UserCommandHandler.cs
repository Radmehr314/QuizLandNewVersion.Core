using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using QuizLand.Application.Contract.Commands.User;
using QuizLand.Application.Contract.Contracts;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Domain;
using QuizLand.Domain.Models.Users;
using QuizLand.Application.Mapper;
using QuizLand.Domain.Models.CodeLogs;

namespace QuizLand.Application.CommandHandler;

public class UserCommandHandler:ICommandHandler<UpdateUserCommand>,ICommandHandler<DeleteUserCommand>,ICommandHandler<RegisterUserCommand>
    ,ICommandHandler<MakeOnlineUserCommand>,ICommandHandler<MakeOfflineUserCommand>,ICommandHandler<ForgetPasswordCommand>,ICommandHandler<UserStatusComamnd>
    ,ICommandHandler<VerifyUserCommand>

{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IConfiguration _config;
    private readonly IRealTimeNotifier _realTimeNotifier;

    public UserCommandHandler(IUnitOfWork unitOfWork, IUserInfoService userInfoService, IConfiguration config, IRealTimeNotifier realTimeNotifier)
    {
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _config = config;
        _realTimeNotifier = realTimeNotifier;
    }
    /*public async Task<CommandResult> Handle(AddUserCommand command)
    {
        var exist = await _unitOfWork.UserRepository.UserExists(command.PersonelCode);
        if(exist) throw new ValidationException("کاربر تکراری است!!!");
        var data = command.Factory();
        await _unitOfWork.UserRepository.Add(data);
        await _unitOfWork.Save();
        await _realTimeNotifier.BroadcastAsync("getOnlineUsers",new {action  = "GetOnlineUser", at = DateTime.UtcNow},CancellationToken.None);
        return new CommandResult()
        {
            Id = data.Id
        };
    }*/

    public async Task<CommandResult> Handle(UpdateUserCommand command)
    {
        var user = await _unitOfWork.UserRepository.GetById(command.Id);
        //ToDo Fix Later
        await _unitOfWork.Save();
        return new CommandResult()
        {
            Id = command.Id
        };

    }

    public async Task<CommandResult> Handle(DeleteUserCommand command)
    {
        await _unitOfWork.UserRepository.Delete(command.Id);
        await _unitOfWork.Save();
        return new CommandResult();
    }

    public async Task<CommandResult> Handle(RegisterUserCommand command)
    {
        var existUserName  = await _unitOfWork.UserRepository.GetByUsername(command.Username);
        if (existUserName.IsVerified == false)
        {
            await _unitOfWork.UserRepository.Delete(existUserName.Id);
            await _unitOfWork.Save();
        }
        if (existUserName is not null) throw new NotFoundException(" نام کاربری تکراری است!!!");
        var existPhoneNumber  = await _unitOfWork.UserRepository.GetByPhoneNumber(command.PhoneNumber);
        if (existPhoneNumber is not null) throw new NotFoundException(" شماره تماس  تکراری است!!!");
        if (!Regex.IsMatch(command.PhoneNumber ?? "", @"^09\d{9}$")) throw new ValidationException("شماره تماس نامعتبر است!!!");
        bool strong = Regex.IsMatch(command.Password ?? "",
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9\s]).{8,}$");
        if (!strong) throw new ValidationException("رمز عبور قوی نیست!!!!");
        if (command.Password != command.ConfirmPassword) throw new ValidationException("رمز عبور با تکرار ان یکی نیست!!!");
        if (string.IsNullOrWhiteSpace(command.IP) || !IPAddress.TryParse(command.IP, out var ip) || ip.AddressFamily != AddressFamily.InterNetwork) throw new ValidationException("آیپی دستگاه نامعتبر است!!!");
        if (string.IsNullOrWhiteSpace(command.DeviceId))
            throw new ValidationException("DeviceId الزامی است.");

        var avatar = await _unitOfWork.AvatarRepository.GetFirst();
        var user = command.RegisterMapper(avatar.Id);

        
        var pepper = _config["Security:Pepper"]
                     ?? throw new InvalidOperationException("Missing Security:Pepper");
        
        var (hashPassword, salt) = HashMaker.HashPassword(command.Password, pepper);
        
        user.Password = hashPassword;
        user.Salt = salt;
        
        await _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.Save();
        

        return new CommandResult()
        {
            Id = user.Id
        };
        
        static byte[] FromB64Url(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            // تکمیل padding
            int mod = s.Length % 4;
            if (mod == 2) s += "==";
            else if (mod == 3) s += "=";
            else if (mod == 1) throw new FormatException("Invalid Base64Url length");
            return Convert.FromBase64String(s);
        }
    }

    public async Task<CodeLogs> VerifyOtp(string otp, string username) => await _unitOfWork.CodeLogsRepository.GetByUsernameOrPhoneNumber(username , otp);


    public async Task<CommandResult> Handle(MakeOnlineUserCommand command)
    {
        var user = await _unitOfWork.UserRepository.GetById(command.Id); 
        user.IsOnline = true;
        await _unitOfWork.Save();
        await _realTimeNotifier.BroadcastAsync("getOnlineUsers",new {action  = "GetOnlineUser", at = DateTime.UtcNow},CancellationToken.None);
        return new  CommandResult(){Id = user.Id};
        
    }

    public async Task<CommandResult> Handle(MakeOfflineUserCommand command)
    {
        var user = await _unitOfWork.UserRepository.GetById(command.Id);
        user.IsOnline = false;
        await _unitOfWork.Save();
        await _realTimeNotifier.BroadcastAsync("getOnlineUsers",new {action  = "GetOnlineUser", at = DateTime.UtcNow},CancellationToken.None);
        return new  CommandResult(){Id = user.Id};
    }

    public async Task<CommandResult> Handle(ForgetPasswordCommand command)
    {
        var user  = await _unitOfWork.UserRepository.GetByPhoneNumber(command.PhoneNumber);
        if (user is null) throw new NotFoundException("همچین کاربری یافت نشد!!!");
        bool strong = Regex.IsMatch(command.Password ?? "",
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9\s]).{8,}$");
        if (!strong) throw new ValidationException("رمز عبور قوی نیست!!!!");
        if (command.Password != command.ConfirmPassword) throw new ValidationException("رمز عبور با تکرار ان یکی نیست!!!");
        var ValidateOtp = await VerifyOtp(command.Otp, user.Username);
        if (ValidateOtp is null) throw new ValidationException("کد پیامک شده نامعتبر است");
        if (ValidateOtp.IsUsed) throw new ValidationException("کد پیامک شده استفاده شده است!!!");
        if (string.IsNullOrWhiteSpace(command.IP) || !IPAddress.TryParse(command.IP, out var ip) || ip.AddressFamily != AddressFamily.InterNetwork) throw new ValidationException("آیپی دستگاه نامعتبر است!!!");
        if(ValidateOtp.SendedAt.AddMinutes(2) <= DateTime.Now)throw new ValidationException("کد منقضی شده است!!!");
        if (string.IsNullOrWhiteSpace(command.DeviceId))
            throw new ValidationException("DeviceId الزامی است.");

       
        var code  = await _unitOfWork.CodeLogsRepository.GetByUsernameOrPhoneNumber(user.Username,command.Otp);
        code.IsUsed = true;

        user.ActiveDeviceId = command.DeviceId;

        
        var pepper = _config["Security:Pepper"]
                     ?? throw new InvalidOperationException("Missing Security:Pepper");
        
        var (hashPassword, salt) = HashMaker.HashPassword(command.Password, pepper);
        
        
        
        user.Password = hashPassword;
        user.Salt = salt;
        user.TokenVersion++;
        
        static byte[] FromB64Url(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            // تکمیل padding
            int mod = s.Length % 4;
            if (mod == 2) s += "==";
            else if (mod == 3) s += "=";
            else if (mod == 1) throw new FormatException("Invalid Base64Url length");
            return Convert.FromBase64String(s);
        }
        
        await _unitOfWork.Save();
        return new CommandResult(){Id = user.Id};
        
        
        
        
    }

    public async Task<CommandResult> Handle(UserStatusComamnd command)
    {
        var user = await _unitOfWork.UserRepository.GetById(command.Id);
        if (user is null) throw new NotFoundException("کاربر یافت نشد!!!");
        user.IsBan = !user.IsBan;
        await _unitOfWork.Save();
        return new CommandResult(){Id = user.Id};
    }


    public async Task<int> GenerateUniqueCode(string username)
    {
        int code;
        while (true)
        {
            code = RandomNumberGenerator.GetInt32(10000, 100000);
            var exist  =await _unitOfWork.CodeLogsRepository.GetByUsernameOrPhoneNumber(username, code.ToString());
            if (exist is null)
                break;
        }
        return code;
    }

    public async Task<CommandResult> Handle(VerifyUserCommand command)
    {
        var user =  await _unitOfWork.UserRepository.GetByPhoneNumber(command.PhoneNumber);
        if (user is null) throw new NotFoundException("کاربر یات نشد");
        if (user.IsVerified == true) throw new UserAccessException("کاربر قبلا اعتبار سنجی شده ست");
        
        var otpValidation =await VerifyOtp(command.Otp, command.PhoneNumber);
        if (otpValidation is null || otpValidation.IsUsed || otpValidation.SendedAt.AddMinutes(2) <= DateTime.Now)
            throw new UserAccessException("کد منقضی شده است");
        user.IsVerified = true;
        await _unitOfWork.Save();
        return new CommandResult(){Id = user.Id};

    }
}