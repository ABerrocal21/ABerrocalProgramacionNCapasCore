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

        [HttpGet]
        public IActionResult Form(int? idDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();

            departamento.Area = new ML.Area();
            ML.Result resultArea = BL.Area.GetAll();
            departamento.Area.Areas = resultArea.Objects;

            if (idDepartamento == null)
            {
                ViewBag.Action = "Agregar";
            }
            else
            {
                departamento.IdDepartamento = idDepartamento.Value;
                ML.Result result = BL.Departamento.GetById(departamento);

                if (result.Correct)
                {
                    departamento = (ML.Departamento)result.Object;

                    resultArea = BL.Area.GetAll();

                    departamento = (ML.Departamento)result.Object;

                    departamento.Area.Areas = resultArea.Objects;

                }

                ViewBag.Action = "Actualizar";
            }

            return View(departamento);

        }

        [HttpPost]
        public IActionResult Form(ML.Departamento departamento)
        {         

            if (departamento.IdDepartamento == 0)
            {
                ML.Result result = BL.Departamento.Add(departamento);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha ingresado correctamente el Departamento" + result.Message;
                    return PartialView("Modal");
                }
            }
            else
            {
                
                ML.Result result = BL.Departamento.Update(departamento);

                if (result.Correct)
                {

                    ViewBag.Message = result.Message;
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha ingresado correctamente el Departamento" + result.Message;
                    return PartialView("Modal");
                }

            }

            return View(departamento);

        }
    }
}
