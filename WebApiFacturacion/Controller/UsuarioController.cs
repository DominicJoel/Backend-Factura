using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFacturacion.EntityConections;
using WebApiFacturacion.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFacturacionController
{
    [Produces("application/json")]
 
    public class ValuesController : Controller
    {
        Usuarios db = new Usuarios();

       // GET: api/<controller>
        [HttpGet]
        [Route("api/getUsers")]
        public IActionResult Get()
        {
            IList<Usuario> user = new List<Usuario>();

            user = db.ObtenerUsuario();

            return Ok(user);
        }

        // Post api/validar
        [HttpPost]
        [Route("api/validar")]
        public IActionResult Get([FromBody] Usuario value)
        {
            var existe = db.UsuarioExiste(value);

            if (existe == null)
            {
                return BadRequest("Este Usuario no tiene acceso");
            }

            return Ok ( Json( existe ));
        }


        // POST api/<controller>
        [HttpPost]
        [DisableCors]
        [Route("api/[controller]")]
        public IActionResult Post([FromBody]Usuario value)
        {


            using (var context = new FacturaContext())
            {
                try
                {
                    context.Usuario.FromSql($" exec Insertar_Usuario {value.IdUsuario},{value.Nombre},{value.Correo},{value.Pass},{value.IdRole}").FirstOrDefault();
   
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return CreatedAtAction("Get", value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}
