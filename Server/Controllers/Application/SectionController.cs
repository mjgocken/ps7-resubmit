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
    public class SectionController : BaseController, iBaseController<Section>
    {


        public SectionController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Section> lstSections = await _context.Sections.OrderBy(x => x.CourseNo).ToListAsync();
            return Ok(lstSections);
        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get/{COURSE_NO}/{SECTION_ID}")]
        public async Task<IActionResult> Get(int COURSE_NO, int SECTION_ID)
        {
            Section itmSection= await _context.Sections.Where(x => (x.CourseNo == COURSE_NO) && (x.SectionId == SECTION_ID)).FirstOrDefaultAsync();
            return Ok(itmSection);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{COURSE_NO}/{SECTION_ID}")]
        public async Task<IActionResult> Delete(int COURSE_NO, int SECTION_ID)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Section itmSection= await _context.Sections.Where(x => (x.CourseNo == COURSE_NO) && (x.SectionId == SECTION_ID)).FirstOrDefaultAsync();
                _context.Remove(itmSection);
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
        public async Task<IActionResult> Post([FromBody] Section _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _sec = await _context.Sections.Where(x => (x.CourseNo == _Item.CourseNo) && (x.SectionId == _Item.SectionId)).FirstOrDefaultAsync();

                if (_sec != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Section already exists!");
                }

                _sec = new Section();

                _sec.SectionId = _Item.SectionId;
                _sec.CourseNo = _Item.CourseNo;
                _sec.SectionNo = _Item.SectionNo;
                _sec.StartDateTime = _Item.StartDateTime;
                _sec.Location = _Item.Location;
                _sec.InstructorId = _Item.InstructorId;
                _sec.Capacity = _Item.Capacity;
                _context.Sections.Add(_sec);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.SectionId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Section _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _sec = await _context.Sections.Where(x => (x.CourseNo == _Item.CourseNo) && (x.SectionId == _Item.SectionId)).FirstOrDefaultAsync();

                if (_sec == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

		_sec.SectionId = _Item.SectionId;
                _sec.CourseNo = _Item.CourseNo;
                _sec.SectionNo = _Item.SectionNo;
                _sec.StartDateTime = _Item.StartDateTime;
                _sec.Location = _Item.Location;
                _sec.InstructorId = _Item.InstructorId;
                _sec.Capacity = _Item.Capacity;
                _context.Sections.Update(_sec);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.SectionId);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
