using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace QuizLand.Infrastructure.Persistance.SQl.Error;

public class TelegramNotifier  : ITelegramNotifier
{
    
    private readonly IHttpClientFactory _http;
    private readonly ILogger<TelegramNotifier> _logger;

    // اگه میخوای هاردکد باشه، همینی بمونه
    private readonly string _botToken  = "EAIBD0WXUEOPFLYSVDUEAYZMCHJKJLUDBOOHTFOEZQNYLRHOIEEPAXYXNUSOYOPP";
    private readonly List<string> _chatIds = new() { "b0ohIi0C7S0fff19615f087fc8009d34" };

    public TelegramNotifier(IHttpClientFactory http, ILogger<TelegramNotifier> logger)
    {
        _http = http;
        _logger = logger;
    }

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
        var textMessage = 
            $@"❗️*{Esc(level)}*
`{Esc(correlationId)}`
*{Esc(message)}*
`{Esc(path)}`
`{Esc(userId)}`
{(statusCode?.ToString() ?? "")} · {when}";

        var url = $"https://botapi.rubika.ir/v3/{_botToken}/sendMessage";
        var client = _http.CreateClient();

        foreach (var chatId in _chatIds)
        {
            var payload = new 
            {
                chat_id = chatId,
                text = textMessage,
                chat_keypad_type = "New",
            };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null // نگه داشتن نام فیلدها همونطور که هست
            });

            try
            {
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await client.PostAsync(url, content);
                var respText = await resp.Content.ReadAsStringAsync();
                
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send telegram message to {ChatId}", chatId);
            }
        }
    }


}