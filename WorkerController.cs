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
    public class WorkerController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public WorkerController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Worker/?line_id=1&dat=2020-02-17
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftViewModel>>> GetWorker(int line_id, string dat)
        {
            DateTime localDate = DateTime.Now;
            int day;
            int month;
            int year;
            if (dat != null)
            {
                year = Int32.Parse(dat.Substring(0, 4));
                month = Int32.Parse(dat.Substring(5, 2));
                day = Int32.Parse(dat.Substring(8, 2));
            }
            else
            {
                day = localDate.Day;
                month = localDate.Month;
                year = localDate.Year;
            }
            List<int> shifts = await _context.Workers.Where(i => i.Date.Day == day && i.Date.Month == month && i.Date.Year == year && i.Line_id == line_id).Select(i => i.Shift_id).Distinct().ToListAsync();

            List<ShiftViewModel> shiftslist = new List<ShiftViewModel>();

            foreach (var shift in shifts)
            {
                string shiftname = _context.ShiftModels.Where(i => i.Shift_id == shift).Select(i => i.Shift_duration).FirstOrDefault();
                ShiftViewModel shiftview = new ShiftViewModel();
                shiftview.Line_id = line_id;
                shiftview.Shift_id = shift;
                shiftview.Shift_name = shiftname;
                shiftview.Date = localDate;
                shiftslist.Add(shiftview);
            }

            return shiftslist;

        }

        // GET: api/Worker/shift/?line_id=1&shift_id=2&dat=2020-02-17
        [HttpGet]
        [Route("shift")]
        public async Task<ActionResult<Worker>> GetWorkerByShift(int line_id, int shift_id, string dat)
        {
            DateTime localDate = DateTime.Now;
            int day;
            int month;
            int year;
            if (dat != null)
            {
                year = Int32.Parse(dat.Substring(0, 4));
                month = Int32.Parse(dat.Substring(5, 2));
                day = Int32.Parse(dat.Substring(8, 2));
            }
            else
            {
                day = localDate.Day;
                month = localDate.Month;
                year = localDate.Year;
            }

            var row =  _context.Workers.Where(i => i.Date.Day == day && i.Date.Month == month && i.Date.Year == year && i.Line_id == line_id && i.Shift_id == shift_id).FirstOrDefault();
            string shiftduration = _context.ShiftModels.Where(i => i.Shift_id == row.Shift_id).Select(i => i.Shift_duration).FirstOrDefault();
            string linename = _context.LineModels.Where(i => i.Line_id == line_id).Select(i => i.Short_line_name).FirstOrDefault();
            int mse_id = _context.LineModels.Where(i => i.Line_id == line_id).Select(i => i.MSE_id).FirstOrDefault();
            string mse_name = _context.MSEs.Where(i => i.MSE_id == mse_id).Select(i => i.name_abrv).FirstOrDefault();
            row.Line_name = linename;
            row.Department = mse_name;
            row.Shift_duration = shiftduration;
            
            if (row != null)
            {
                return row;
            }
            return NotFound();          
        }

        // POST: api/Workers
        [HttpPost("{id}")]
        public async Task<ActionResult<Worker>> PostWorkerNum(int id, Worker workerNum)
        {
            if (id != workerNum.Id)
            {
                return BadRequest();
            }

            _context.Entry(workerNum).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerNumExists(id))
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

        //// GET: api/Worker/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Worker>> GetWorkerNum(int id)
        //{
        //    var workerNum = await _context.Workers.FindAsync(id);

        //    if (workerNum == null)
        //    {
        //        return NotFound();
        //    }

        //    return workerNum;
        //}

        // PUT: api/Workers/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWorkerNum(int id, Worker workerNum)
        //{
        //    if (id != workerNum.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(workerNum).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WorkerNumExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        // DELETE: api/Workers/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Worker>> DeleteWorkerNum(int id)
        //{
        //    var workerNum = await _context.Workers.FindAsync(id);
        //    if (workerNum == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Workers.Remove(workerNum);
        //    await _context.SaveChangesAsync();

        //    return workerNum;
        //}

        private bool WorkerNumExists(int id)
        {
            return _context.Workers.Any(e => e.Id == id);
        }
    }
}
