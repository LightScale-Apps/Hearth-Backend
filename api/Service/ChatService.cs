using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Service {
    public class ChatService : IChatService
    {
        private Dictionary<string, string> CONNECTIONS;

        private readonly ApplicationDBContext _context;

        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cancellationTokenSource;

        public ChatService(ApplicationDBContext c) {
            _context = c;

            _cancellationTokenSource = new CancellationTokenSource();

            _webSocket = new ClientWebSocket();
            _webSocket.ConnectAsync(new Uri("ws://3.148.141.81/ws"), _cancellationTokenSource.Token);
        }

        public async string SendMessage(string userId, string query) {
 
            var allChats = _context.ChatHistory.AsQueryable();
            var chatsToSend = await allChats.Where(s => s.OwnedBy.Equals(userId)).ToListAsync();
            Console.WriteLine($"We found a history of {chatsToSend.Count}");

            string fullQuery = "";

            foreach(var chat in chatsToSend) {
                fullQuery += $"User: {chat.Query}\nAssistant: {chat.Response}\n";            
            }
            
            fullQuery = "{" + $"""
                "chats": "{fullQuery}User: {query}{'\n'}"
            """ + "}";
            Console.WriteLine(fullQuery);


            byte[] messageBytes = Encoding.UTF8.GetBytes(fullQuery);
            await _webSocket.SendAsync(
                new ArraySegment<byte>(messageBytes),
                WebSocketMessageType.Text,
                false,
                _cancellationTokenSource.Token
            );

            return fullQuery;
        }
    }
}