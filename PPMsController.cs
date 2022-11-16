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
    public class PPMsController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public PPMsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/PPMs
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<PPM>>> GetPPMs()
        {
            return await _context.PPMs.ToListAsync();
        }*/

        // GET: api/PPMs?lineid=1&dat=2020-01
        [HttpGet]
        //ovde bi trebala da bude logika da se uzimaju smene iz ppm modela gde je zadata linija i datum
        public async Task<ActionResult<IEnumerable<ShiftViewModel>>> GetPPM(int? lineid, string dat)
        {
            DateTime localDate = DateTime.Now;
            int month;
            int year;
            string pp = dat;
            if (dat != null && dat != "undefined")
            {
                year = Int32.Parse(dat.Substring(0, 4));
                month = Int32.Parse(dat.Substring(5)); //IZMENITI OVAJ NACIN, TREBA DA STOJE DVE CIFRE
            }
            else
            {
                month = localDate.Month;
                year = localDate.Year;
            }
            var ppm = await _context.PPMs.Where(i => i.Line_id == lineid && i.Date.Month == month && i.Date.Year == year).ToListAsync();
            string linename = _context.LineModels.Where(i => i.Line_id == lineid).Select(i => i.Short_line_name).FirstOrDefault();
            List<int> shifts = await _context.PPMs.Where(i => i.Date.Month == month && i.Date.Year == year).Select(i => i.Shift_Id).Distinct().ToListAsync();
            List<ShiftViewModel> shiftslist = new List<ShiftViewModel>();

            foreach(var shift in shifts)
            {
                string shiftname=  _context.ShiftModels.Where(i => i.Shift_id == shift).Select(i => i.Shift_duration).FirstOrDefault();
                ShiftViewModel shiftview = new ShiftViewModel();
                shiftview.Line_id= lineid ?? default(int);
                shiftview.Shift_id = shift;
                shiftview.Shift_name = shiftname;
                shiftview.Date = ppm[0].Date; //proveriti
                shiftslist.Add(shiftview);
            }
            return shiftslist;
        }

        // GET: api/PPMs/shift/?lineid=1&shiftid=1
        [HttpGet]
        [Route("shift")]
        public async Task<ActionResult<PPMViewModel>> GetPPM(int? lineid, int? shiftid, string dat)
        {
            /*DateTime localDate = DateTime.Now;
            int year = localDate.Year;
            int month = localDate.Month;
            DateTime oDate = Convert.ToDateTime(year + "-" + month + "-01");*/

            DateTime localDate = DateTime.Now;
            int day;
            int month;
            int year;
            string ppp = dat;
            if (dat != null && dat != "undefined")
            {
                year = Int32.Parse(dat.Substring(0, 4));
                month = Int32.Parse(dat.Substring(5));//IZMENITI OVAJ NACIN, TREBA DA STOJE DVE CIFRE
                //day = Int32.Parse(dat.Substring(8, 2));
            }
            else
            {
                //day = localDate.Day;
                month = localDate.Month;
                year = localDate.Year;
            }

            var ppm = await _context.PPMs.Where(i=>i.Line_id==lineid && i.Shift_Id==shiftid && i.Date.Month==month && i.Date.Year == year).ToListAsync();
            string linename = _context.LineModels.Where(i => i.Line_id == lineid).Select(i=> i.Short_line_name).FirstOrDefault();
            int mseid= _context.LineModels.Where(i => i.Line_id == lineid).Select(i=> i.MSE_id).FirstOrDefault();
            string msename= _context.MSEs.Where(i => i.MSE_id == mseid).Select(i => i.name_abrv).FirstOrDefault();
            var target = await _context.TargetPPMs.Where(i=> i.Date.Month==month && i.Date.Year==year && i.Line_id==lineid).ToListAsync();
            if (ppm == null)
            {
                return NotFound();
            }

            List<PPMViewModel> ppmlist = new List<PPMViewModel>();
            PPMViewModel ppmview = new PPMViewModel();

            ppmview.ppm_id = ppm[0].Id;
            ppmview.shift = ppm[0].Shift_Id;
            ppmview.line_id = ppm[0].Line_id;
            ppmview.line_name = linename;
            ppmview.mse_name = msename;
            ppmview.date = ppm[0].Date;
            ppmview.opscrap1 = ppm[0].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap2 = ppm[1].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap3 = ppm[2].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap4 = ppm[3].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap5 = ppm[4].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap6 = ppm[5].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap7 = ppm[6].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap8 = ppm[7].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap9 = ppm[8].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap10 = ppm[9].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap11 = ppm[10].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap12 = ppm[11].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap13 = ppm[12].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap14 = ppm[13].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap15 = ppm[14].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap16 = ppm[15].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap17 = ppm[16].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap18 = ppm[17].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap19 = ppm[18].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap20 = ppm[19].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap21 = ppm[20].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap22 = ppm[21].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap23 = ppm[22].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap24 = ppm[23].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap25 = ppm[24].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap26 = ppm[25].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap27 = ppm[26].Operator_scrap.GetValueOrDefault();
            ppmview.opscrap28 = ppm[27].Operator_scrap.GetValueOrDefault();
            try
            {
                ppmview.opscrap29 = ppm[28].Operator_scrap.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.opscrap29 = null;
            }
            try
            {
                ppmview.opscrap30 = ppm[29].Operator_scrap.GetValueOrDefault();
            }
            catch (Exception)
            {
                ppmview.opscrap30 = null;
            }
            try
            {
                ppmview.opscrap31 = ppm[30].Operator_scrap.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.opscrap31 = null;
            }
            

            ppmview.supscrap1 = ppm[0].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap2 = ppm[1].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap3 = ppm[2].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap4 = ppm[3].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap5 = ppm[4].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap6 = ppm[5].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap7 = ppm[6].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap8 = ppm[7].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap9 = ppm[8].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap10 = ppm[9].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap11 = ppm[10].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap12 = ppm[11].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap13 = ppm[12].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap14 = ppm[13].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap15 = ppm[14].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap16 = ppm[15].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap17 = ppm[16].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap18 = ppm[17].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap19 = ppm[18].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap20 = ppm[19].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap21 = ppm[20].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap22 = ppm[21].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap23 = ppm[22].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap24 = ppm[23].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap25 = ppm[24].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap26 = ppm[25].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap27 = ppm[26].Supplier_scrap.GetValueOrDefault();
            ppmview.supscrap28 = ppm[27].Supplier_scrap.GetValueOrDefault();
            try
            {
                ppmview.supscrap29 = ppm[28].Supplier_scrap.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.supscrap29 = null;
            }
            try
            {
                ppmview.supscrap30 = ppm[29].Supplier_scrap.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.supscrap30 = null;
            }
            try
            {
                ppmview.supscrap31 = ppm[30].Supplier_scrap.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.supscrap31 = null;
            }

            ppmview.target1 = target[0].Value.GetValueOrDefault();
            ppmview.target2 = target[1].Value.GetValueOrDefault();
            ppmview.target3 = target[2].Value.GetValueOrDefault();
            ppmview.target4 = target[3].Value.GetValueOrDefault();
            ppmview.target5 = target[4].Value.GetValueOrDefault();
            ppmview.target6 = target[5].Value.GetValueOrDefault();
            ppmview.target7 = target[6].Value.GetValueOrDefault();
            ppmview.target8 = target[7].Value.GetValueOrDefault();
            ppmview.target9 = target[8].Value.GetValueOrDefault();
            ppmview.target10 = target[9].Value.GetValueOrDefault();
            ppmview.target11 = target[10].Value.GetValueOrDefault();
            ppmview.target12 = target[11].Value.GetValueOrDefault();
            ppmview.target13 = target[12].Value.GetValueOrDefault();
            ppmview.target14 = target[13].Value.GetValueOrDefault();
            ppmview.target15 = target[14].Value.GetValueOrDefault();
            ppmview.target16 = target[15].Value.GetValueOrDefault();
            ppmview.target17 = target[16].Value.GetValueOrDefault();
            ppmview.target18 = target[17].Value.GetValueOrDefault();
            ppmview.target19 = target[18].Value.GetValueOrDefault();
            ppmview.target20 = target[19].Value.GetValueOrDefault();
            ppmview.target21 = target[20].Value.GetValueOrDefault();
            ppmview.target22 = target[21].Value.GetValueOrDefault();
            ppmview.target23 = target[22].Value.GetValueOrDefault();
            ppmview.target24 = target[23].Value.GetValueOrDefault();
            ppmview.target25 = target[24].Value.GetValueOrDefault();
            ppmview.target26 = target[25].Value.GetValueOrDefault();
            ppmview.target27 = target[26].Value.GetValueOrDefault();
            ppmview.target28 = target[27].Value.GetValueOrDefault();
            try
            {
                ppmview.target29 = target[28].Value.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.target29 = null;
            }
            try
            {
                ppmview.target30 = target[29].Value.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.target30 = null;
            }
            try
            {
                ppmview.target31 = target[30].Value.GetValueOrDefault();
            }
            catch (Exception)
            {

                ppmview.target31 = null;
            }

            return ppmview;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostPPM(int id, PPMViewModel ppmview)
        {
            PPM ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Id == id).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap1;
            ppm.Supplier_scrap = ppmview.supscrap1;

            TargetPPM tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target1;

            if (ppmview.date != null)
            {
                ppm = new PPM();
                //ppmview.line_id
                ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(1) && i.Line_id == ppmview.line_id && i.Shift_Id == ppmview.shift).FirstOrDefault();
                _context.PPMs.Attach(ppm);
                ppm.Operator_scrap = ppmview.opscrap2;
                ppm.Supplier_scrap = ppmview.supscrap2;
                tar = new TargetPPM();
                tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(1) && i.Line_id == ppmview.line_id).FirstOrDefault();
                _context.TargetPPMs.Attach(tar);
                tar.Value = ppmview.target2;
            }

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(2) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap3;
            ppm.Supplier_scrap = ppmview.supscrap3;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(2) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target3;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(3) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap4;
            ppm.Supplier_scrap = ppmview.supscrap4;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(3) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target4;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(4) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap5;
            ppm.Supplier_scrap = ppmview.supscrap5;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(4) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target5;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(5) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap6;
            ppm.Supplier_scrap = ppmview.supscrap6;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(5) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target6;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(6) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap7;
            ppm.Supplier_scrap = ppmview.supscrap7;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(6) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target7;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(7) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap8;
            ppm.Supplier_scrap = ppmview.supscrap8;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(7) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target8;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(8) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap9;
            ppm.Supplier_scrap = ppmview.supscrap9;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(8) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target9;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(9) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap10;
            ppm.Supplier_scrap = ppmview.supscrap10;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(9) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target10;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(10) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap11;
            ppm.Supplier_scrap = ppmview.supscrap11;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(10) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target11;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(11) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap12;
            ppm.Supplier_scrap = ppmview.supscrap12;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(11) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target12;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(12) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap13;
            ppm.Supplier_scrap = ppmview.supscrap13;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(12) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target13;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(13) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap14;
            ppm.Supplier_scrap = ppmview.supscrap14;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(13) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target14;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(14) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap15;
            ppm.Supplier_scrap = ppmview.supscrap15;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(14) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target15;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(15) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap16;
            ppm.Supplier_scrap = ppmview.supscrap16;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(15) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target16;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(16) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap17;
            ppm.Supplier_scrap = ppmview.supscrap17;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(16) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target17;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(17) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap18;
            ppm.Supplier_scrap = ppmview.supscrap18;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(17) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target18;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(18) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap19;
            ppm.Supplier_scrap = ppmview.supscrap19;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(18) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target19;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(19) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap20;
            ppm.Supplier_scrap = ppmview.supscrap20;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(19) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target20;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(20) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap21;
            ppm.Supplier_scrap = ppmview.supscrap21;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(20) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target21;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(21) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap22;
            ppm.Supplier_scrap = ppmview.supscrap22;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(21) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target22;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(22) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap23;
            ppm.Supplier_scrap = ppmview.supscrap23;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(22) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target23;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(23) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap24;
            ppm.Supplier_scrap = ppmview.supscrap24;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(23) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target24;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(24) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap25;
            ppm.Supplier_scrap = ppmview.supscrap25;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(24) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target25;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(25) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap26;
            ppm.Supplier_scrap = ppmview.supscrap26;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(25) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target26;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(26) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap27;
            ppm.Supplier_scrap = ppmview.supscrap27;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(26) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target27;

            ppm = new PPM();
            ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(27) && i.Line_id == ppmview.line_id && i.Shift_Id==ppmview.shift).FirstOrDefault();
            _context.PPMs.Attach(ppm);
            ppm.Operator_scrap = ppmview.opscrap28;
            ppm.Supplier_scrap = ppmview.supscrap28;
            tar = new TargetPPM();
            tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(27) && i.Line_id == ppmview.line_id).FirstOrDefault();
            _context.TargetPPMs.Attach(tar);
            tar.Value = ppmview.target28;


            int dani = DateTime.DaysInMonth(ppmview.date.Year, ppm.Date.Month);

            if (dani >= 29)
            {
                ppm = new PPM();
                ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(28) && i.Line_id == ppmview.line_id && i.Shift_Id == ppmview.shift).FirstOrDefault();
                _context.PPMs.Attach(ppm);
                ppm.Operator_scrap = ppmview.opscrap29;
                ppm.Supplier_scrap = ppmview.supscrap29;
                tar = new TargetPPM();
                tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(28) && i.Line_id == ppmview.line_id).FirstOrDefault();
                _context.TargetPPMs.Attach(tar);
                tar.Value = ppmview.target29;
            }

            if (dani >= 30)
            {
                ppm = new PPM();
                ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(29) && i.Line_id == ppmview.line_id && i.Shift_Id == ppmview.shift).FirstOrDefault();
                _context.PPMs.Attach(ppm);
                ppm.Operator_scrap = ppmview.opscrap30;
                ppm.Supplier_scrap = ppmview.supscrap30;
                tar = new TargetPPM();
                tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(29) && i.Line_id == ppmview.line_id).FirstOrDefault();
                _context.TargetPPMs.Attach(tar);
                tar.Value = ppmview.target30;
            }

            if (dani >= 31)
            {
                ppm = new PPM();
                ppm = _context.PPMs.Where(i => i.Date == ppmview.date.AddDays(30) && i.Line_id == ppmview.line_id && i.Shift_Id == ppmview.shift).FirstOrDefault();
                _context.PPMs.Attach(ppm);
                ppm.Operator_scrap = ppmview.opscrap31;
                ppm.Supplier_scrap = ppmview.supscrap31;
                tar = new TargetPPM();
                tar = _context.TargetPPMs.Where(i => i.Date == ppmview.date.AddDays(30) && i.Line_id == ppmview.line_id).FirstOrDefault();
                _context.TargetPPMs.Attach(tar);
                tar.Value = ppmview.target31;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
            return NoContent();
        }


        //za post nam treba id reda koji postujemo i ceo model 
        // POST: api/PPMs
        /*[HttpPost]
        public async Task<ActionResult<PPM>> PostPPM(PPM pPM)
        {
            _context.PPMs.Add(pPM);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPPM", new { id = pPM.Id }, pPM);
        }*/


        // PUT: api/PPMs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPPM(int id, PPM pPM)
        {
            if (id != pPM.Id)
            {
                return BadRequest();
            }

            _context.Entry(pPM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PPMExists(id))
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

        
        private bool PPMExists(int id)
        {
            return _context.PPMs.Any(e => e.Id == id);
        }
    }
}
