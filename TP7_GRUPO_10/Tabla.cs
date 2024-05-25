using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace TP7_GRUPO_10
{
    public class Tabla
    {
        public DataTable CrearTabla()
        {
            DataTable tabla = new DataTable();

            DataColumn columna = new DataColumn("IdSucursal", System.Type.GetType("System.String"));
            tabla.Columns.Add(columna);

            columna = new DataColumn("Nombre", System.Type.GetType("System.String"));
            tabla.Columns.Add(columna);

            columna = new DataColumn("Descripcion", System.Type.GetType("System.String"));
            tabla.Columns.Add(columna);

            return tabla;
        }

        public void AgregarFila(DataTable tabla, string IdSucursal, string nombreSucursal, string descripcion)
        {
            DataRow fila = tabla.NewRow();
            fila["IdSucursal"] = IdSucursal;
            fila["Nombre"] = nombreSucursal;
            fila["Descripcion"] = descripcion;     

            tabla.Rows.Add(fila);
        }
    }
}