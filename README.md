## azure-function-telegram

* Set up Azure function with a http trigger
* Pass the following object in the request body to send a message to Telegram

```
{
    "msg": "The message you want to send",
    "recipientId": "<the telegram id of the recipient user>"
}
```

