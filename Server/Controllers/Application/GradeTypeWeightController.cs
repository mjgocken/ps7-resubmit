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
    public class GradeTypeWeightController : BaseController, iBaseController<GradeTypeWeight>
    {


        public GradeTypeWeightController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<GradeTypeWeight> lstGradeTypeWeight = await _context.GradeTypeWeights.OrderBy(x => x.SchoolId).ToListAsync();
            return Ok(lstGradeTypeWeight);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{SCHOOL_ID}/{SECTION_ID}/{GRADE_TYPE_CODE}")]
        public async Task<IActionResult> Get(int SCHOOL_ID, int SECTION_ID, string GRADE_TYPE_CODE)
        {
            GradeTypeWeight itmGradeTypeWeight = await _context.GradeTypeWeights.Where(x => (x.SchoolId == SCHOOL_ID) && (x.SectionId == SECTION_ID) && (x.GradeTypeCode == GRADE_TYPE_CODE)).FirstOrDefaultAsync();
            return Ok(itmGradeTypeWeight);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{SCHOOL_ID}/{SECTION_ID}/{GRADE_TYPE_CODE}")]
        public async Task<IActionResult> Delete(int SCHOOL_ID, int SECTION_ID, string GRADE_TYPE_CODE)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                GradeTypeWeight itmGradeTypeWeight = await _context.GradeTypeWeights.Where(x => (x.SchoolId == SCHOOL_ID) && (x.SectionId == SECTION_ID) && (x.GradeTypeCode == GRADE_TYPE_CODE)).FirstOrDefaultAsync();
                _context.Remove(itmGradeTypeWeight);
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
        public async Task<IActionResult> Post([FromBody] GradeTypeWeight _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grdtypeweight = await _context.GradeTypeWeights.Where(x => (x.SchoolId == _Item.SchoolId) && (x.SectionId == _Item.SectionId) && (x.GradeTypeCode == _Item.GradeTypeCode)).FirstOrDefaultAsync();

                if (_grdtypeweight != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Grade Type Weight already exists!");
                }

                _grdtypeweight = new GradeTypeWeight();

                _grdtypeweight.SchoolId = _Item.SchoolId;
		_grdtypeweight.SectionId = _Item.SectionId;
                _grdtypeweight.GradeTypeCode = _Item.GradeTypeCode;
                _grdtypeweight.NumberPerSection = _Item.NumberPerSection;
		_grdtypeweight.PercentOfFinalGrade = _Item.PercentOfFinalGrade;
                _grdtypeweight.DropLowest = _Item.DropLowest;           

                _context.GradeTypeWeights.Add(_grdtypeweight);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.GradeTypeCode);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GradeTypeWeight _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grdtypeweight = await _context.GradeTypeWeights.Where(x => (x.SchoolId == _Item.SchoolId) && (x.SectionId == _Item.SectionId) && (x.GradeTypeCode == _Item.GradeTypeCode)).FirstOrDefaultAsync();
                if (_grdtypeweight == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }
 
		_grdtypeweight.SchoolId = _Item.SchoolId;
		_grdtypeweight.SectionId = _Item.SectionId;
                _grdtypeweight.GradeTypeCode = _Item.GradeTypeCode;
                _grdtypeweight.NumberPerSection = _Item.NumberPerSection;
		_grdtypeweight.PercentOfFinalGrade = _Item.PercentOfFinalGrade;
                _grdtypeweight.DropLowest = _Item.DropLowest;           

                _context.GradeTypeWeights.Update(_grdtypeweight);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.GradeTypeCode);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}