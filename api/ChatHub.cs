using Microsoft.AspNetCore.SignalR;
using System.Net.WebSockets;
using api.Interfaces;

public class ChatHub : Hub
{
    private readonly IChatService _chatService;
    public ChatHub(IChatService cs) { _chatService = cs; }


    public async Task SendMessage(string message)
    {
        string response = await _chatService.SendMessage(Context.ConnectionId, message);

        await Clients.Caller.SendAsync("ReceiveMessage", response);
        //then add the query and the response to the chat history
    }

}