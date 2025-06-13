using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.PatientData;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDBContext _context;
        public DataRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<List<PatientData>> GetAllAsync(string userId)
        {
            var data = _context.PatientData.AsQueryable();
            data = data.Where(s => s.OwnedBy.Equals(userId));

            var dataList = await data.ToListAsync();

            return dataList;
        }

        public async Task<List<PatientData>> DebugGetAllAsync()
        {
            var data = _context.PatientData.AsQueryable();

            return await data.ToListAsync();
        }
        public async Task<List<ChatHistory>> DebugChatHistoryAsync()
        {
            return await _context.ChatHistory.AsQueryable().ToListAsync();
        }
        public async Task<ChatHistory> AddChatHistoryAsync(ChatHistory chatHistoryEntry)
        {
            await _context.ChatHistory.AddAsync(chatHistoryEntry);
            await _context.SaveChangesAsync();
            return chatHistoryEntry;
        }

        public async Task<List<AppUser>> DebugListUsersAsync()
        {
            var data = _context.Users.AsQueryable();

            return await data.ToListAsync();
        }
        public async Task<List<PatientData>> GetPropertyAsync(string userId, string property)
        {
            var data = _context.PatientData.AsQueryable();
            data = data.Where(s => s.OwnedBy.Equals(userId) && s.Property.Equals(property));

            var dataList = await data.ToListAsync();

            return dataList;
        }

        public async Task<PatientData> CreateAsync(PatientData patientDataModel)
        {
            await _context.PatientData.AddAsync(patientDataModel);
            await _context.SaveChangesAsync();
            return patientDataModel;
        }
    }
}