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
    public class ZipcodeController : BaseController, iBaseController<Zipcode>
    {


        public ZipcodeController(SWARMOracleContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {

        }

        [HttpGet]
        [Route("Get/{KeyValue}")]
        public async Task<IActionResult> Get(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Get function");
        }


        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Zipcode> lstZipcodes = await _context.Zipcodes.OrderBy(x => x.Zip).ToListAsync();
            return Ok(lstZipcodes);
        }

        [HttpGet]
        [Route("Get/{ZIP}/{STATE}")]
        public async Task<IActionResult> Get(string ZIP, string STATE)
        {
            Zipcode itmZipcode = await _context.Zipcodes.Where(x => ((x.Zip == ZIP) && (x.State == STATE))).FirstOrDefaultAsync();
            return Ok(itmZipcode);
        }


        [HttpDelete]
        [Route("Delete/{KeyValue}")]
        public async Task<IActionResult> Delete(int KeyValue)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Bad Route; should be using other Delete function");
        }

        [HttpDelete]
        [Route("Delete/{ZIP}/{STATE}")]
        public async Task<IActionResult> Delete(string ZIP, string STATE)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                Zipcode itmZipcode = await _context.Zipcodes.Where(x => ((x.Zip == ZIP) && (x.State == STATE))).FirstOrDefaultAsync();
                _context.Remove(itmZipcode);
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
        public async Task<IActionResult> Post([FromBody] Zipcode _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _Zipcode = await _context.Zipcodes.Where(x => (x.Zip == _Item.Zip)).FirstOrDefaultAsync();

                if (_Zipcode != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Zip Code already exists!");
                }

                _Zipcode = new Zipcode();

                _Zipcode.Zip = _Item.Zip;
		_Zipcode.City = _Item.City;
		_Zipcode.State = _Item.State;
                _context.Zipcodes.Add(_Zipcode);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.Zip);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Zipcode _Item)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                var _Zipcode = await _context.Zipcodes.Where(x => (x.Zip == _Item.Zip)).FirstOrDefaultAsync();

                if (_Zipcode == null)
                {
                    await this.Post(_Item);
                    return Ok();
                }

                _Zipcode.Zip = _Item.Zip;
		_Zipcode.City = _Item.City;
		_Zipcode.State = _Item.State;
                _context.Zipcodes.Update(_Zipcode);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();

                return Ok(_Item.Zip);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
