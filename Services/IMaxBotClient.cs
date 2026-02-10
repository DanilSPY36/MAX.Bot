using MAX.Bot.Models;
using MAX.Bot.Models.Enums;
using MAX.Bot.Types;
using MAX.Bot.Types.Keyboards;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MAX.Bot.Services
{
    public interface IMaxBotClient
    {
        Task<MaxBot> GetMe(CancellationToken cancellationToken = default);
        Task<IEnumerable<Chat>> GetChats(int count = 50, int? marker = default);
        Task<Chat> GetChat(Int64 chatId);
        Task<Message> SendMessage(long chatId, string text, MaxInlineKeyboard? replyMarkup = null, ParseMode parseMode = ParseMode.none, CancellationToken cancellationToken = default);
        Task<Message> SendFile(long chatId, UploadedFile file, string? caption, MaxInlineKeyboard? replyMarkup = null, ParseMode parseMode = ParseMode.none, CancellationToken cancellationToken = default);
        Task<Message> GetMessage(string messageId, long? chatId = null, CancellationToken cancellationToken = default);
        //Task<IEnumerable<Message>> GetMessagesByChat(long? chatId, DateTimeOffset? from = null, DateTimeOffset? to = null,  int count = 50, CancellationToken cancellationToken = default);
        //Task<IEnumerable<Message>> GetMessagesByIds(IReadOnlyCollection<string> messageIds, CancellationToken cancellationToken = default);
        Task<bool> EditMessageText(string messageId, string text, long? chatId = null, MaxInlineKeyboard? replyMarkup = null, ParseMode parseMode = ParseMode.none, CancellationToken cancellationToken = default);
        Task<bool> DeleteMessage(string messageId, long? chatId = null, CancellationToken cancellationToken = default);
        Task SetWebhook(string url, string? secretToken = null,IEnumerable<string>? updateTypes = null, CancellationToken cancellationToken = default);
        Task DeleteWebhook(string webHook, CancellationToken ct = default);
        Task DeleteAllWebhooks(CancellationToken ct = default);
        Task<Subscriptions> GetWebhookInfo(CancellationToken ct = default);


    }
}
