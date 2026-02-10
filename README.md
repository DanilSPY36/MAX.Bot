## **MAX.Bot** is the most popular .NET Client for [MAX.API](https://dev.max.ru/docs-api)

The Bot API is an HTTP-based interface created for developers keen on building bots for [MAX](https://max.ru/).

Check 👉[How to create bot](https://dev.max.ru/docs/chatbots/bots-create)👈

Check 👉 [setting up the bot](https://dev.max.ru/docs/chatbots/bots-coding/prepare) 👈

Check 👉 [Bots: An introduction for developers](https://dev.max.ru/docs/chatbots/bots-create) 👈 to understand what a Telegram bot is and what it can do.

## 🚧 Supported Platforms 🚧
.NET 10

## ⭐ Quick start ⭐

Bot token is a key that required to authorize the bot and send requests to the Bot API. Keep your token secure and store it safely, it can be used to control your bot. It should look like this: 

`k7L9clfI4zjGIK8jgWS2ieFffQcgXRhK-K20I_q5v8wgot3HheQUSoqjX1ln7V371MVDYwdXdJWlICxytQ1XR`

Now that you have a bot, it’s time to bring it to life!

```bash
dotnet add package Max.Bot
```

## Webhooks

With Webhook, your web application gets notified one by one, automatically by [MAX.api](https://dev.max.ru/docs-api) when new updates arrive for your bot.

Here are example codes for handling updates, depending on the types of ASP.NET projects:

**ASP.NET Core with Controllers (MVC)**
## 🧩 Quickstart (Dependency Injection)



### 🌐 Webhook Setup & Debugging (HTTPS Required) ###
MAX Bot API works **exclusively over HTTPS**, therefore a secure public URL is required when using webhooks.
For local development and debugging, we strongly recommend using **ngrok**.

---

### 🔧 Why ngrok?

When running your application locally:
- your server is available only on `http://localhost`
- MAX Bot API **cannot send webhooks to non-HTTPS endpoints**

**ngrok** creates a secure HTTPS tunnel to your local server and exposes it to the internet.

---
### 📥 Step 1: Install ngrok ###

Download ngrok from the official website:

👉 https://ngrok.com/download

### 🚀 Step 2: Start NGROK Start HTTPS Tunnel ###
``` bash
ngrok http (your applicationUrl port)
```

### 🚀 Step 3: Configure Webhook URL ####

Use the generated HTTPS URL as your webhook endpoint:

``` bash
Forwarding       https://61a6-77-73-123-123.ngrok-free.app -> http://localhost:1234  
```
### 🚀 Step 4: Change config in appsettings.json
``` json
{
  "BotConfiguration": {
    "Token": "YOUR_MAX_BOT_TOKEN",
    "BaseUrl": "https://61a6-77-73-232-178.ngrok-free.app/(Your endpoint name 'Bot')"
  }
}
``` 
### 🚀 Step 5: Dependency Injection in program.cs or Startup.cs

``` C#
@using MAX.Bot.Services;

var builder = WebApplication.CreateBuilder(args);

var botConfigSection = builder.Configuration.GetSection("BotConfiguration");
builder.Services.Configure<BotConfiguration>(botConfigSection);
builder.Services.AddMaxBot(builder.Configuration);

var app = builder.Build();
```
### 🚀 Step 6: Using Max.Bot in Your Own Services
Max.Bot is designed to be easily integrated into your own application services.

You can implement custom update processing logic by creating your own
`IUpdateHandler`.

**Custom Update Handler**
``` C# 
public class UpdateHandler : IUpdateHandler
{
    private readonly IMaxBotClient _bot;
    private readonly ILogger<UpdateHandler> _logger;

    public UpdateHandler(
        IMaxBotClient bot,
        ILogger<UpdateHandler> logger)
    {
        _bot = bot;
        _logger = logger;
    }

    public async Task HandleAsync(Update update, CancellationToken ct)
    {
        _logger.LogInformation("Received update: {UpdateId}", update.UpdateId);

        if (update.Message != null)
        {
            await _bot.SendMessageAsync(
                update.Message.ChatId,
                "Hello from Max.Bot 👋",
                ct);
        }
    }
}
```
**Register Custom Handler**
`builder.Services.AddScoped<IUpdateHandler, UpdateHandler>();`

### 🚀 Step 7: Handling Updates (Controller Example)

**(Controller Example)**
```C#
[HttpPost]
public async Task HandleUpdate([FromBody] Update update)
{
    // put your code to handle one Update here.
}
```

**Or using Dependency Injection:**
``` C#
 [HttpPost]
 public async Task<IActionResult> Post([FromBody] Update update, [FromServices] IMaxBotClient bot, [FromServices] IUpdateHandler handler, CancellationToken ct)
 {
    // put your code to handle one Update here.
    await handler.HandleAsync(update, ct);
    return Ok();
 }
```
## This approach provides:

✅ clean separation of concerns
✅ full DI support
✅ easy unit testing
✅ native ASP.NET Core experience

--- 