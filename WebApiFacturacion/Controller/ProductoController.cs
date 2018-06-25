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

    public class ProductoController : Controller
    {
        Producto db = new Producto();

        // GET: api/Producto
        [HttpGet]
        [Route("api/Productos")]
        public IActionResult Get()
        {
            IList<Productos> productos = new List<Productos>();
            productos = db.ObtenerProducto();
            return Ok(productos);

        }

        // GET: api/Producto/5
        [HttpGet("{id}", Name = "GetU")]
        [Route("api/Productos/{id}")]//Le indicamos que recibira un parametro para la busqueda
        public IActionResult Get(int id)
        {

            Productos producto = db.ObtenerUnProducto(id);
            return Ok(producto);
        }

        // POST: api/Producto
        [HttpPost]
        [Route("api/Productos")]
        public IActionResult Post([FromBody]Productos value)
        {

            using (var context = new FacturaContext())
            {
                try
                {
                    context.Productos.FromSql($" exec Insertar_Productos  {value.IdProductos}, {value.IdFabricante},{value.Nombre},{value.Precio},{value.Descripcion},{value.Estado},{value.Stock},{value.Modelo} ").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return CreatedAtAction("Get", value);
        }

        // POST: api/Productos
        [HttpPost]
        [Route("api/Productos/Activar")]
        public IActionResult Delete([FromBody]Productos value)
        {
            using (var context = new FacturaContext())
            {
                try
                {
                    context.Productos.FromSql($" exec Eliminar_Productos {value.IdProductos}, {value.Estado} ").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return CreatedAtAction("Get", value);
        }
    }
}
