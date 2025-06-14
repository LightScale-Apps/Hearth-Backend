using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IChatService
    {
        Task<string> SendMessage(string query);
        Task AddClient(string connectionId);
        Task RemoveClient(string connectionId);
    }
}