using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timeSheetBackEnd.Models;

namespace timeSheetBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeimausController : ControllerBase
    {
        // GET: api/Leimaus
        [HttpGet]
        [Route("")]
        public List<TuntiRaportti> GetAllTunnit()
        {
            TuntiLeimausDBContext db = new TuntiLeimausDBContext();
            List<TuntiRaportti> tunnit = db.TuntiRaportti.ToList();
            return tunnit;
        }

        // GET: api/Leimaus/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Leimaus
        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] TuntiRaportti tunnit) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            TuntiLeimausDBContext db = new TuntiLeimausDBContext();
            try
            {

                _ = db.TuntiRaportti.Add(tunnit);
                _ = db.SaveChanges();

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

        // PUT: api/Leimaus/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
