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

namespace WebApiFacturacionController
{
    [Produces("application/json")]
 
    public class ContactosController : Controller
    {
        Contactos db = new Contactos();

        // GET: api/Clientes
        [HttpGet]
        [Route("api/Clientes")]
        public IActionResult Get()
        {
            IList<Clientes> clientes = new List<Clientes>();
            clientes = db.ObtenerClientes();
            return Ok(clientes);
        }


        // POST: api/Clientes
        [HttpPost]
        [DisableCors]
        [Route("api/Clientes")]
        public IActionResult Post([FromBody]Clientes value)
        {
           
        using (var context = new FacturaContext())
            {
                try
                {
                    context.Clientes.FromSql($" exec Crear_Clientes  {value.Id}, {value.CedulaClientes},{value.Nombre},{value.Apellido},{value.Correo},{value.Telefono},{value.Dirreccion},{value.Ciudad},{value.Pais},{value.NombreEmpresa},{value.SitioWebEmpresa},{value.TelefonoEmpresa}").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return CreatedAtAction("Get", value);
        }

        // DELETE: Clientes
        [HttpDelete("{id}")]
        [Route("api/Clientes")]
        public IActionResult Delete(int id)
        {
            using (var context = new FacturaContext())
            {
                try
                {
                    context.Clientes.FromSql($" exec Eliminar_Cliente {id}").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok(id);
        }


        // GET: api/Proveedores
        [HttpGet]
        [Route("api/Proveedor")]
        public IActionResult GetProveedor()
        {
            IList<Proveedor> proveedor = new List<Proveedor>();
            proveedor = db.ObtenerProveedor();
            return Ok(proveedor);
        }
       
        //Post: api/proveedores
        [HttpPost]
        [DisableCors]
        [Route("api/Proveedor")]
        public IActionResult PostProveedor([FromBody]Proveedor value)
        {
            using (var context = new FacturaContext())
            {
                try
                {
                    context.Proveedor.FromSql($" exec Insertar_Proveedor  {value.IdProveedor}, {value.CedulaProveedor},{value.Nombre},{value.Apellido},{value.Correo},{value.Telefono},{value.Dirreccion},{value.Ciudad},{value.Pais},{value.NombreEmpresa},{value.SitioWebEmpresa},{value.TelefonoEmpresa}").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            return CreatedAtAction("Get", value);
        }


        // DELETE: Proveedores
        [HttpDelete("{id}")]
        [Route("api/Proveedor")]
        public IActionResult DeleteProveedor(int id)
        {
            using (var context = new FacturaContext())
            {
                try
                {
                    context.Proveedor.FromSql($" exec Eliminar_Proveedor {id}").FirstOrDefault();
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
