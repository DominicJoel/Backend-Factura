using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFacturacion.Models
{
    public class FacturasProcesadas
    {
        public int Id_Factura { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Itebis { get; set; }
        public decimal PrecioTotal { get; set; }


    }
}
