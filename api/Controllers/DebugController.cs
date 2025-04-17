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
    [Route("api/debug")]
    [ApiController]
    public class DebugController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IDataRepository _dataRepo;
        public DebugController(ApplicationDBContext context, IDataRepository dataRepo)
        {
            _context = context;
            _dataRepo = dataRepo;
        }

        [HttpGet]
        [Route("/get-all-data")]
        public async Task<IActionResult> DebugGetAllAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var dataList = await _dataRepo.DebugGetAllAsync();

            if (dataList == null) return NotFound();

            return Ok(dataList.ToJSON());
        }
    }
}