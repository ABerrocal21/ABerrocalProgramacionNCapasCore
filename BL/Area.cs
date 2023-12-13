using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ABerrocalProgramacionNCapasCoreContext context = new DL.ABerrocalProgramacionNCapasCoreContext())
                {
                    var query = (from areaLINQ in context.Areas
                                 select new
                                 {
                                     IdArea = areaLINQ.IdArea,
                                     Nombre = areaLINQ.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = item.IdArea;
                            area.Nombre = item.Nombre;

                            result.Objects.Add(area);
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
