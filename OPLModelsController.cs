using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OPLModelsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public OPLModelsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/OPLModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OPLModel>>> GetOPLModels()
        {
            return await _context.OPLModels.Where(i=>i.Active==true).ToListAsync();
        }

        //[HttpGet]// GET: api/OPLModels/getmax/?id=1
        //[Route("getmax")]
        //public int ReturnMax(int id)
        //{
        //    int maxNumber = _context.OPLModels.Where(i => i.Line_Id == id && i.Active == true).Max(i => i.Number).GetValueOrDefault();
        //    Console.WriteLine("Broj" + maxNumber);
        //    return maxNumber;
        //}

        // GET: api/OPLModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OPLModel>>> GetOPLModel(int id)
        {
            var OPLModel = await _context.OPLModels.Where(i=>i.Line_Id==id && i.Active==true).ToListAsync();
            foreach(var o in OPLModel)
            {
                var names = await _context.ApplicationUsers.Where(i => i.Id == o.Responsible).Select(i => i.FullName).ToListAsync();
                foreach(var n in names)
                {
                    o.Responsible_name = n;
                }
                var OPLlines = await _context.LineModels.Where(i => i.Line_id == o.Line_Id).Select(i => i.Short_line_name).ToListAsync();
                foreach(var l in OPLlines)
                {
                    o.Line_name = l;
                }
            }

            if (OPLModel == null)
            {
                return NotFound();
            }

            return OPLModel;
        }


        // POST: api/OPLModels/change/?id=1
        [HttpPost]//("{id}")
        [Route("change")]
        public async Task<ActionResult<OPLModel>> ChangeStatus(int id)
        {
            OPLModel oplrow = _context.OPLModels.Where(i => i.Id == id).SingleOrDefault();
            if (id != oplrow.Id)
            {
                return BadRequest();
            }
            _context.Entry(oplrow).CurrentValues.SetValues(oplrow.Active=false);
            _context.Entry(oplrow).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OPLModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OPLModels/1
        [HttpPost("{id}")]
        public async Task<ActionResult<OPLModel>> PostOPLModelById(int id, OPLModel OPLModel)
        {
            //OPLModel oplrow = _context.OPLModels.Where(i => i.Id == id).SingleOrDefault();
            if (id != OPLModel.Id)
            {
                return BadRequest();
            }
            _context.Entry(OPLModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OPLModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // PUT: api/OPLModels/5
        /* [HttpPut("{id}")]
         public async Task<IActionResult> PutOPLModel(int id, OPLModel oPLModel)
         {
             if (id != oPLModel.Id)
             {
                 return BadRequest();
             }

             _context.Entry(oPLModel).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!OPLModelExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return NoContent();
         }*/

        // POST: api/OPLModels
        //[HttpPost]
        //public async Task<ActionResult<OPLModel>> PostOPLModel(OPLModel oPLModel)
        //{
        //    int maxNumber = _context.OPLModels.Where(i => i.Line_Id == oPLModel.Line_Id && i.Active == true).Max(i => i.Number).GetValueOrDefault();
        //    oPLModel.Number = maxNumber+1;
        //    _context.OPLModels.Add(oPLModel);
        //    await _context.SaveChangesAsync();           

        //    return CreatedAtAction("GetOPLModel", new { id = oPLModel.Id }, oPLModel);
        //}


        // POST: api/OPLModels/add/?lineid=2
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<OPLModel>> PostOPLModel(int lineid)
        {
            Console.Write("komentar");
            OPLModel oplmodel = new OPLModel()
            {
                Line_Id = lineid,
                Reference="CIP",
                Active=true
            };
            
            try
            {
                Console.Write("salji");
                _context.OPLModels.Add(oplmodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            return NoContent();

        }

        // POST: api/OPLModels/dodaj/?lineid=2
        [HttpGet]
        [Route("dodaj")]
        public ActionResult<OPLModel> DodajOPLModel(int lineid)
        {
            OPLModel oplmodel = new OPLModel()
            {
                Line_Id = lineid,
                Reference = "CIP",
                Active = true
            };

            try
            {
                Console.Write("salji");
                _context.OPLModels.Add(oplmodel);
                 _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            return NoContent();

        }

        private bool OPLModelExists(int id)
        {
            return _context.OPLModels.Any(e => e.Id == id);
        }
    }
}
