using System.Net;
using Telegram.Bot;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    string msg = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "msg", true) == 0)
        .Value;

    if (msg == null)
    {
        // Get request body
        dynamic data = await req.Content.ReadAsAsync<object>();
        msg = data?.msg;
    }

    if (msg == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
    } 
    else
    {
        await notify(msg);
        return req.CreateResponse(HttpStatusCode.OK, "Hello " + msg);
    }
}

private static async Task<bool> notify(string msg) {
    var bot = new TelegramBotClient(<string:bot_token>);
    await bot.GetMeAsync();

    await bot.SendTextMessageAsync(<long:receiver_id>, msg);
    return true;
} 
