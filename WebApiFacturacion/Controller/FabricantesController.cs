using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFacturacion.EntityConections;
using WebApiFacturacion.Models;

namespace WebApiFacturacionController
{
    [Produces("application/json")]
    public class FabricantesController : Controller
    {
        Fabricante db = new Fabricante();

        // GET: api/Fabricantes
        [HttpGet]
        [Route("api/Fabricantes")]
        public IActionResult Get()
        {
            IList<Fabricantes> fabricante = new List<Fabricantes>();
            fabricante = db.ObtenerFabricantes();
            return Ok(fabricante);
        }

        
        // POST: api/Fabricantes
        [HttpPost]
        [Route("api/Fabricantes")]
        public IActionResult Post([FromBody]Fabricantes value)
        {
            using (var context = new FacturaContext())
            {
                try
                {
                    context.Fabricantes.FromSql($" exec Insertar_Fabricantes   {value.IdFabricantes}, {value.Nombre},{value.Estado}").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return CreatedAtAction("Get", value);
        }
    


    // DELETE: api/Fabricantes/5
        [HttpDelete("{id}")]
        [Route("api/Fabricantes")]
        public IActionResult Delete(int id)
        {
            using (var context = new FacturaContext())
            {
                try
                {
                    context.Fabricantes.FromSql($" exec Eliminar_Fabricante {id}").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok(id);

        }
    }
}
