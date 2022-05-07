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
    public class InstructorController : BaseController, iBaseController<Instructor>
    {


        public InstructorController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Instructor> lstInstructors = await _context.Instructors.OrderBy(x => x.InstructorId).ToListAsync();
            return Ok(lstInstructors);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{INSTRUCTOR_ID}/{SCHOOL_ID}")]
        public async Task<IActionResult> Get(int INSTRUCTOR_ID, int SCHOOL_ID)
        {
            Instructor itmInstructor = await _context.Instructors.Where(x => (x.InstructorId == INSTRUCTOR_ID) && (x.SchoolId == SCHOOL_ID)).FirstOrDefaultAsync();
            return Ok(itmInstructor);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{INSTRUCTOR_ID}/{SCHOOL_ID}")]
        public async Task<IActionResult> Delete(int INSTRUCTOR_ID, int SCHOOL_ID)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Instructor itmInstructor = await _context.Instructors.Where(x => (x.InstructorId == INSTRUCTOR_ID) && (x.SchoolId == SCHOOL_ID)).FirstOrDefaultAsync();
                _context.Remove(itmInstructor);
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
        public async Task<IActionResult> Post([FromBody] Instructor _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _instr = await _context.Instructors.Where(x => (x.InstructorId == _Item.InstructorId) && (x.SchoolId == _Item.SchoolId)).FirstOrDefaultAsync();

                if (_instr != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Instructor already exists!");
                }

                _instr = new Instructor();

                _instr.SchoolId = _Item.SchoolId;
                _instr.InstructorId = _Item.InstructorId;
                _instr.Salutation = _Item.Salutation;
                _instr.FirstName = _Item.FirstName;
                _instr.LastName = _Item.LastName;
                _instr.StreetAddress = _Item.StreetAddress;
                _instr.Zip = _Item.Zip;
                _instr.Phone = _Item.Phone;
                _context.Instructors.Add(_instr);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.InstructorId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Instructor _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _instr = await _context.Instructors.Where(x => (x.InstructorId == _Item.InstructorId) && (x.SchoolId == _Item.SchoolId)).FirstOrDefaultAsync();

                if (_instr == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _instr.SchoolId = _Item.SchoolId;
                _instr.InstructorId = _Item.InstructorId;
                _instr.Salutation = _Item.Salutation;
                _instr.FirstName = _Item.FirstName;
                _instr.LastName = _Item.LastName;
                _instr.StreetAddress = _Item.StreetAddress;
                _instr.Zip = _Item.Zip;
                _instr.Phone = _Item.Phone;
                _context.Instructors.Update(_instr);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.InstructorId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
