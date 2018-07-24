using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Probar_Entity.EntityConnections;
using WebApiFacturacion.Models;

namespace WebApiFacturacionController
{
    [Produces("application/json")]
    public class ComprasController : Controller
    {
        // GET: api/Compras
        //generar codigo de Compra
        [HttpGet]
        [Route("api/Compra")]
        public IActionResult Get()
        {
            using (var context = new FacturaContext())
            {
                CodigoCompras codigo;
                try
                {
                    codigo = context.CodigoCompras.FromSql($"exec Crear_Code").FirstOrDefault();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(codigo);
            }
        }


        // GET: api/Compra/listaCompra
        //Trae la lista de todas las compras
        [HttpGet]
        [Route("api/Compra/listaCompra")]
        public IActionResult GetListaFactura()
        {
            IList<ComprasProcesadas> listaCompra = new List<ComprasProcesadas>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    listaCompra = context.LoadStoredProc("dbo.ComprasProcesadas")// Nombre del Procedimiento
                                          .ExecuteStoredProc<ComprasProcesadas>();


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

                return Ok(listaCompra);
            }
        }


        //Obtiene todas las Compras relacionadas
        [HttpPost]
        [Route("api/Compra/ListaCompraId")]
        public IActionResult GetListaFacturaId([FromBody]CodigoCompras value)
        {
            using (var context = new FacturaContext())
            {
                IList<ComprasLista> Lista = new List<ComprasLista>();
                try
                {
                    Lista = context.ComprasLista.FromSql($"exec getCompras {value.IdCompra}").ToList();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(Lista);
            }
        }



        //Obtiene Una Compra Relacionada
        [HttpPost]
        [Route("api/Compra/CompraUnica")]
        public IActionResult GetFacturaInfo([FromBody]CodigoCompras value)
        {
            IList<ComprasProcesadas> listaCompras = new List<ComprasProcesadas>();//Compras Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    listaCompras = context.LoadStoredProc("dbo.ComprasProcesadasUnica")// Nombre del Procedimiento
                                          .WithSqlParam("@idCompras", value.IdCompra)
                                          .ExecuteStoredProc<ComprasProcesadas>();

                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(listaCompras);
            }
        }



        // POST: api/Compra
        //Insertar una nueva Compra
        [HttpPost]
        [Route("api/Compra")]
        public IActionResult Post([FromBody]ComprasLista value)
        {
            using (var context = new FacturaContext())
            {
                ComprasLista compraNueva;
                try
                {
                    compraNueva = context.ComprasLista.FromSql($"exec insertar_Compras {value.IdCompra},{value.IdProveedor},{value.Neto},{value.Iva},{value.PrecioTotal},{value.Cantidad},{value.IdProductos} ").FirstOrDefault();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return CreatedAtAction("Get", compraNueva);
            }
        }
    }
}
