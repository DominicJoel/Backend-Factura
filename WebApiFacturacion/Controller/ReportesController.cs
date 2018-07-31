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
    public class ReportesController : Controller
    {
        // GET: api/Reportes/Compras
        [HttpGet]
        [Route("api/Reportes/Compras")]
        public IActionResult Get()
        {
            IList<ReporteGastos> reporteGastos = new List<ReporteGastos>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    reporteGastos = context.LoadStoredProc("dbo.CapturaCompras")// Nombre del Procedimiento
                                          .ExecuteStoredProc<ReporteGastos>();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(reporteGastos);
            }
        }

        // GET: api/Reportes/Ventas
        [HttpGet]
        [Route("api/Reportes/Ventas")]
        public IActionResult GetVentas()
        {
            IList<ReporteIngresos> reporteIngresos = new List<ReporteIngresos>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    reporteIngresos = context.LoadStoredProc("dbo.CapturaVentas")// Nombre del Procedimiento
                                          .ExecuteStoredProc<ReporteIngresos>();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(reporteIngresos);
            }
        }

        // POST: api/Reportes/MesVentas
        [HttpPost]
        [Route("api/Reportes/MesVentas")]
        public IActionResult Post( int anio)
        {
            IList<ReporteMes> reporteMesVentas = new List<ReporteMes>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    reporteMesVentas = context.LoadStoredProc("dbo.CantidadVendidaPorMes")// Nombre del Procedimiento
                                              .WithSqlParam("@anio", anio)
                                              .ExecuteStoredProc<ReporteMes>();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(reporteMesVentas);
            }
        }

        // POST: api/Reportes/MesCompra
        [HttpPost]
        [Route("api/Reportes/MesCompra")]
        public IActionResult PostCompra(int anio)
        {
            IList<ReporteMes> reporteMesVentas = new List<ReporteMes>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    reporteMesVentas = context.LoadStoredProc("dbo.CantidadCompradaPorMes")// Nombre del Procedimiento
                                              .WithSqlParam("@anio", anio)
                                              .ExecuteStoredProc<ReporteMes>();
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }

                return Ok(reporteMesVentas);
            }
        }

        // Get: api/Reportes/cantidadClientes
        [HttpGet]
        [Route("api/Reportes/cantidadClientes")]
        public IActionResult CantidadCliente()
        {
            IList<CantidadClientes> cantidad = new List<CantidadClientes>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    cantidad = context.LoadStoredProc("dbo.cantidadClientes")// Nombre del Procedimiento
                                                   .ExecuteStoredProc<CantidadClientes>();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok(cantidad);
        }

        /// Get: api/Reportes/cantidadProductos
        [HttpGet]
        [Route("api/Reportes/cantidadProductos")]
        public IActionResult CantidadProducto()
        {
            IList<CantidadClientes> cantidad = new List<CantidadClientes>();//Facturas Procesadas es Un modelo que se tuvo que crear para obtener los datos de el procedimiento con relacion
            using (var context = new FacturaContext())
            {
                try
                {
                    cantidad = context.LoadStoredProc("dbo.cantidadProductos")// Nombre del Procedimiento
                                                   .ExecuteStoredProc<CantidadClientes>();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return Ok(cantidad);
        }
    }
}
