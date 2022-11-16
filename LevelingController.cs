using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2.Models;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelingController : ControllerBase
    {
        private readonly Models.AuthenticationContext _context;

        public LevelingController(Models.AuthenticationContext context)
        {
            _context = context;
        }


        // GET: api/Leveling
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<LevelingModel>>> GetLeveling()
        //{
        //    DateTime localDate = DateTime.Now;
        //    int year = localDate.Year;
        //    int month = localDate.Month;
        //    DateTime oDate = Convert.ToDateTime(year + "-" + month + "-01");

        //    var leveling = await _context.LevelingModels.Where(i => i.Date == oDate).ToListAsync();
        //    return leveling;
        //}

        // GET: api/Leveling
        //[HttpGet]
        //public  async Task<ActionResult<LevelingModel>> GetLeveling(int l, string d)
        //{
        //    DateTime oDate = Convert.ToDateTime(d + "-01");
        //    var leveling = await _context.LevelingModels.FirstOrDefaultAsync(i => i.Line_id == l && i.Date == oDate);
        //    return leveling;
        //}

        [HttpGet]
        public async Task<ActionResult<LevelingViewModel>> GetLeveling(int? lin_id, string dat)
        {
            DateTime localDate = DateTime.Now;
            int year = localDate.Year;
            int month = localDate.Month;
            //DateTime oDate = localDate;
            if (dat != null)
            {
                year = Int32.Parse(dat.Substring(0, 4));
                month = Int32.Parse(dat.Substring(5));

            }
            DateTime oDate = Convert.ToDateTime(year + "-" + month + "-01");

            List<int> linije = await _context.LineModels.Select(i => i.Line_id).Distinct().ToListAsync();
            if (lin_id != null)
            {
                linije.Clear(); ;
                linije.Add(lin_id.GetValueOrDefault());
            }



            List<LevelingViewModel> leveli = new List<LevelingViewModel>();
            LevelingViewModel lev = new LevelingViewModel();
            foreach (int l in linije)
            {
                var lev1 = await _context.Leveling_modifModels.Where(i => i.Date.Month == month && i.Date.Year == year && i.Line_id == l).OrderBy(i => i.Date).ToListAsync();
                if (lev1.Count == 0) { return null; }
                lev.Date = oDate;
                var lin = await _context.LineModels.Where(i => i.Line_id == l).ToListAsync();
                lev.line_name = lin[0].Short_line_name;
                var ms = await _context.MSEs.Where(i => i.MSE_id == lin[0].MSE_id).ToListAsync();
                lev.mse = ms[0].name_abrv;
                lev.mse_id = ms[0].MSE_id;
                lev.Line_id = l; //vratiti naziv linije

                lev.leveling_id = lev1[0].Leveling_id;
                lev.lot1 = lev1[0].lot.GetValueOrDefault();
                lev.lot2 = lev1[1].lot.GetValueOrDefault();
                lev.lot3 = lev1[2].lot.GetValueOrDefault();
                lev.lot4 = lev1[3].lot.GetValueOrDefault();
                lev.lot5 = lev1[4].lot.GetValueOrDefault();
                lev.lot6 = lev1[5].lot.GetValueOrDefault();
                lev.lot7 = lev1[6].lot.GetValueOrDefault();
                lev.lot8 = lev1[7].lot.GetValueOrDefault();
                lev.lot9 = lev1[8].lot.GetValueOrDefault();
                lev.lot10 = lev1[9].lot.GetValueOrDefault();
                lev.lot11 = lev1[10].lot.GetValueOrDefault();
                lev.lot12 = lev1[11].lot.GetValueOrDefault();
                lev.lot13 = lev1[12].lot.GetValueOrDefault();
                lev.lot14 = lev1[13].lot.GetValueOrDefault();
                lev.lot15 = lev1[14].lot.GetValueOrDefault();
                lev.lot16 = lev1[15].lot.GetValueOrDefault();
                lev.lot17 = lev1[16].lot.GetValueOrDefault();
                lev.lot18 = lev1[17].lot.GetValueOrDefault();
                lev.lot19 = lev1[18].lot.GetValueOrDefault();
                lev.lot20 = lev1[19].lot.GetValueOrDefault();
                lev.lot21 = lev1[20].lot.GetValueOrDefault();
                lev.lot22 = lev1[21].lot.GetValueOrDefault();
                lev.lot23 = lev1[22].lot.GetValueOrDefault();
                lev.lot24 = lev1[23].lot.GetValueOrDefault();
                lev.lot25 = lev1[24].lot.GetValueOrDefault();
                lev.lot26 = lev1[25].lot.GetValueOrDefault();
                lev.lot27 = lev1[26].lot.GetValueOrDefault();
                lev.lot28 = lev1[27].lot.GetValueOrDefault();
                try
                {
                    lev.lot29 = lev1[28].lot.GetValueOrDefault();
                }
                catch
                {
                    lev.lot29 = null;
                }
                try
                {
                    lev.lot30 = lev1[29].lot.GetValueOrDefault();
                }
                catch { lev.lot30 = null; }
                try
                {
                    lev.lot31 = lev1[30].lot.GetValueOrDefault();
                }
                catch
                {
                    lev.lot31 = null;
                }
                lev.lotfull1 = lev1[0].lotfull.GetValueOrDefault();
                lev.lotfull2 = lev1[1].lotfull.GetValueOrDefault();
                lev.lotfull3 = lev1[2].lotfull.GetValueOrDefault();
                lev.lotfull4 = lev1[3].lotfull.GetValueOrDefault();
                lev.lotfull5 = lev1[4].lotfull.GetValueOrDefault();
                lev.lotfull6 = lev1[5].lotfull.GetValueOrDefault();
                lev.lotfull7 = lev1[6].lotfull.GetValueOrDefault();
                lev.lotfull8 = lev1[7].lotfull.GetValueOrDefault();
                lev.lotfull9 = lev1[8].lotfull.GetValueOrDefault();
                lev.lotfull10 = lev1[9].lotfull.GetValueOrDefault();
                lev.lotfull11 = lev1[10].lotfull.GetValueOrDefault();
                lev.lotfull12 = lev1[11].lotfull.GetValueOrDefault();
                lev.lotfull13 = lev1[12].lotfull.GetValueOrDefault();
                lev.lotfull14 = lev1[13].lotfull.GetValueOrDefault();
                lev.lotfull15 = lev1[14].lotfull.GetValueOrDefault();
                lev.lotfull16 = lev1[15].lotfull.GetValueOrDefault();
                lev.lotfull17 = lev1[16].lotfull.GetValueOrDefault();
                lev.lotfull18 = lev1[17].lotfull.GetValueOrDefault();
                lev.lotfull19 = lev1[18].lotfull.GetValueOrDefault();
                lev.lotfull20 = lev1[19].lotfull.GetValueOrDefault();
                lev.lotfull21 = lev1[20].lotfull.GetValueOrDefault();
                lev.lotfull22 = lev1[21].lotfull.GetValueOrDefault();
                lev.lotfull23 = lev1[22].lotfull.GetValueOrDefault();
                lev.lotfull24 = lev1[23].lotfull.GetValueOrDefault();
                lev.lotfull25 = lev1[24].lotfull.GetValueOrDefault();
                lev.lotfull26 = lev1[25].lotfull.GetValueOrDefault();
                lev.lotfull27 = lev1[26].lotfull.GetValueOrDefault();
                lev.lotfull28 = lev1[27].lotfull.GetValueOrDefault();

                try
                {
                    lev.lotfull29 = lev1[28].lotfull.GetValueOrDefault();
                }
                catch { lev.lotfull29 = null; }
                try
                {
                    lev.lotfull30 = lev1[29].lotfull.GetValueOrDefault();
                }
                catch { lev.lotfull30 = null; }
                try
                {
                    lev.lotfull31 = lev1[30].lotfull.GetValueOrDefault();
                }
                catch { lev.lotfull31 = null; }

                lev.target1 = lev1[0].target.GetValueOrDefault();
                lev.target2 = lev1[1].target.GetValueOrDefault();
                lev.target3 = lev1[2].target.GetValueOrDefault();
                lev.target4 = lev1[3].target.GetValueOrDefault();
                lev.target5 = lev1[4].target.GetValueOrDefault();
                lev.target6 = lev1[5].target.GetValueOrDefault();
                lev.target7 = lev1[6].target.GetValueOrDefault();
                lev.target8 = lev1[7].target.GetValueOrDefault();
                lev.target9 = lev1[8].target.GetValueOrDefault();
                lev.target10 = lev1[9].target.GetValueOrDefault();
                lev.target11 = lev1[10].target.GetValueOrDefault();
                lev.target12 = lev1[11].target.GetValueOrDefault();
                lev.target13 = lev1[12].target.GetValueOrDefault();
                lev.target14 = lev1[13].target.GetValueOrDefault();
                lev.target15 = lev1[14].target.GetValueOrDefault();
                lev.target16 = lev1[15].target.GetValueOrDefault();
                lev.target17 = lev1[16].target.GetValueOrDefault();
                lev.target18 = lev1[17].target.GetValueOrDefault();
                lev.target19 = lev1[18].target.GetValueOrDefault();
                lev.target20 = lev1[19].target.GetValueOrDefault();
                lev.target21 = lev1[20].target.GetValueOrDefault();
                lev.target22 = lev1[21].target.GetValueOrDefault();
                lev.target23 = lev1[22].target.GetValueOrDefault();
                lev.target24 = lev1[23].target.GetValueOrDefault();
                lev.target25 = lev1[24].target.GetValueOrDefault();
                lev.target26 = lev1[25].target.GetValueOrDefault();
                lev.target27 = lev1[26].target.GetValueOrDefault();
                lev.target28 = lev1[27].target.GetValueOrDefault();

                try
                {
                    lev.target29 = lev1[28].target.GetValueOrDefault();
                }
                catch { lev.target29 = null; }
                try
                {
                    lev.target30 = lev1[29].target.GetValueOrDefault();
                }
                catch { lev.target30 = null; }
                try
                {
                    lev.target31 = lev1[30].target.GetValueOrDefault();
                }
                catch { lev.target31 = null; }

                leveli.Add(lev);


            }

            return new ObjectResult(lev);
            //var leveling = await _context.Leveling_modifModels.Where(i => i.Date.Month == month).ToListAsync();
            //return leveling;
        }




        // POST: api/Leveling/5
        [HttpPost]
        [Authorize(Roles = "Admin, LineLider,ShiftLeader")]
        //public async Task<IActionResult> PutLeveling(int id, LevelingModel LevelingModel)
        public async Task<IActionResult> PutLeveling(int id,int edit_level, LevelingViewModel LevelingViewModel)
        {


            
            if (id != LevelingViewModel.leveling_id)
            {
                return BadRequest();
            }




            //_context.Entry(LevelingModel).State = EntityState.Modified;

            Leveling_modif lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Leveling_id == id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot1;
            lev.lotfull = LevelingViewModel.lotfull1;
            if (edit_level == 1) { lev.target = LevelingViewModel.target1; }
            

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(1) && I.Line_id== LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot2;
            lev.lotfull = LevelingViewModel.lotfull2;
           
            if (edit_level == 1) { lev.target = LevelingViewModel.target2; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(2) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot3;
            lev.lotfull = LevelingViewModel.lotfull3;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target3; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(3) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot4;
            lev.lotfull = LevelingViewModel.lotfull4;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target4; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(4) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot5;
            lev.lotfull = LevelingViewModel.lotfull5;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target5; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(5) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot6;
            lev.lotfull = LevelingViewModel.lotfull6;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target6; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(6) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot7;
            lev.lotfull = LevelingViewModel.lotfull7;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target7; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(7) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot8;
            lev.lotfull = LevelingViewModel.lotfull8;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target8; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(8) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot9;
            lev.lotfull = LevelingViewModel.lotfull9;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target9; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(9) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot10;
            lev.lotfull = LevelingViewModel.lotfull10;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target10; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(10) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot11;
            lev.lotfull = LevelingViewModel.lotfull11;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target11; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(11) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot12;
            lev.lotfull = LevelingViewModel.lotfull12;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target12; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(12) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot13;
            lev.lotfull = LevelingViewModel.lotfull13;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target13; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(13) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot14;
            lev.lotfull = LevelingViewModel.lotfull14;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target14; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(14) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot15;
            lev.lotfull = LevelingViewModel.lotfull15;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target15; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(15) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot16;
            lev.lotfull = LevelingViewModel.lotfull16;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target16; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(16) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot17;
            lev.lotfull = LevelingViewModel.lotfull17;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target17; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(17) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot18;
            lev.lotfull = LevelingViewModel.lotfull18;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target18; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(18) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot19;
            lev.lotfull = LevelingViewModel.lotfull19;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target19; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(19) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot20;
            lev.lotfull = LevelingViewModel.lotfull20;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target20; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(20) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot21;
            lev.lotfull = LevelingViewModel.lotfull21;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target21; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(21) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot22;
            lev.lotfull = LevelingViewModel.lotfull22;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target22; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(22) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot23;
            lev.lotfull = LevelingViewModel.lotfull23;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target23; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(23) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot24;
            lev.lotfull = LevelingViewModel.lotfull24;
           
            if (edit_level == 1) { lev.target = LevelingViewModel.target24; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(24) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot25;
            lev.lotfull = LevelingViewModel.lotfull25;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target25; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(25) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot26;
            lev.lotfull = LevelingViewModel.lotfull26;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target26; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(26) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot27;
            lev.lotfull = LevelingViewModel.lotfull27;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target27; }

            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(27) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot28;
            lev.lotfull = LevelingViewModel.lotfull28;
            
            int dani = DateTime.DaysInMonth(LevelingViewModel.Date.Year, LevelingViewModel.Date.Month);
            if (edit_level == 1) { lev.target = LevelingViewModel.target28; }

            if (dani >= 29)
            {
                lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(28) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot29;
            lev.lotfull = LevelingViewModel.lotfull29;
            
            if (edit_level == 1) { lev.target = LevelingViewModel.target29; }
            }
            if (dani >= 30)
            {
            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(29) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot30;
            lev.lotfull = LevelingViewModel.lotfull30;
           
            if (edit_level == 1) { lev.target = LevelingViewModel.target30; }
            }
            if (dani >= 31)
            {
            lev = new Leveling_modif();
            lev = _context.Leveling_modifModels.Where(I => I.Date == LevelingViewModel.Date.AddDays(30) && I.Line_id == LevelingViewModel.Line_id).FirstOrDefault();
            _context.Leveling_modifModels.Attach(lev);
            lev.lot = LevelingViewModel.lot31;
            lev.lotfull = LevelingViewModel.lotfull31;
            
                if (edit_level == 1) { lev.target = LevelingViewModel.target31; }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException )
            {
                //if (!LevelingExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }

            return NoContent();
        }

        // GET: api/Leveling/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<LevelingModel>> GetLeveling(int id)
        //{
        //    DateTime oDate = Convert.ToDateTime(d+"-01");
        //    var leveling = await _context.LevelingModels.FirstOrDefaultAsync(i => i.Line_id == l &&  i.Date == oDate);

        //    if (leveling == null)
        //    {
        //        return NotFound();
        //    }

        //    return leveling;
        //}

        // POST: api/Leveling
        //[HttpPost]
        //public async Task<ActionResult<LevelingModel>> PostLeveling(LevelingModel levelingModel)
        //{
        //    _context.LevelingModels.Add(levelingModel);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLeveling", new { id = levelingModel.Leveling_id }, levelingModel);
        //}

        //// DELETE: api/Leveling/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<LevelingModel>> DeleteLeveling(int id)
        //{
        //    var LevelingModel = await _context.LevelingModels.FindAsync(id);
        //    if (LevelingModel == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.LevelingModels.Remove(LevelingModel);
        //    await _context.SaveChangesAsync();

        //    return LevelingModel;
        //}

        //private bool LevelingExists(int id)
        //{
        //    return _context.LevelingModels.Any(e => e.Leveling_id == id);
        //}
    }
}