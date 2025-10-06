using QuizLand.Domain.Models.CodeLogs;

namespace QuizLand.Application.Contract.Framework;

public static class CodeStateFa
{
    public static string ToFa(this State s) => s switch
    {
        State.Register => "ثبت‌نام",
        State.Forgot   => "فراموشی رمز",
        _              => "نامشخص"
    };
}