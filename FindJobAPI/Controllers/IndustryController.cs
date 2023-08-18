﻿using FindJobAPI.Data;
using FindJobAPI.Model.Industries;
using FindJobAPI.Repository.Industries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IIndustry_Repository industry_repository;

        public IndustryController(AppDbContext appDbContext, IIndustry_Repository industry_repository)
        {
            this.appDbContext = appDbContext;
            this.industry_repository = industry_repository;
        }

        [HttpGet("Get-all-industry")]
        public async Task<IActionResult> GetAll()
        {
            var ListIndustry = await industry_repository.GetAll();
            if (ListIndustry == null)
                return BadRequest();
            return Ok(ListIndustry);
        }

        [HttpPost("Create-industry")]
        public async Task<IActionResult> CreateIndustry(IndustryNoId industryNoId)
        {
            var IndustryDomain = await industry_repository.CreateIndustry(industryNoId);
            if (IndustryDomain == null)
                return BadRequest("industry đã tồn tại");
            return Ok(IndustryDomain);
        }

        [HttpPut("Update-industry")]
        public async Task<IActionResult> UpdateIndustry ([Required] int id, IndustryNoId industryNoId)
        {
            var IndustryDomain = await industry_repository.UpdateIndustry(id, industryNoId);
            if (IndustryDomain == null)
                return BadRequest($"Không tìm thấy industry có id: {id}");
            return Ok(IndustryDomain);
        }

        [HttpDelete("Delete_industry")]
        public async Task<IActionResult> DeleteIndustry ([Required] int id)
        {
            var industryDomain = await industry_repository.DeleteIndustry(id);
            if (industryDomain == null)
                return BadRequest($"Không tìm thấy industry có id: {id}");
            return Ok(industryDomain);
        }
    }
}