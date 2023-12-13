﻿namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public ML.Proveedor? Proveedor { get; set; }
        public ML.Departamento? Departamento { get; set; }
        public string? Descripcion { get; set; }
        public byte[]? Imagen { get; set; }
        public List<object>? Productos { get; set; }
    }
}