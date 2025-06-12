using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;

public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        //get last 10 chats and then add the asked message to the end

        //send that to websocket and wait for response

        //_webSocketChatService.doMyJobForMe();

        await Clients.Caller.SendAsync("ReceiveMessage", modelResponse);

        //then add the query and the response to the chat history
    }
}