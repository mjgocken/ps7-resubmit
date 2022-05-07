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
    public class GradeController : BaseController, iBaseController<Grade>
    {


        public GradeController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Grade> lstGrade = await _context.Grades.OrderBy(x => x.StudentId).ToListAsync();
            return Ok(lstGrade);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{SCHOOL_ID}/{STUDENT_ID}/{SECTION_ID}/{GRADE_TYPE_CODE}/{GRADE_CODE_OCCURRENCE}")]
        public async Task<IActionResult> Get(int SCHOOL_ID, int STUDENT_ID, int SECTION_ID, string GRADE_TYPE_CODE, int GRADE_CODE_OCCURRENCE)
        {
            Grade itmGrade = await _context.Grades.Where(x => (x.SchoolId == SCHOOL_ID) && (x.StudentId == STUDENT_ID) && (x.SectionId == SECTION_ID) && (x.GradeTypeCode == GRADE_TYPE_CODE) && (x.GradeCodeOccurrence == GRADE_CODE_OCCURRENCE)).FirstOrDefaultAsync();
            return Ok(itmGrade);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{SCHOOL_ID}/{STUDENT_ID}/{SECTION_ID}/{GRADE_TYPE_CODE}/{GRADE_CODE_OCCURRENCE}")]
        public async Task<IActionResult> Delete(int SCHOOL_ID, int STUDENT_ID, int SECTION_ID, string GRADE_TYPE_CODE, int GRADE_CODE_OCCURRENCE)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Grade itmGrade = await _context.Grades.Where(x => (x.SchoolId == SCHOOL_ID) && (x.StudentId == STUDENT_ID) && (x.SectionId == SECTION_ID) && (x.GradeTypeCode == GRADE_TYPE_CODE) && (x.GradeCodeOccurrence == GRADE_CODE_OCCURRENCE)).FirstOrDefaultAsync();
                _context.Remove(itmGrade);
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
        public async Task<IActionResult> Post([FromBody] Grade _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grd = await _context.Grades.Where(x => (x.SchoolId == _Item.SchoolId) && (x.StudentId == _Item.StudentId) && (x.SectionId == _Item.SectionId) && (x.GradeTypeCode == _Item.GradeTypeCode) && (x.GradeCodeOccurrence == _Item.GradeCodeOccurrence)).FirstOrDefaultAsync();

                if (_grd != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Grade already exists!");
                }

                _grd = new Grade();

                _grd.StudentId = _Item.StudentId;
                _grd.StudentId = _Item.SectionId;
                _grd.SectionId = _Item.SectionId;
                _grd.GradeTypeCode = _Item.GradeTypeCode;
                _grd.GradeCodeOccurrence = _Item.GradeCodeOccurrence;
                _grd.NumericGrade = _Item.NumericGrade;
                _grd.Comments = _Item.Comments;
                _grd.SchoolId = _Item.SchoolId;
                _context.Grades.Add(_grd);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.NumericGrade);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Grade _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grd = await _context.Grades.Where(x => (x.SchoolId == _Item.SchoolId) && (x.StudentId == _Item.StudentId) && (x.SectionId == _Item.SectionId) && (x.GradeTypeCode == _Item.GradeTypeCode) && (x.GradeCodeOccurrence == _Item.GradeCodeOccurrence)).FirstOrDefaultAsync();
                if (_grd == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _grd.StudentId = _Item.StudentId;
                _grd.StudentId = _Item.SectionId;
                _grd.SectionId = _Item.SectionId;
                _grd.GradeTypeCode = _Item.GradeTypeCode;
                _grd.GradeCodeOccurrence = _Item.GradeCodeOccurrence;
                _grd.NumericGrade = _Item.NumericGrade;
                _grd.Comments = _Item.Comments;
                _grd.SchoolId = _Item.SchoolId;
                _context.Grades.Update(_grd);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.NumericGrade);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}