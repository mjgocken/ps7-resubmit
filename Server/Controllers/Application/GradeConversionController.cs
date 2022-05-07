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
    public class GradeConversionController : BaseController, iBaseController<GradeConversion>
    {


        public GradeConversionController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<GradeConversion> lstGradeConversion = await _context.GradeConversions.OrderBy(x => x.LetterGrade).ToListAsync();
            return Ok(lstGradeConversion);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{SCHOOL_ID}/{LETTER_GRADE}")]
        public async Task<IActionResult> Get(int SCHOOL_ID, string LETTER_GRADE)
        {
            GradeConversion itmGradeConversion = await _context.GradeConversions.Where(x => (x.SchoolId == SCHOOL_ID) && (x.LetterGrade == LETTER_GRADE)).FirstOrDefaultAsync();
            return Ok(itmGradeConversion);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{SCHOOL_ID}/{LETTER_GRADE}")]
        public async Task<IActionResult> Delete(int SCHOOL_ID, string LETTER_GRADE)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                GradeConversion itmGradeConversion = await _context.GradeConversions.Where(x => (x.SchoolId == SCHOOL_ID) && (x.LetterGrade == LETTER_GRADE)).FirstOrDefaultAsync();
                _context.Remove(itmGradeConversion);
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
        public async Task<IActionResult> Post([FromBody] GradeConversion _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grdc = await _context.GradeConversions.Where(x => (x.SchoolId == _Item.SchoolId) && (x.LetterGrade == _Item.LetterGrade)).FirstOrDefaultAsync();

                if (_grdc != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Grade Conversion already exists!");
                }

                _grdc = new GradeConversion();

		        _grdc.SchoolId = _Item.SchoolId;
		        _grdc.LetterGrade = _Item.LetterGrade;
                _grdc.GradePoint = _Item.GradePoint;
                _grdc.MaxGrade = _Item.MaxGrade;
                _grdc.MinGrade = _Item.MinGrade;
                _context.GradeConversions.Add(_grdc);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.LetterGrade);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GradeConversion _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grdc = await _context.GradeConversions.Where(x => (x.SchoolId == _Item.SchoolId) && (x.LetterGrade == _Item.LetterGrade)).FirstOrDefaultAsync();
                if (_grdc == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _grdc.SchoolId = _Item.SchoolId;
		_grdc.LetterGrade = _Item.LetterGrade;
                _grdc.GradePoint = _Item.GradePoint;
                _grdc.MaxGrade = _Item.MaxGrade;
                _grdc.MinGrade = _Item.MinGrade;
                _context.GradeConversions.Update(_grdc);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.LetterGrade);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}