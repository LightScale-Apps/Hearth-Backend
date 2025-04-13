using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.PatientData;
using api.Helpers;
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

        public async Task<PatientData> CreateAsync(PatientData patientDataModel)
        {
            await _context.PatientData.AddAsync(patientDataModel);
            await _context.SaveChangesAsync();
            return patientDataModel;
        }
    }
}