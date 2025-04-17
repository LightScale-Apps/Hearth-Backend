using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.PatientData;
using api.Models;

namespace api.Interfaces
{
    public interface IDataRepository
    {
        Task<List<PatientData>> GetAllAsync(string userId);
        Task<List<PatientData>> DebugGetAllAsync();
        Task<List<AppUser>> DebugListUsersAsync();
        Task<List<PatientData>> GetPropertyAsync(string userId, string property);
        Task<PatientData> CreateAsync(PatientData patientDataModel);

    }
}