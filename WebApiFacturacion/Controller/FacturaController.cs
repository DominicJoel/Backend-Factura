using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Probar_Entity.EntityConnections;
using WebApiFacturacion.Models;

namespace WebApiFacturacionController
{
    [Produces("application/json")]
    public class FacturaController : Controller
    {
        // GET: api/Factura
        //generar codigo de la Factura
        [HttpGet]
        [Route("api/Factura")]
        public IActionResult Get()
        {
            using (var context = new FacturaContext())
            {
                CodigoFactura codigo;
                try
                {   
                  codigo = context.CodigoFactura.FromSql($"exec genera_Codigo").FirstOrDefault();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(codigo);
            }
        }


        // GET: api/Factura
        //Trae la lista de todas las facturas
        [HttpGet]
        [Route("api/Factura/listaFactura")]
        public IActionResult GetListaFactura()
        {
            IList<FacturasProcesadas> listaFactura = new List<FacturasProcesadas>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    listaFactura = context.LoadStoredProc("dbo.FacturasProcesadas")// Nombre del Procedimiento
                                          .ExecuteStoredProc<FacturasProcesadas>();


                    // Enviar mensaje de correo electronico

                 //   var message = new MimeMessage();
                 //   message.From.Add(new MailboxAddress("Compra", "domiamazing@gmail.com"));//Aqui va lo que va a aparecer de donde viene
                 //   message.To.Add(new MailboxAddress("Hola", "dominicminaya03@gmail.com"));//Aqui va lo que va a aparecer a donde va
                 //   message.Subject = " Prueba de los mensajes ";
                 //   message.Body = new TextPart("Helloo")
                 ////   message.Attachments();//Tenerlo en OJO por si vamos a enviar directamente el archivo de la factura
                 //   {
                 //       Text = "Aqui estamos comodo" //Lo que va en el cuerpo
                 //   };

                 //   using (var client = new SmtpClient())
                 //   {
                 //       client.Connect("smtp.gmail.com", 587, true);//El puerto de gmail
                 //       client.Authenticate("domiamazing@gmail.com", "Dominic17121996");//Las credenciales de correo que estamos usando
                 //       client.Send(message);//Que lo envie 
                 //       client.Disconnect(true); // //Que se desconecte
                 //   }

                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(listaFactura);
            }
        }


        //Obtiene todas las facturas relacionadas
        [HttpPost]
        [Route("api/Factura/ListaFacturaId")]
        public IActionResult GetListaFacturaId([FromBody]CodigoFactura value)
        { 
            using (var context = new FacturaContext())
            {
                IList<FacturaLista> Lista = new List<FacturaLista>();
                try
                {
                    Lista = context.FacturaLista.FromSql($"exec getFacturas {value.IdFactura}").ToList();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(Lista);
            }
        }



        //Obtiene Una Factura Relacionada
        [HttpPost]
        [Route("api/Factura/FacturaUnica")]
        public IActionResult GetFacturaInfo([FromBody]CodigoFactura value)
        {
            IList<FacturasProcesadas> listaFactura = new List<FacturasProcesadas>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    listaFactura = context.LoadStoredProc("dbo.FacturasProcesadasUnica")// Nombre del Procedimiento
                                          .WithSqlParam("@idFactura",value.IdFactura)
                                          .ExecuteStoredProc<FacturasProcesadas>();

                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(listaFactura);
            }
        }



        // POST: api/Factura
        //Insertar una nueva Factura
        [HttpPost]
        [Route("api/Factura")]
        public IActionResult Post([FromBody]FacturaLista value)
        {
            using (var context = new FacturaContext())
            {
                FacturaLista facturaNueva;
                try
                {
                    facturaNueva = context.FacturaLista.FromSql($"exec InsertarFacturas {value.IdFactura},{value.IdCliente},{value.IdProductos},{value.Cantidad},{value.Vendedor} ").FirstOrDefault();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return CreatedAtAction("Get",facturaNueva);
            }
        }
        

    }
}
