using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebApi2.Models;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftleaderController : ControllerBase
    {
        private readonly Models.AuthenticationContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ShiftleaderController(Models.AuthenticationContext context)
        {
            _context = context;
        }


     


 
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<LineLiderViewModel>>> GetLineLider(int? lin_id)
        public async Task<ActionResult<IEnumerable<LineLiderViewModel>>> GetLineLider()
        {
            DateTime localDate = DateTime.Now;
            int year = localDate.Year;
            int month = localDate.Month;
            DateTime oDate = Convert.ToDateTime(year + "-" + month + "-01");
            List<int> linije= new List<int> ();/* = await _context.Customer_incidModels.Where(i => i.Datum.Month == month).Select(i => i.line_id).Distinct().ToListAsync();*/

            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            List<int> linija_rola = await _context.LLRoleModels.Where(i => i.UserId== userId).Select(i => i.line_id).ToListAsync();

            //lin_id = linija_rola;
            //if (linija_rola != null)
            int duzina=linija_rola.Count;
            if (duzina == 1)
            {
                linije.Clear(); ;
                linije.Add(linija_rola[0]);
            }
            else { linije = linija_rola; }

            List<LineLiderViewModel> ll = new List<LineLiderViewModel>();
            List<Customer_incidViewModel> leveli2 = new List<Customer_incidViewModel>();

            foreach (int l in linije)
            {
                var ci1 = await _context.Customer_incidModels.Where(i => i.Date.Month == month && i.line_id == l).OrderBy(i => i.Date).ToListAsync();
                LineLiderViewModel linel = new LineLiderViewModel();
                linel.Datum = oDate;
                var lin = await _context.LineModels.Where(i => i.Line_id == l).ToListAsync();
                linel.line_name = lin[0].Short_line_name;
                var ms = await _context.MSEs.Where(i => i.MSE_id == lin[0].MSE_id).ToListAsync();
                //linel.mse = ms[0].name_abrv;
                //linel.mse_id = ms[0].MSE_id;
                linel.line_id = l; //vratiti naziv linije
                //linel.customer_incid_id = ci1[0].customer_incid_id;
                //linel.cust_incid1 = ci1[0].value.GetValueOrDefault();
                //linel.cust_incid2 = ci1[1].value.GetValueOrDefault();
                //linel.cust_incid3 = ci1[2].value.GetValueOrDefault();
                //linel.cust_incid4 = ci1[3].value.GetValueOrDefault();
                //linel.cust_incid5 = ci1[4].value.GetValueOrDefault();
                //linel.cust_incid6 = ci1[5].value.GetValueOrDefault();
                //linel.cust_incid7 = ci1[6].value.GetValueOrDefault();
                //linel.cust_incid8 = ci1[7].value.GetValueOrDefault();
                //linel.cust_incid9 = ci1[8].value.GetValueOrDefault();
                //linel.cust_incid10 = ci1[9].value.GetValueOrDefault();
                //linel.cust_incid11 = ci1[10].value.GetValueOrDefault();
                //linel.cust_incid12 = ci1[11].value.GetValueOrDefault();
                //linel.cust_incid13 = ci1[12].value.GetValueOrDefault();
                //linel.cust_incid14 = ci1[13].value.GetValueOrDefault();
                //linel.cust_incid15 = ci1[14].value.GetValueOrDefault();
                //linel.cust_incid16 = ci1[15].value.GetValueOrDefault();
                //linel.cust_incid17 = ci1[16].value.GetValueOrDefault();
                //linel.cust_incid18 = ci1[17].value.GetValueOrDefault();
                //linel.cust_incid19 = ci1[18].value.GetValueOrDefault();
                //linel.cust_incid20 = ci1[19].value.GetValueOrDefault();
                //linel.cust_incid21 = ci1[20].value.GetValueOrDefault();
                //linel.cust_incid22 = ci1[21].value.GetValueOrDefault();
                //linel.cust_incid23 = ci1[22].value.GetValueOrDefault();
                //linel.cust_incid24 = ci1[23].value.GetValueOrDefault();
                //linel.cust_incid25 = ci1[24].value.GetValueOrDefault();
                //linel.cust_incid26 = ci1[25].value.GetValueOrDefault();
                //linel.cust_incid27 = ci1[26].value.GetValueOrDefault();
                //linel.cust_incid28 = ci1[27].value.GetValueOrDefault();
                //try
                //{
                //    linel.cust_incid29 = ci1[28].value.GetValueOrDefault();
                //}
                //catch
                //{
                //    linel.cust_incid29 = -1;
                //}
                //try
                //{
                //    linel.cust_incid30 = ci1[29].value.GetValueOrDefault();
                //}
                //catch { linel.cust_incid30 = -1; }
                //try
                //{
                //    linel.cust_incid31 = ci1[30].value.GetValueOrDefault();
                //}
                //catch
                //{
                //    linel.cust_incid31 = -1;
                //}

                //var lev1 = await _context.Leveling_modifModels.Where(i => i.Datum.Month == month && i.line_id == l).OrderBy(i => i.Datum).ToListAsync();
                //linel.leveling_id = lev1[0].Leveling_id;

                //linel.lot1 = lev1[0].lot.GetValueOrDefault();
                //linel.lot2 = lev1[1].lot.GetValueOrDefault();
                //linel.lot3 = lev1[2].lot.GetValueOrDefault();
                //linel.lot4 = lev1[3].lot.GetValueOrDefault();
                //linel.lot5 = lev1[4].lot.GetValueOrDefault();
                //linel.lot6 = lev1[5].lot.GetValueOrDefault();
                //linel.lot7 = lev1[6].lot.GetValueOrDefault();
                //linel.lot8 = lev1[7].lot.GetValueOrDefault();
                //linel.lot9 = lev1[8].lot.GetValueOrDefault();
                //linel.lot10 = lev1[9].lot.GetValueOrDefault();
                //linel.lot11 = lev1[10].lot.GetValueOrDefault();
                //linel.lot12 = lev1[11].lot.GetValueOrDefault();
                //linel.lot13 = lev1[12].lot.GetValueOrDefault();
                //linel.lot14 = lev1[13].lot.GetValueOrDefault();
                //linel.lot15 = lev1[14].lot.GetValueOrDefault();
                //linel.lot16 = lev1[15].lot.GetValueOrDefault();
                //linel.lot17 = lev1[16].lot.GetValueOrDefault();
                //linel.lot18 = lev1[17].lot.GetValueOrDefault();
                //linel.lot19 = lev1[18].lot.GetValueOrDefault();
                //linel.lot20 = lev1[19].lot.GetValueOrDefault();
                //linel.lot21 = lev1[20].lot.GetValueOrDefault();
                //linel.lot22 = lev1[21].lot.GetValueOrDefault();
                //linel.lot23 = lev1[22].lot.GetValueOrDefault();
                //linel.lot24 = lev1[23].lot.GetValueOrDefault();
                //linel.lot25 = lev1[24].lot.GetValueOrDefault();
                //linel.lot26 = lev1[25].lot.GetValueOrDefault();
                //linel.lot27 = lev1[26].lot.GetValueOrDefault();
                //linel.lot28 = lev1[27].lot.GetValueOrDefault();
                //try
                //{
                //    linel.lot29 = lev1[28].lot.GetValueOrDefault();
                //}
                //catch
                //{
                //    linel.lot29 = -1;
                //}
                //try
                //{
                //    linel.lot30 = lev1[29].lot.GetValueOrDefault();
                //}
                //catch { linel.lot30 = -1; }
                //try
                //{
                //    linel.lot31 = lev1[30].lot.GetValueOrDefault();
                //}
                //catch
                //{
                //    linel.lot31 = -1;
                //}
                //linel.lotfull1 = lev1[0].lotfull.GetValueOrDefault();
                //linel.lotfull2 = lev1[1].lotfull.GetValueOrDefault();
                //linel.lotfull3 = lev1[2].lotfull.GetValueOrDefault();
                //linel.lotfull4 = lev1[3].lotfull.GetValueOrDefault();
                //linel.lotfull5 = lev1[4].lotfull.GetValueOrDefault();
                //linel.lotfull6 = lev1[5].lotfull.GetValueOrDefault();
                //linel.lotfull7 = lev1[6].lotfull.GetValueOrDefault();
                //linel.lotfull8 = lev1[7].lotfull.GetValueOrDefault();
                //linel.lotfull9 = lev1[8].lotfull.GetValueOrDefault();
                //linel.lotfull10 = lev1[9].lotfull.GetValueOrDefault();
                //linel.lotfull11 = lev1[10].lotfull.GetValueOrDefault();
                //linel.lotfull12 = lev1[11].lotfull.GetValueOrDefault();
                //linel.lotfull13 = lev1[12].lotfull.GetValueOrDefault();
                //linel.lotfull14 = lev1[13].lotfull.GetValueOrDefault();
                //linel.lotfull15 = lev1[14].lotfull.GetValueOrDefault();
                //linel.lotfull16 = lev1[15].lotfull.GetValueOrDefault();
                //linel.lotfull17 = lev1[16].lotfull.GetValueOrDefault();
                //linel.lotfull18 = lev1[17].lotfull.GetValueOrDefault();
                //linel.lotfull19 = lev1[18].lotfull.GetValueOrDefault();
                //linel.lotfull20 = lev1[19].lotfull.GetValueOrDefault();
                //linel.lotfull21 = lev1[20].lotfull.GetValueOrDefault();
                //linel.lotfull22 = lev1[21].lotfull.GetValueOrDefault();
                //linel.lotfull23 = lev1[22].lotfull.GetValueOrDefault();
                //linel.lotfull24 = lev1[23].lotfull.GetValueOrDefault();
                //linel.lotfull25 = lev1[24].lotfull.GetValueOrDefault();
                //linel.lotfull26 = lev1[25].lotfull.GetValueOrDefault();
                //linel.lotfull27 = lev1[26].lotfull.GetValueOrDefault();
                //linel.lotfull28 = lev1[27].lotfull.GetValueOrDefault();

                //try
                //{
                //    linel.lotfull29 = lev1[28].lotfull.GetValueOrDefault();
                //}
                //catch { linel.lotfull29 = -1; }
                //try
                //{
                //    linel.lotfull30 = lev1[29].lotfull.GetValueOrDefault();
                //}
                //catch { linel.lotfull30 = -1; }
                //try
                //{
                //    linel.lotfull31 = lev1[30].lotfull.GetValueOrDefault();
                //}
                //catch { linel.lotfull31 = -1; }
                ll.Add(linel);


            }

            return new ObjectResult(ll.AsEnumerable());
           
        }


    }
}