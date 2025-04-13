using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.PatientData;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IDataRepository
    {
        Task<List<PatientData>> GetAllAsync(string userId);
        Task<PatientData> CreateAsync(PatientData patientDataModel);

    }
}