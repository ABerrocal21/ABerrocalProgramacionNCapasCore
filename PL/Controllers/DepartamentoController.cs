using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            ML.Result result = BL.Departamento.GetAll();

            
            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
                return View(departamento);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.Message;
                return View(departamento);
            }
        }
    }
}
