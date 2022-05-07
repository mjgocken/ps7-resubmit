using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWARM.EF.Data;
using SWARM.EF.Models;
using SWARM.Server.Controllers.Base;
using SWARM.Server.Models;
using SWARM.Shared;
using SWARM.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace SWARM.Server.Controllers.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : BaseController, iBaseController<School>
    {


        public SchoolController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<School> lstSchools = await _context.Schools.OrderBy(x => x.SchoolId).ToListAsync();
            return Ok(lstSchools);
        }

        [HttpGet]
        [Route("Get/{SCHOOL_ID}")]
        public async Task<IActionResult> Get(int SCHOOL_ID)
        {
            School itmSchool = await _context.Schools.Where(x => (x.SchoolId == SCHOOL_ID)).FirstOrDefaultAsync();
            return Ok(itmSchool);
        }

        [HttpDelete]
        [Route("Delete/{SCHOOL_ID}")]
        public async Task<IActionResult> Delete(int SCHOOL_ID)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                School itmSchool = await _context.Schools.Where(x => (x.SchoolId == SCHOOL_ID)).FirstOrDefaultAsync();
                _context.Remove(itmSchool);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] School _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _school = await _context.Schools.Where(x => (x.SchoolId == _Item.SchoolId)).FirstOrDefaultAsync();

                if (_school != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "School already exists!");
                }

                _school = new School();

                _school.SchoolId = _Item.SchoolId;
		_school.SchoolName = _Item.SchoolName;
                _context.Schools.Add(_school);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.SchoolId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] School _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _school = await _context.Schools.Where(x => (x.SchoolId == _Item.SchoolId)).FirstOrDefaultAsync();

                if (_school == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

		_school.SchoolId = _Item.SchoolId;
		_school.SchoolName = _Item.SchoolName;
                _context.Schools.Update(_school);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.SchoolId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
