using System;
using System.Collections.Generic;

namespace DL
{
    public partial class VwProductosGetAll
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public decimal PrecioUnitorio { get; set; }
        public int Stock { get; set; }
        public int? IdProveedor { get; set; }
        public string? NombreProveedor { get; set; }
        public int? IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public int? IdArea { get; set; }
        public string? NombreArea { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
    }
}
