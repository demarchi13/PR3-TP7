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
        GestionDataSource objDataSource = new GestionDataSource();
        protected void Page_Load(object sender, EventArgs e)
        {         
            //PUNTO
            if (!IsPostBack)
            {
                Session["ProvinciaID"] = null;
            }
            else if (Session["ProvinciaID"] != null)
            {
                string ID = Session["ProvinciaID"].ToString();               
                SqlDataSource1.SelectCommand = $"SELECT[Id_Sucursal], [NombreSucursal], [DescripcionSucursal], [URL_Imagen_Sucursal] FROM [Sucursal] WHERE [Id_ProvinciaSucursal] = {ID}";
            }         
        }

        protected void btnSeleccionar_Command(object sender, CommandEventArgs e)
        {
            //PUNTO 5
            string idSucursal;
            string nombre;
            string descripcion;

            if (Session["Tabla"] == null) //Crea la variable session, crea y guarda la tabla si no existe
            {
                Session["Tabla"] = objTabla.CrearTabla();
            }         

            //PUNTO 6
            //Verifica si el comando es "EventoSeleccionar" y guarda el id que contiene en el argumento     
            if (e.CommandName == "EventoSeleccionar")
            {
                idSucursal = e.CommandArgument.ToString();  //obtiene el id            

                foreach (ListViewItem item in lvSucursales.Items)  //Recorre los items del listview
                {
                    //Obtener los datos del boton "Seleccionar" dentro del listview
                    Button boton = (Button)item.FindControl("btnSeleccionar");

                    //Busca coincidencia con el valor del boton y el auxid
                    if (boton != null && boton.CommandArgument == idSucursal)
                    {                     
                        nombre = ((Label)item.FindControl("NombreSucursalLabel")).Text; //Guarda nombre y descripcion 
                        descripcion = ((Label)item.FindControl("DescripcionSucursalLabel")).Text;

                        //PUNTO 7
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
                        //PUNTO 8
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

        //PUNTO 9
        protected void btnProvincia_Command(object sender, CommandEventArgs e)
        {
            string idProvincia;
            if(e.CommandName == "e_FiltrarPorProvincia")
            {             
                idProvincia = e.CommandArgument.ToString(); //Obtiene el id de la provincia seleccionada
                Session["ProvinciaID"] = idProvincia;

                //Configura el comando SQL del SqlDataSource del listview para filtrar las sucursales por provincia
                objDataSource.FiltrarPorProvincia(SqlDataSource1, idProvincia);          
            }
        }

        //PUNTO 10
         protected void btnBuscar_Click(object sender, EventArgs e)
         {
            objDataSource.BuscarSucursal(txtBuscarSucursal, SqlDataSource1, lvSucursales);
         }
    }
}