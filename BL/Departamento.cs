using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetbyArea(int IdArea)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from departamentoLINQ in context.Departamentos
                                 where departamentoLINQ.IdArea == IdArea
                                 select new
                                 {
                                     IdDepartamento = departamentoLINQ.IdDepartamento,
                                     Nombre = departamentoLINQ.Nombre

                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = item.IdDepartamento;
                            departamento.Nombre = item.Nombre;

                            result.Objects.Add(departamento);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.Ex = e;
                result.Message = "No se encontraron registros. " + result.Ex;
            }

            return result;

        }
    }
}
