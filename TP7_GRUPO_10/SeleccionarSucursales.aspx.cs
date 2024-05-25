using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TP7_GRUPO_10
{
    public partial class SeleccionarSucursales : System.Web.UI.Page
    {
        Tabla objTabla = new Tabla();
        AccesoDatos ObjDatos = new AccesoDatos();

        protected void Page_Load(object sender, EventArgs e)
        {         
            if (!IsPostBack)
            {
                Session["ProvinciaID"] = null;
            }
            else if (Session["ProvinciaID"] != null)
            {
                string ID = Session["ProvinciaID"].ToString();
                SqlDataSource1.SelectCommand = $"SELECT[Id_Sucursal], [NombreSucursal], [DescripcionSucursal], [URL_Imagen_Sucursal] FROM [Sucursal] WHERE [Id_ProvinciaSucursal] = {ID}";
                label.Text = Session["ProvinciaID"].ToString();
            } 
            
        }

        protected void btnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            string idSucursal;
            string nombre;
            string descripcion;

            //Crea la variable session, crea y guarda la tabla si no existe
            if (Session["Tabla"] == null)
            {
                Session["Tabla"] = objTabla.CrearTabla();
            }         

            //Verifica si el comando es "EventoSeleccionar" y guarda el id que contiene en el argumento     
            if (e.CommandName == "EventoSeleccionar")
            {
                //obtiene el id
                idSucursal = e.CommandArgument.ToString();              

                //Recorre los items del listview
                foreach (ListViewItem item in lvSucursales.Items)
                {
                    //Obtener los datos del boton "Seleccionar" dentro del listview
                    Button boton = (Button)item.FindControl("btnSeleccionar");

                    //Busca coincidencia con el valor del boton y el auxid
                    if (boton != null && boton.CommandArgument == idSucursal)
                    {
                        //Guarda nombre y descripcion                      
                        nombre = ((Label)item.FindControl("NombreSucursalLabel")).Text;
                        descripcion = ((Label)item.FindControl("DescripcionSucursalLabel")).Text;

                        //Verificia si la sucursal ya esta en la tabla
                        bool flag_SucursalRepetida = false;
                        DataTable auxTabla = (DataTable)Session["Tabla"];
                        foreach (DataRow fila in auxTabla.Rows)
                        {
                            if(fila["IdSucursal"].ToString() == idSucursal)
                            {
                                flag_SucursalRepetida = true;
                                break;
                            }
                        }
                        //Agrega la fila a la tabla                  
                        if (!flag_SucursalRepetida)
                        {
                            objTabla.AgregarFila((DataTable)Session["Tabla"], idSucursal, nombre, descripcion);
                            lblMensaje.Text = "Sucursal agregada: " + idSucursal + " - Nombre: " + nombre + " Descripción: " + descripcion;
                        }
                        else
                        {
                            lblMensaje.Text = "La sucursal de " + nombre + " ya se encuentra agregada.";
                        }
                    }                                                      
                }            
            }
        }

        protected void btnProvincia_Command(object sender, CommandEventArgs e)
        {
            string idProvincia;
            if(e.CommandName == "e_FiltrarPorProvincia")
            {             
                //Obtiene el id de la provincia seleccionada
                idProvincia = e.CommandArgument.ToString();
                Session["ProvinciaID"] = idProvincia;

                //Configura el comando SQL del SqlDataSource del listview para filtrar las sucursales por provincia
                SqlDataSource1.SelectCommand = "select Id_Sucursal, NombreSucursal, DescripcionSucursal, Id_ProvinciaSucursal, URL_Imagen_Sucursal from Sucursal where Id_ProvinciaSucursal = @Id_Provincia";              
                SqlDataSource1.SelectParameters.Clear(); // Limpia los parametros previamente definidos
                SqlDataSource1.SelectParameters.Add("Id_Provincia", idProvincia);             
            }
        }

         protected void btnBuscar_Click(object sender, EventArgs e)
         {
            string nombreSucursal = txtBuscarSucursal.Text;

            if(nombreSucursal == "")
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
    }
}