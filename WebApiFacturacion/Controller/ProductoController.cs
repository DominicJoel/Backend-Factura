using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFacturacion.EntityConections;
using WebApiFacturacion.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using System.Web;

namespace WebApiFacturacionController
{
    [Produces("application/json")]

    public class ProductoController : Controller
    {
       // private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment _appEnvironment;

        public ProductoController(IHostingEnvironment appEnvironment)

        {
            //----< Init: Controller >----

            _appEnvironment = appEnvironment;

            //----</ Init: Controller >----

        }


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

        // GET: api/Producto
        [HttpGet]
        [Route("api/Productos/activos")]
        public IActionResult GetProductosActivos()
        {
            IList<Productos> productos = new List<Productos>();
            productos = db.ObtenerProductosActivo();
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
            Productos produc;
            using (var context = new FacturaContext())
            {
                try
                {
                    produc = context.Productos.FromSql($" exec Insertar_Productos  {value.IdProductos}, {value.IdFabricante},{value.Nombre},{value.Precio},{value.Descripcion},{value.Estado},{value.Stock},{value.Modelo},{value.PhotoUrl} ").FirstOrDefault();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return CreatedAtAction("Get", produc);
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


        [HttpPost] //Postback
        [Route("api/Productos/img")]
        public IActionResult Upload_User_Profil_Image()

        {

            //--------< Upload_ImageFile() >--------

            string sResult = "";
            var files = HttpContext.Request.Form.Files["PhotoUrl"];
            var IdProducto = HttpContext.Request.Form["ProductoId"];

            //< check >

            if (files == null || files.Length == 0)

            {
                return BadRequest("Archivo no seleccionado");
            }


            if (files.ContentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) < 0)

            {

                return BadRequest("This file is not an image");

            }

            //</ check >


            //< init >

            string sImage_Folder = "Producto_Images";

            string sTarget_Filename = "Producto_Images" + IdProducto + ".jpg";

            //</ init >

            //< get Path >

            //string sPath_of_Target_Folder = sPath_WebRoot + "\\User_Files\\" + sImage_Folder + "\\";
            string sPath_of_Target_Folder = @"C:\Users\d.minaya\Desktop\Programacion3\WebApiFacturacion\WebApiFacturacion\wwwroot\fotos";//Para indicar la ruta donde lo vamos a mandar

            string sFile_Target_Original = sPath_of_Target_Folder + "\\Original\\" + sTarget_Filename;

            //</ get Path >

            //< Copy File to Target >
            try {

                using (var stream = new FileStream(sFile_Target_Original, FileMode.Create))//Aqui mandamos la carpeta al folder
                {

                    files.CopyToAsync(stream);

                }

            } catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok("Se creo");

            //</ Copy File to Target >

            //--------</ Upload_ImageFile() >--------

        }


    }
}
