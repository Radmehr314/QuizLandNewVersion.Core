using QuizLand.Application.Contract.Contracts;

namespace QuizLand.Infrastructure.Persistance.SQl.Services;

public class SmsService : ISmsService
{
    public async Task<bool> SendCode(string mobile, string code)
    {
        try
        {
            var token = new SmsIrRestfulNetCore.Token().GetToken("2129f40ea5d4612d10360105",
                "qwer9823kjbadkjf2v3khjSDkjsd");
            var paramList = new List<SmsIrRestfulNetCore.UltraFastParameters>();
            paramList.Add(new SmsIrRestfulNetCore.UltraFastParameters
            {
                Parameter = "VerificationCode",
                ParameterValue = code
            });
            var ultraFastSend = new SmsIrRestfulNetCore.UltraFastSend()
            {
                Mobile = long.Parse(mobile),
                TemplateId = 83162,
                ParameterArray = paramList.ToArray()
            };
            new SmsIrRestfulNetCore.UltraFast().Send(token, ultraFastSend);
            return true;
        }
        catch
        {
            return false;
        }

    }


}