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
    public class StudentController : BaseController, iBaseController<Student>
    {
        public StudentController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpDelete]
        [Route("Delete/{STUDENT_ID}")]
        public async Task<IActionResult> Delete(int STUDENT_ID)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Student itmStudent = await _context.Students.Where(x => (x.StudentId == STUDENT_ID)).FirstOrDefaultAsync();
                _context.Remove(itmStudent);
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
            List<Student> lstStudent = await _context.Students.OrderBy(x => x.StudentId).ToListAsync();
            return Ok(lstStudent);
        }

        [HttpGet]
        [Route("Get/{STUDENT_ID}")]
        public async Task<IActionResult> Get(int STUDENT_ID)
        {
            Student itmStudent = await _context.Students.Where(x => (x.StudentId == STUDENT_ID)).FirstOrDefaultAsync();
            return Ok(itmStudent);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _stu = await _context.Students.Where(x => (x.StudentId == _Item.StudentId)).FirstOrDefaultAsync();

                if (_stu != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Student already exists!");
                }

                _stu = new Student();

                _stu.StudentId = _Item.StudentId;
                _stu.Salutation = _Item.Salutation;
                _stu.FirstName = _Item.FirstName;
                _stu.LastName = _Item.LastName;
                _stu.StreetAddress = _Item.StreetAddress;
                _stu.Zip = _Item.Zip;
                _stu.Phone = _Item.Phone;
                _stu.Employer = _Item.Employer;
                _stu.RegistrationDate = _Item.RegistrationDate;
                _context.Students.Add(_stu);
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
        public async Task<IActionResult> Put([FromBody] Student _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _stu = await _context.Students.Where(x => (x.StudentId == _Item.StudentId)).FirstOrDefaultAsync();

                if (_stu == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _stu.StudentId = _Item.StudentId;
                _stu.Salutation = _Item.Salutation;
                _stu.FirstName = _Item.FirstName;
                _stu.LastName = _Item.LastName;
                _stu.StreetAddress = _Item.StreetAddress;
                _stu.Zip = _Item.Zip;
                _stu.Phone = _Item.Phone;
                _stu.Employer = _Item.Employer;
                _stu.RegistrationDate = _Item.RegistrationDate;
                _stu.SchoolId = _Item.SchoolId;
                _context.Students.Update(_stu);
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
