﻿using FindJobAPI.Data;
using FindJobAPI.Model.Types;
using FindJobAPI.Repository.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IType_Repository _typeRepository;

        public TypeController(AppDbContext appDbContext, IType_Repository typeRepository)
        {
            _appDbContext = appDbContext;
            _typeRepository = typeRepository;
        }

        [HttpGet ("Get-all-type")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var AllType = await _typeRepository.GetAll();
                return Ok(AllType);
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpPost ("Create-type")]
        public async Task<IActionResult> CreateType(TypeNoId typeNoId)
        {
            try
            {
                var AddType = await _typeRepository.CreateType(typeNoId);
                if (AddType == null)
                {
                    return BadRequest("type name đã tồn tại");
                }
                return Ok(AddType);
            }
            catch
            {
                return BadRequest("Thêm type không thành công");
            }
        }

        [HttpPut("Update-Type")]
        public async Task<IActionResult> UpdateType (int id, TypeNoId typeNoId)
        {
            try
            {
                var TypeUdate = await _typeRepository.UpdateType(id, typeNoId);
                if (TypeUdate != null)
                {
                    return Ok(TypeUdate);
                }
                else return BadRequest($"Không tìm thấy type có id: {id}");
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete-type")]
        public async Task<IActionResult> DeleteType (int id)
        {
            try
            {
                var DeleteType = await _typeRepository.DeleteType(id);
                if (DeleteType != null)
                {
                    return Ok(DeleteType);
                }
                return BadRequest($"Không tìm thấy type có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
