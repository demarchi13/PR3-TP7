using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace TP7_GRUPO_10
{
    public class GestionDataSource
    {
        public void BuscarSucursal(TextBox txtBuscarSucursal, SqlDataSource SqlDataSource1, ListView lvSucursales)
        {
            string nombreSucursal = txtBuscarSucursal.Text;

            if (nombreSucursal == "")
            {
                SqlDataSource1.SelectCommand = "SELECT [Id_Sucursal], [NombreSucursal], [DescripcionSucursal], [Id_ProvinciaSucursal], [URL_Imagen_Sucursal] FROM [Sucursal]";
                lvSucursales.DataSourceID = "SqlDataSource1";
                lvSucursales.DataBind();
            }
            else if (nombreSucursal != "")
            {
                SqlDataSource1.SelectCommand = "SELECT [Id_Sucursal], [NombreSucursal], [DescripcionSucursal], [Id_ProvinciaSucursal], [URL_Imagen_Sucursal] FROM [Sucursal] WHERE [NombreSucursal] LIKE '%' + @NombreSucursal+ '%' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("NombreSucursal", nombreSucursal);
                lvSucursales.DataSourceID = "SqlDataSource1";
                lvSucursales.DataBind();
            }
        }

        public void FiltrarPorProvincia(SqlDataSource SqlDataSource1, string idProvincia)
        {         
            //Configura el comando SQL del SqlDataSource del listview para filtrar las sucursales por provincia
            SqlDataSource1.SelectCommand = "select Id_Sucursal, NombreSucursal, DescripcionSucursal, Id_ProvinciaSucursal, URL_Imagen_Sucursal from Sucursal where Id_ProvinciaSucursal = @Id_Provincia";
            SqlDataSource1.SelectParameters.Clear(); // Limpia los parametros previamente definidos
            SqlDataSource1.SelectParameters.Add("Id_Provincia", idProvincia);                
    }
    }
}