using System;
using System.Collections.Generic;

namespace WebApiFacturacion.Models
{
    public partial class ComprasLista
    {
       

        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public int IdProductos { get; set; }
        public decimal Neto { get; set; }
        public decimal Iva { get; set; }
        public decimal PrecioTotal { get; set; }
        public int Numero { get; set; }
        public int Cantidad { get; set; }


        public CodigoCompras IdCompraNavigation { get; set; }
        public Proveedor IdProveedorNavigation { get; set; }
        public Productos IdProductoNavigation { get; set; }
    }
}
