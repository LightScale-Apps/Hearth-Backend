using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;

namespace api.Service {
    public class ChatService : IChatService
    {
        private Dictionary<string, string> CONNECTIONS;

        private readonly ApplicationDBContext _context;
        private WebSocket _webSocket;

        public ChatService(ApplicationDBContext c) {
            _context = c;

            _webSocket = new WebSocket();
            _webSocket.ConnectAsync(new Uri("ws://3.148.141.81/ws"));
        }

        public Task AddClient(string id) {
        }
        public Task RemoveClient(string id) {
        }

        public SendMessage(string connId, string query) {

            var userId = CONNECTIONS[connId];

            var allChats = _context.ChatHistory.AsQueryable();
            var chatsToSend = await allChats.Where(s => s.OwnedBy.Equals(userId)).ToListAsync();

            string fullQuery = "";
            foreach(var chat in chatsToSend) {
                var s = $"User: {chat.Query}\nAssistant: {chat.Response}\n";
                fullQuery += s;
            }
            fullQuery += $"User: {query}\n";

            var chatString = "{" + $"""
                "chats": "{fullQuery}"
            """ + "}";

            _webSocket.SendAsync(chatString);
        }
    }
}