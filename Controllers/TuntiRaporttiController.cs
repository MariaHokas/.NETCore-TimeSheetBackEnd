using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using timeSheetBackEnd.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace timeSheetBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuntiRaporttiController : Controller
    {
        // GET: raportti/<controller>
        [HttpGet]
        [Route("")]
        public List<TuntiRaportti> GetAllTunnit()
        {
            TuntiLeimausDBContext db = new TuntiLeimausDBContext();
            List<TuntiRaportti> tunnit = db.TuntiRaportti.ToList();
            return tunnit;
        }

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] TuntiRaportti tunnit) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            TuntiLeimausDBContext db = new TuntiLeimausDBContext();
            try
            {
                db.TuntiRaportti.Add(tunnit);
                db.SaveChanges();

                return Ok(tunnit.Idleimaus);
            }

            catch (Exception)
            {
                return BadRequest("Jokin meni pieleen leimausta lisättäessä!?!?");
            }

            finally
            {
                db.Dispose();
            }

            /*return doku.DocumentationId.ToString; //k*//*uittaus Frontille, että päivitys meni oikein --> Frontti voi tsekata, että kontrolleri palauttaa saman id:n mitä käsitteli*/
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        //[HttpGet]
        //[Route("R")]
        //public List<TuntiRaportti> GetAllTunnit2()
        //{
        //    TuntiLeimausDBContext db = new TuntiLeimausDBContext();
        //    List<TuntiRaportti> tunnit = db.TuntiRaportti.ToList();
        //    return tunnit;
        //}
    }
}
