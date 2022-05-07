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
    public class GradeTypeController : BaseController, iBaseController<GradeType>
    {


        public GradeTypeController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<GradeType> lstGradeType = await _context.GradeTypes.OrderBy(x => x.SchoolId).ToListAsync();
            return Ok(lstGradeType);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{SCHOOL_ID}/{GRADE_TYPE_CODE}")]
        public async Task<IActionResult> Get(int SCHOOL_ID, string GRADE_TYPE_CODE)
        {
            GradeType itmGradeType = await _context.GradeTypes.Where(x => (x.SchoolId == SCHOOL_ID) && (x.GradeTypeCode == GRADE_TYPE_CODE)).FirstOrDefaultAsync();
            return Ok(itmGradeType);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{SCHOOL_ID}/{GRADE_TYPE_CODE}")]
        public async Task<IActionResult> Delete(int SCHOOL_ID, string GRADE_TYPE_CODE)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                GradeType itmGradeType = await _context.GradeTypes.Where(x => (x.SchoolId == SCHOOL_ID) && (x.GradeTypeCode == GRADE_TYPE_CODE)).FirstOrDefaultAsync();
                _context.Remove(itmGradeType);
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
        public async Task<IActionResult> Post([FromBody] GradeType _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grdtype = await _context.GradeTypes.Where(x => (x.SchoolId == _Item.SchoolId) && (x.GradeTypeCode == _Item.GradeTypeCode)).FirstOrDefaultAsync();

                if (_grdtype != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Grade Type already exists!");
                }

                _grdtype = new GradeType();

                _grdtype.SchoolId = _Item.SchoolId;
                _grdtype.GradeTypeCode = _Item.GradeTypeCode;
                _grdtype.Description = _Item.Description;

                _context.GradeTypes.Add(_grdtype);
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
        public async Task<IActionResult> Put([FromBody] GradeType _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _grdtype = await _context.GradeTypes.Where(x => (x.SchoolId == _Item.SchoolId) && (x.GradeTypeCode == _Item.GradeTypeCode)).FirstOrDefaultAsync();
                if (_grdtype == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _grdtype.SchoolId = _Item.SchoolId;
                _grdtype.GradeTypeCode = _Item.GradeTypeCode;
                _grdtype.Description = _Item.Description;
                _context.GradeTypes.Update(_grdtype);
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