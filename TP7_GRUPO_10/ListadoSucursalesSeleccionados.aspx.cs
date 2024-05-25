using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TP7_GRUPO_10
{
    public partial class ListadoSucursalesSeleccionados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session ["Tabla"] != null)
            {
                grdSucursalesSeleccionadas.DataSource = (DataTable)Session["Tabla"];
                grdSucursalesSeleccionadas.DataBind();
            }
        }
    }
}