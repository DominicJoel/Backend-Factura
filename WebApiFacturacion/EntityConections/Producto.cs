using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFacturacion.Models;

namespace WebApiFacturacion.EntityConections
{
    public class Producto
    {
        public IList<Productos> ObtenerProducto()
        {
            IList<Productos> producto;
            using (var context = new FacturaContext())
            {
                producto = context.Productos.ToList();
                return producto;
            }
        }

        public IList<Productos> ObtenerProductosActivo()
        {
            IList<Productos> producto;
            using (var context = new FacturaContext())
            {
                string estado = "a";
                producto = context.Productos.Where( c => c.Estado == estado).ToList();
                return producto;
            }
        }

        public Productos ObtenerUnProducto(int id)
        {
            Productos producto;
             using (var context = new FacturaContext())
            {
                   producto = context.Productos
                     .Where(x => x.IdProductos == id).FirstOrDefault();
            }
           return producto;
        }

    }
}
