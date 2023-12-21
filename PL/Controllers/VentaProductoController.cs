using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        public IActionResult Index()
        {
            return View("Detalle");
        }

        public IActionResult GetById(List<object> Venta)
        {
            //ML.Result result = BL.VentaProducto.GetbyIdVenta(Venta.IdVenta);
            ML.VentaProducto ventaProducto = new ML.VentaProducto();

            //if (result.Correct)
            //{
            //    ventaProducto.VentaProductos = result.Objects;
            //}
            //else
            //{
            //    ViewBag.Message = result.Message;
            //}

            ventaProducto.VentaProductos = Venta;
            return View("Detalle", ventaProducto);
        }
    }
}
