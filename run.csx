using System.Net;
using Telegram.Bot;
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    string msg = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "msg", true) == 0)
        .Value;

    long recipientId = (long) Convert.ToInt64(
        req.GetQueryNameValuePairs()
            .FirstOrDefault(q => string.Compare(q.Key, "recipientId", true) == 0)
            .Value
    );

    if (msg == null || recipientId == null)
    {
        // Get request body
        dynamic data = await req.Content.ReadAsAsync<object>();
        msg = data?.msg;
        recipientId = data?.recipientId;
    }

    if (msg == null || recipientId == null)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a msg and recipientId on the query string or in the request body");
    } 
    else
    {
        await notify(msg, recipientId);
        return req.CreateResponse(HttpStatusCode.OK, "Hello " + msg);
    }
}

private static async Task<bool> notify(string msg, long recipientId) {
    var bot = new TelegramBotClient(<string:botToken>);
    await bot.GetMeAsync();

    await bot.SendTextMessageAsync(recipientId, msg);
    return true;
} 
