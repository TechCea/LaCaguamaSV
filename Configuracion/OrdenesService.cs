using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaCaguamaSV.Configuracion
{
    class OrdenesService
    {
        public static DataTable ListarOrdenes()
        {
            return OrdenesD.ObtenerOrdenes();
        }

    

    }
}
