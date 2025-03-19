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

        public static void CrearOrden(string cliente, decimal total, decimal descuento, int mesa, int pago, int usuario)
        {
            if (total < 0)
                throw new Exception("El total no puede ser negativo.");

            OrdenesD.AgregarOrden(cliente, total, descuento, mesa, pago, usuario);
        }

        public static void EditarOrden(int id, decimal total, decimal descuento, int mesa, int pago)
        {
            OrdenesD.ActualizarOrden(id, total, descuento, mesa, pago);
        }

        public static void BorrarOrden(int id)
        {
            OrdenesD.EliminarOrden(id);
        }
    }
}
