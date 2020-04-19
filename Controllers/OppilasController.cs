using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using timeTrackingSystemBackend.Entities;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using timeTrackingSystemBackend.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace timeTrackingSystemBackend.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]

    public class OppilasController : ControllerBase

    {
        //public override Task<User> GetUserAsync(ClaimsPrincipal principal)
        //{
        //    var userId = GetUserId(principal);
        //    return FindByNameAsync(userId);
        //}

        [HttpGet]
        [Route("")]
        public List<Tunnit> GetAllLogins()
        {
            Entities.WebApiDatabaseContext db = new Entities.WebApiDatabaseContext();
            List<Tunnit> tunnit = db.Tunnit.ToList();
            return tunnit;
        }

        // POST: api/Leimaus
        [HttpPost]
        [Route("sisaan")]
        public async Task<ActionResult> PostSisaanAsync([FromBody] Tunnit tunti)
            
                
        {
            Entities.WebApiDatabaseContext db = new Entities.WebApiDatabaseContext();
        
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<string>();
            try
            {

                Tunnit dbItem = new Tunnit()

                {
                    TunnitId = tunti.TunnitId,
                    LuokkahuoneId = tunti.LuokkahuoneId,
                    //OppilasId = userId,
                    //UserId = await UserManager.GetUserAsync(User),
                    Sisaan = DateTime.Now,
                };

                _ = db.Tunnit.Add(dbItem);
                _ = db.SaveChanges();

                return Ok(dbItem.TunnitId);
            }

            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen sisäänleimausta lisättäessä!?!?");
            }

            finally
            {

                db.Dispose();
            }

            /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        }
        // POST: api/Leimaus
        [HttpPost]
        [Route("ulos")]
        public ActionResult PostUlos([FromBody] Tunnit tunti)
        {
            Entities.WebApiDatabaseContext db = new Entities.WebApiDatabaseContext();
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {

                Tunnit dbItem = (from p in db.Tunnit
                                 where p.OppilasId == tunti.OppilasId && p.LuokkahuoneId == tunti.LuokkahuoneId
                                 orderby p.TunnitId descending
                                 select p).First();
                {
                    dbItem.Ulos = DateTime.Now;
                    _ = db.SaveChanges();

                    return Ok(dbItem.TunnitId);
                }
            }

            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen ulosleimausta lisättäessä!?!?");
            }

            finally
            {

                db.Dispose();
            }

            /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        }

        [HttpGet]
        [Route("r")]

        public IActionResult GetSomeTunnit(int offset, int limit, int oppilasid)
        {
            //if (oppilasid != null)
            //{
            //    WebApiDatabaseContext db = new WebApiDatabaseContext();
            //    List<Tunnit> leimaukset = db.Tunnit.Where(d => d.OppilasId == oppilasid).ToList();
            //    return Ok(leimaukset);
            //}

            //else

            {
                Entities.WebApiDatabaseContext db = new Entities.WebApiDatabaseContext();
                var model = (from c in db.Tunnit
                             orderby c.TunnitId descending
                             select new
                             {
                                 c.TunnitId,
                                 c.LuokkahuoneId,
                                 c.Sisaan,
                                 c.Ulos,
                                 //c.UserId,
                                 c.OppilasId
                             }).Skip(offset).Take(limit).ToList();

                return Ok(model);
            }

        }

    }
}