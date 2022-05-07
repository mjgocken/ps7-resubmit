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
    public class EnrollmentController : BaseController, iBaseController<Enrollment>
    {
        public EnrollmentController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{STUDENT_ID}/{SECTION_ID}/{SCHOOL_ID}")]
        public async Task<IActionResult> Delete(int STUDENT_ID, int SECTION_ID, int SCHOOL_ID)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Enrollment itmEnrollment = await _context.Enrollments.Where(x => (x.StudentId == STUDENT_ID) && (x.SectionId == SECTION_ID) && (x.SchoolId == SCHOOL_ID)).FirstOrDefaultAsync();
                _context.Remove(itmEnrollment);
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

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Enrollment> lstEnrollment = await _context.Enrollments.OrderBy(x => x.SchoolId).ToListAsync();
            return Ok(lstEnrollment);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{STUDENT_ID}/{SECTION_ID}/{SCHOOL_ID}")]
        public async Task<IActionResult> Get(int STUDENT_ID, int SECTION_ID, int SCHOOL_ID)
        {
            Enrollment itmEnrollment = await _context.Enrollments.Where(x => (x.StudentId == STUDENT_ID) && (x.SectionId == SECTION_ID) && (x.SchoolId == SCHOOL_ID)).FirstOrDefaultAsync();
            return Ok(itmEnrollment);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Enrollment _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _enr = await _context.Enrollments.Where(x => (x.StudentId == _Item.StudentId) && (x.SectionId == _Item.SectionId) && (x.SchoolId == _Item.SchoolId)).FirstOrDefaultAsync();

                if (_enr != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Enrollment already exists!");
                }

                _enr = new Enrollment();

                _enr.StudentId = _Item.StudentId;
                _enr.SectionId = _Item.SectionId;
                _enr.EnrollDate = _Item.EnrollDate;
                _enr.FinalGrade = _Item.FinalGrade;
                _enr.SchoolId = _Item.SchoolId;
                _context.Enrollments.Add(_enr);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.StudentId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Enrollment _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _enr = await _context.Enrollments.Where(x => (x.StudentId == _Item.StudentId) && (x.SectionId == _Item.SectionId) && (x.SchoolId == _Item.SchoolId)).FirstOrDefaultAsync();

                if (_enr == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _enr.StudentId = _Item.StudentId;
                _enr.SectionId = _Item.SectionId;
                _enr.EnrollDate = _Item.EnrollDate;
                _enr.FinalGrade = _Item.FinalGrade;
                _enr.SchoolId = _Item.SchoolId;
                _context.Enrollments.Update(_enr);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.StudentId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
