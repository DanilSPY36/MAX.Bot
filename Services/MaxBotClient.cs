using MAX.Bot.Exceptions;
using MAX.Bot.Models;
using MAX.Bot.Models.Attachments;
using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Payloads;
using MAX.Bot.Types;
using MAX.Bot.Types.Keyboards;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using static System.Net.WebRequestMethods;

namespace MAX.Bot.Services
{
    internal sealed class MaxBotClient : IMaxBotClient
    {
        private readonly HttpClient httpClient;
        private static readonly FileExtensionContentTypeProvider _contentTypeProvider = new();
        public MaxBotClient(HttpClient httpClient, CancellationToken cancellationToken = default)
        {

            this.httpClient = httpClient;
            
        }

        #region Message
        public async Task<Message> SendMessage(long chatId, string text, MaxInlineKeyboard? replyMarkup = null, ParseMode parseMode = ParseMode.none, CancellationToken cancellationToken = default)
        {
            
            var payload = new
            {
                text = text,
                format = parseMode != ParseMode.none ? parseMode.ToString() : null,
                attachments = replyMarkup is not null && replyMarkup.Payload.Buttons.Count()!=0 ? new object[] { replyMarkup } : null
            };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            Console.WriteLine(json);
            HttpResponseMessage response;
            try
            {
                if(chatId>0)
                    response = await httpClient.PostAsJsonAsync($"messages?user_id={chatId}", payload, cancellationToken);
                else
                {
                    response = await httpClient.PostAsJsonAsync($"messages?chat_id={chatId}", payload, cancellationToken);

                }
            }
            catch (HttpRequestException ex)
            {
                throw new MaxRequestException("Network error", ex);

            }
            catch (TaskCanceledException ex)
            {
                throw new MaxRequestException("Request timeout", ex);
            }
            if (response.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
                var messageElement = doc.RootElement.GetProperty("message");
                var message = JsonSerializer.Deserialize<Message>(messageElement.GetRawText(), 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true } ?? throw new MaxRequestException("Empty response"));
                return message ?? throw new MaxRequestException("Message from response is null");
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                MaxErrorResponse? errorResponse = null;
                try
                {
                    errorResponse = JsonSerializer.Deserialize<MaxErrorResponse>(errorBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch 
                {
                    errorResponse = new MaxErrorResponse()
                    {
                        Code = "Code is empty (SDK)",
                        Message = "Необработанная ошибка"
                        
                    };
                }
                var errorCode = response.StatusCode switch
                {
                    HttpStatusCode.BadRequest => MaxErrorCode.InvalidRequest,
                    HttpStatusCode.NotFound => MaxErrorCode.ChatNotFound,
                    HttpStatusCode.Forbidden => MaxErrorCode.BotBlocked,
                    HttpStatusCode.TooManyRequests => MaxErrorCode.RateLimited,
                    _ => MaxErrorCode.Unknown
                };

                throw new MaxRequestException(
                    message: $"Failed to send message to chatId {chatId}:  {errorResponse?.Message ?? errorBody}",
                    statusCode: response.StatusCode,
                    errorCode: errorCode);
            }
            
        }
        public async Task<Message> SendFile(long chatId, UploadedFile file, string? caption, MaxInlineKeyboard? replyMarkup = null, ParseMode parseMode = ParseMode.none, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(file);

            var uploadType = UploadTypeResolver.Resolve(file);
            var upladUrl = await GetUploadUrlAsync(uploadType, cancellationToken);
            var uploadResult = await UploadMultipartAsync(uploadType, upladUrl, file, cancellationToken);
            var message = await SendMessageWithAttachmentAsync(uploadType, chatId, uploadResult, caption, parseMode, cancellationToken);
            return message;
        }
        public async Task<Message> GetMessage(string messageId, long? chatId = null, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"messages/{messageId}", ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);
            if (response.IsSuccessStatusCode)
            {
                var message = JsonSerializer.Deserialize<Message>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                return message ?? throw new MaxRequestException("Message from response is null");
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                throw new MaxRequestException(
                    message: $"Failed to get message {messageId} in chatId {chatId}:  {errorBody}",
                    statusCode: response.StatusCode);
            }
        }
        public async Task<bool> EditMessageText(string messageId, string text, long? chatId = null, MaxInlineKeyboard? replyMarkup = null, ParseMode parseMode = ParseMode.none, CancellationToken cancellationToken = default)
        {
            var payload = new
            {
                text = text,
                format = parseMode != ParseMode.none ? parseMode.ToString() : null,
                attachments = replyMarkup is not null && replyMarkup.Payload.Buttons.Count() != 0 ? new object[] { replyMarkup } : null
            };
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            HttpResponseMessage response;
            try
            {
                response = await httpClient.PutAsJsonAsync($"messages?message_id={messageId}", payload, cancellationToken);

            }
            catch (HttpRequestException ex)
            {
                throw new MaxRequestException("Network error", ex);

            }
            catch (TaskCanceledException ex)
            {
                throw new MaxRequestException("Request timeout", ex);
            }
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            MaxBooleanResponse? result = null;
            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync(cancellationToken);
                try
                {
                    result = JsonSerializer.Deserialize<MaxBooleanResponse>(responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    ) ?? throw new MaxRequestException("Empty response from MAX");
                    
                    return result.Success;
                }
                catch (JsonException ex)
                {
                    throw new MaxRequestException("Invalid response format from MAX", ex);
                }
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                MaxErrorResponse? errorResponse = null;
                try
                {
                    errorResponse = JsonSerializer.Deserialize<MaxErrorResponse>(errorBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch
                {
                    errorResponse = new MaxErrorResponse()
                    {
                        Code = "Code is empty (SDK)",
                        Message = "Необработанная ошибка"

                    };
                }
                var errorCode = response.StatusCode switch
                {
                    HttpStatusCode.BadRequest => MaxErrorCode.InvalidRequest,
                    HttpStatusCode.NotFound => MaxErrorCode.ChatNotFound,
                    HttpStatusCode.Forbidden => MaxErrorCode.BotBlocked,
                    HttpStatusCode.TooManyRequests => MaxErrorCode.RateLimited,
                    _ => MaxErrorCode.Unknown
                };

                throw new MaxRequestException(
                    message: $"Failed to send message to chatId {chatId}:  {errorResponse?.Message ?? errorBody}",
                    statusCode: response.StatusCode,
                    errorCode: errorCode);
            }
        }
        public async Task<bool> DeleteMessage(string messageId, long? chatId = null, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.DeleteAsync($"messages?message_id={messageId}", cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            MaxBooleanResponse? result = null;
            if (response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync(cancellationToken);
                try
                {
                    result = JsonSerializer.Deserialize<MaxBooleanResponse>(responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    ) ?? throw new MaxRequestException("Empty response from MAX");
                    return result.Success;
                }
                catch (JsonException ex)
                {
                    throw new MaxRequestException("Invalid response format from MAX", ex);
                }
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new MaxRequestException(
                    message: $"Failed to delete message {messageId} in chatId {chatId}:  {errorBody}",
                    statusCode: response.StatusCode);
            }
        }

        #endregion

        #region Bot
        public async Task<MaxBot> GetMe(CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync("me", cancellationToken);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            Console.WriteLine(json);
            var bot = JsonSerializer.Deserialize<MaxBot>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return bot ?? throw new InvalidOperationException("Bot info is null");
        }
        #endregion

        #region Chats
        /// <summary>
        /// Возвращает список групповых чатов, в которых участвовал бот, информацию о каждом чате и маркер для перехода к следующей странице списка
        /// </summary>
        /// <param name="count">По умолчанию: 50. [1-100] Количество запрашиваемых чатов</param>
        /// <param name="marker">Указатель на следующую страницу данных. Для первой страницы передайте nul</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<IEnumerable<Chat>> GetChats(int count = 50, int? marker = default)
        {
            if (count > 100)
                throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be more than 100");
            
            var response = await httpClient.GetAsync($"chats?count={count}&marker={marker}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var chatList = JsonSerializer.Deserialize<ChatList>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return chatList?.Chats ?? Enumerable.Empty<Chat>();

        }

        /// <summary>
        /// Возвращает информацию о групповом чате по его ID
        /// </summary>
        /// <param name="chatId">ID запрашиваемого чата</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Chat> GetChat(Int64 chatId)
        {
            var response = await httpClient.GetAsync($"chats/{chatId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var chatList = JsonSerializer.Deserialize<Chat>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return chatList ?? throw new InvalidOperationException("Chat info is null");
        }
        #endregion

        #region Webhook
        public async Task SetWebhook(string url, string? secretToken = null,IEnumerable<string>? updateTypes = null, CancellationToken cancellationToken = default)
        {
            var types = updateTypes ?? GetAllUpdateTypes();
            var payload = new
            {
                url = url,
                update_types = types,
                secret_token = secretToken // вот тут важно
            };

            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("subscriptions", content, cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task<Subscriptions> GetWebhookInfo(CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"subscriptions", ct);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync(ct);
            //Console.WriteLine(json);
            var webhookInfo = JsonSerializer.Deserialize<Subscriptions>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return webhookInfo ?? throw new InvalidOperationException("Webhook info is null");
        }

        public async Task DeleteWebhook(string webHook, CancellationToken ct = default)
        {
            var response = await httpClient.DeleteAsync($"subscriptions?url={webHook}", ct);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(ct);
            Console.WriteLine("DeleteWebhook response: " + json);
        }

        public async Task DeleteAllWebhooks(CancellationToken ct = default)
        {
            var responce = await httpClient.GetAsync("subscriptions", ct);
            responce.EnsureSuccessStatusCode();

            var json = await responce.Content.ReadAsStringAsync(ct);
            var root = JsonSerializer.Deserialize<Subscriptions>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var subs = root?.SubscriptionList ?? new List<WebhookInfo>();

            foreach (var sub in subs)
            {
                Console.WriteLine($"Deleting webhook: {sub.Url}");
                var deleteResponse = await httpClient.DeleteAsync($"subscriptions?url={Uri.EscapeDataString(sub.Url)}", ct);
                deleteResponse.EnsureSuccessStatusCode();
            }
            Console.WriteLine("All webhooks deleted");
        }

        private string[] GetAllUpdateTypes()
        {
            return new[]
            {
                    "message_created",
                    "message_callback",
                    "message_edited",
                    "message_removed",

                    "bot_added",
                    "bot_removed",

                    "dialog_muted",
                    "dialog_unmuted",
                    "dialog_cleared",
                    "dialog_removed",

                    "user_added",
                    "user_removed",

                    "bot_started",
                    "bot_stopped",

                    "chat_title_changed"
            };
        }



        #endregion

        #region Private
        private async Task<string> GetUploadUrlAsync(UploadType uploadType, CancellationToken ct = default)
        {
            var response = await httpClient.PostAsync($"uploads?type={uploadType}", content: null, ct);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(ct);

            var result = JsonSerializer.Deserialize<UploadUrlResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );

            return result?.UploadedUrl ?? throw new InvalidOperationException("Upload URL is missing");
        }
        private async Task<UploadResult> UploadMultipartAsync(UploadType uploadType, string uploadUrl, UploadedFile file, CancellationToken ct)
        {
            if (file.stream.CanSeek)
            {
                file.stream.Position = 0;
            }
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(file.stream);
            string fieldName = uploadType == UploadType.video
                    ? "data"
                    : uploadType.ToString();

            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = fieldName,   
                FileName = file.FileName
            };

            if(uploadType == UploadType.file)
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(ResolveContentType(file.FileName));

            if (uploadType == UploadType.video)
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(ResolveContentType(file.FileName));

            content.Add(fileContent);
            using var response = await httpClient.PostAsync(uploadUrl, content, ct);
            var testContnt = response.Content;
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(ct);

            Console.WriteLine(json);
            // Для видео/аудио API вернёт объект с token
            var result = JsonSerializer.Deserialize<UploadResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? throw new InvalidOperationException("Upload failed");
        }
        private async Task<Message> SendMessageWithAttachmentAsync(UploadType uploadType, Int64 chatId, UploadResult uploadResult, string? caption, ParseMode parseMode = ParseMode.none, CancellationToken ct = default)
        {
            int maxRetries = 5;
            int delayMs = 500;
            if (uploadResult == null) throw new ArgumentNullException(nameof(uploadResult));

            // Получаем первый доступный токен (поддерживает фото, видео, файлы)
            string? token = uploadResult.GetFirstToken();
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("No token found in upload result.");

            // Определяем тип вложения по ключу в UploadResult (например, photos, videos, files)
            string attachmentType = uploadResult.Media?.Keys.FirstOrDefault() ?? "file";


            object messagePayload;

            switch (uploadType.ToString())
            {
                case "photos":
                case "image":
                    {
                        var firstPhotoEntry = uploadResult.Media!.Values.First();

                        messagePayload = new
                        {
                            text = caption ?? string.Empty,
                            //format = parseMode.ToString(),
                            attachments = new[]
                            {
                            new
                            {
                                type = "image",
                                payload = new
                                {
                                    token = token,
                                    url = firstPhotoEntry.ToString()
                                }
                            }
                        }
                        };
                        break;
                    }

                case "video":
                case "file":
                case "document":
                    {

                        messagePayload = new
                        {
                            text = caption ?? string.Empty,
                            //format = parseMode.ToString(),
                            attachments = new[]
                            {
                                new
                                {
                                    type = "file",
                                    payload = new
                                    {
                                        token = token,
                                        filename = "uploadResult.Media",
                                        size = "file.Length"
                                    }
                                }
                            }
                        };
                        //await Task.Delay(700, ct);
                        break;
                    }

                default:
                    throw new InvalidOperationException($"Unknown attachment type: {attachmentType}");
            }

            var json = JsonSerializer.Serialize(messagePayload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true, // для отладки
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            for(int attempt = 1; attempt<= maxRetries; attempt++)
            {
                var response = await httpClient.PostAsync($"messages?user_id={chatId}", content, ct);
                var jsonString = await response.Content.ReadAsStringAsync(ct);

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(jsonString);
                    var messageElement = doc.RootElement.GetProperty("message");
                    var message = JsonSerializer.Deserialize<Message>(messageElement.GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return message ?? throw new MaxRequestException("Message from response is null");
                }

                if (jsonString.Contains("attachment.not.ready") && attempt < maxRetries)
                {
                    await Task.Delay(delayMs * attempt, ct); // экспоненциальная задержка
                    continue;
                }

                // Любая другая ошибка или последний retry
                var errorCode = response.StatusCode switch
                {
                    HttpStatusCode.BadRequest => MaxErrorCode.InvalidChatId,
                    HttpStatusCode.NotFound => MaxErrorCode.ChatNotFound,
                    HttpStatusCode.Forbidden => MaxErrorCode.Forbidden,
                    HttpStatusCode.TooManyRequests => MaxErrorCode.RateLimited,
                    _ => MaxErrorCode.Unknown
                };

                throw new MaxRequestException($"Failed to send message to chatId {chatId}: {jsonString}", response.StatusCode, errorCode: errorCode);
            }
            throw new MaxRequestException($"Failed to send message to chatId {chatId}: ", HttpStatusCode.TooManyRequests, errorCode: MaxErrorCode.RateLimited);

        }
        private static string ResolveContentType(string fileName)
        {
            return _contentTypeProvider.TryGetContentType(fileName, out var contentType) ? contentType : string.Empty;
        }

        private async Task<IReadOnlyList<Message>> GetMessagesInternalAsync(long? chatId, IReadOnlyCollection<string>? messageIds, DateTimeOffset? from, DateTimeOffset? to, int? count, CancellationToken ct)
        {
            if (chatId is null && messageIds is null)
                throw new InvalidOperationException("chatId or messageIds must be provided");

            if (chatId is not null && messageIds is not null)
                throw new InvalidOperationException("Only one of chatId or messageIds must be provided");
            
            var query = new List<string>();

            if (chatId is not null)
                query.Add($"chat_id={chatId}");

            if(messageIds is not null)
                query.Add($"message_ids={string.Join(",", messageIds)}");

            if(from is not null)
                query.Add($"from={from.Value.ToUnixTimeSeconds()}");
            if(to is not null)
                query.Add($"to={to.Value.ToUnixTimeSeconds()}");
            if(count is not null) 
            {
                if(count.Value > 100)
                {
                    count = 100;
                }
                if(count.Value < 0)
                {
                    count = 1;
                }
                query.Add($"count={count}");
            }
            var url = "messages?" + string.Join("&", query);

            using var response = await httpClient.GetAsync(url, ct);
            var body = await response.Content.ReadAsStringAsync(ct);
            if (!response.IsSuccessStatusCode)
            {
                throw new MaxRequestException($"Failed to get messages: {body}", response.StatusCode);
            }
            var messages = JsonSerializer.Deserialize<List<Message>>(body, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return messages ?? new List<Message>();
        }

        #endregion
    }
}