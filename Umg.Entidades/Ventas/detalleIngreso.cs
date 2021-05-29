using System;
using System.Collections.Generic;
using System.Text;

namespace Umg.Entidades.Ventas
{
   public class detalleIngreso
    {
        public int idDetalleIngreso { get; set; }

        public int idIngreso { get; set; }

        public int idArticulo { get; set; }

        public int cantidadDetalleIngreso { get; set; }
        [required]

        public decimal precioDetalleIngreso { get; set; }
        [required]
    }
}
