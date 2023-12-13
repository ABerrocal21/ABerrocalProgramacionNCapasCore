using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Producto
    {
        public Producto()
        {
            VentaProductos = new HashSet<VentaProducto>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioUnitorio { get; set; }
        public int Stock { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdDepartamento { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }

        public virtual ICollection<VentaProducto> VentaProductos { get; set; }
    }
}
