using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.PatientData;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace api.Controllers
{
    [Route("api/patient")]
    [ApiController]
    [Authorize]
    public class PatientDataController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IDataRepository _dataRepo;
        public PatientDataController(ApplicationDBContext context, IDataRepository dataRepo)
        {
            _context = context;
            _dataRepo = dataRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientData()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.FindFirst(ClaimTypes.GivenName)?.Value;
            var dataList = await _dataRepo.GetAllAsync(userId);

            if (dataList == null) return NotFound();

            return Ok(dataList.ToJSON());
        }

         [HttpGet]
         [Route("{property}")]
        public async Task<IActionResult> GetPatientDataWithProperty([FromRoute] string property)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var userId = User.FindFirst(ClaimTypes.GivenName)?.Value;
            var dataList = await _dataRepo.GetPropertyAsync(userId, property);

            if (dataList == null) return NotFound();

            return Ok(dataList.ToJSON());
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientData([FromBody] JSONDataDto dataDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           var userId = User.FindFirst(ClaimTypes.GivenName)?.Value;

            foreach (var item in dataDto) {

                var dataItem = new PatientData {
                    Property = item.Key,
                    Value = item.Value,
                    OwnedBy = userId,
                };
                await _dataRepo.CreateAsync(dataItem);
            }

            return Ok(dataDto);
        }
    }
}