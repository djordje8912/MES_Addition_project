using System;
using System.Collections.Generic;
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
    public class ShiftModelsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ShiftModelsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/ShiftModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShiftModels()
        {
            return await _context.ShiftModels.ToListAsync();
        }

        // GET: api/ShiftModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShiftModel(int id)
        {
            var shiftModel = await _context.ShiftModels.FindAsync(id);

            if (shiftModel == null)
            {
                return NotFound();
            }

            return shiftModel;
        }

        // PUT: api/ShiftModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftModel(int id, ShiftModel shiftModel)
        {
            if (id != shiftModel.Shift_id)
            {
                return BadRequest();
            }

            _context.Entry(shiftModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftModelExists(id))
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

        // POST: api/ShiftModels
        [HttpPost]
        public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
        {
            _context.ShiftModels.Add(shiftModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftModel", new { id = shiftModel.Shift_id }, shiftModel);
        }

        // DELETE: api/ShiftModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShiftModel>> DeleteShiftModel(int id)
        {
            var shiftModel = await _context.ShiftModels.FindAsync(id);
            if (shiftModel == null)
            {
                return NotFound();
            }

            _context.ShiftModels.Remove(shiftModel);
            await _context.SaveChangesAsync();

            return shiftModel;
        }

        private bool ShiftModelExists(int id)
        {
            return _context.ShiftModels.Any(e => e.Shift_id == id);
        }
    }
}
