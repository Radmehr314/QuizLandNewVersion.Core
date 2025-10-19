using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace QuizLand.Infrastructure.Persistance.SQl.Error;

public class TelegramNotifier  : ITelegramNotifier
{
    
    private readonly IHttpClientFactory _http;
    private readonly ILogger<TelegramNotifier> _logger;

    // اگه میخوای هاردکد باشه، همینی بمونه
    private readonly string _botToken  = "8312279011:AAE_gy2QnZuWk_iF5H70NrY4OqTK4itS7sM";
    private readonly List<string> _chatIds = new() { "1176678219" };
    
    
    public async Task SendAsync(string level, string message, string? path = null, int? statusCode = null, string? correlationId = null,
        string? userId = null, DateTime? whenUtc = null)
    {
        string Esc(string? s)
        {
            if (string.IsNullOrEmpty(s)) return "-";
            // ساده: بک‌تیک و آندرلاین و ستاره و ... رو بک‌اسلش می‌زنیم
            return Regex.Replace(s, @"([_*\[\]()~`>#+\-=|{}.!])", @"\$1");
        }

        var when = (whenUtc ?? DateTime.UtcNow).ToString("yyyy-MM-dd HH:mm:ss") + "Z";
        var text = 
            $@"❗️*{Esc(level)}*
`{Esc(correlationId)}`
*{Esc(message)}*
`{Esc(path)}`
`{Esc(userId)}`
{(statusCode?.ToString() ?? "")} · {when}";

        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var client = _http.CreateClient();

        foreach (var chatId in _chatIds)
        {
            var payload = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["text"] = text,
                ["parse_mode"] = "MarkdownV2" // با escape بالا سازگاره
            };

            try
            {
                using var content = new FormUrlEncodedContent(payload);
                var resp = await client.PostAsync(url, content);
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send telegram message to {ChatId}", chatId);
            }
        }
    }


}