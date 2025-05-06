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
        [Route("get-all-data")]
        public async Task<IActionResult> DebugGetAllAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var dataList = await _dataRepo.DebugGetAllAsync();

            if (dataList == null) return NotFound();

            return Ok(dataList);
        }
        [HttpGet]
        [Route("get-data-by-id/{userId}")]
        public async Task<IActionResult> DebugGetByIdAsync([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var dataList = await _dataRepo.GetAllAsync(userId);

            if (dataList == null) return NotFound();

            return Ok(dataList);
        }
        [HttpGet]
        [Route("list-users")]
        public async Task<IActionResult> DebugListUsersAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var dataList = await _dataRepo.DebugListUsersAsync();

            if (dataList == null) return NotFound();

            return Ok(dataList);
        }
        
        [HttpGet]
        [Route("list-tokens")]
        public async Task<IActionResult> DebugListTokensAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var dataList = await _context.RefreshTokens.ToListAsync();

            if (dataList == null) return NotFound();

            return Ok(dataList);
        }
    }
}