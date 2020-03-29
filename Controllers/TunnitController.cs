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
    public class TunnitController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public List<Tunnit> GetAllLogins()
        {
            TuntiLeimausDBContext db = new TuntiLeimausDBContext();
            List<Tunnit> tunnit = db.Tunnit.ToList();
            return tunnit;
        }
        // POST: api/Leimaus
        [HttpPost] //<--filtteri rajoittaa alapuolella olevan metodin vain POST-pyyntöihin (uuden asian luominen tai lisääminen)
        [Route("")] //<--tyhjä reitinmääritys (ei ole pakko laittaa), eli ei mitään lisättävää reittiin, jolloin
        public ActionResult PostCreateNew([FromBody] Tunnit tunti) //<-- [FromBody] tarkoittaa, että HTTP-pyynnön Body:ssä välitetään JSON-muodossa oleva objekti ,joka om Documentation-tyyppinen customer-niminen
        {
            TuntiLeimausDBContext db = new TuntiLeimausDBContext();
            try
            {


                _ = db.Tunnit.Add(tunti);
                _ = db.SaveChanges();

                return Ok(tunti.TunnitId);
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
    }
}