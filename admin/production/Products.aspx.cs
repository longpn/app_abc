using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class production_Products : System.Web.UI.Page
{
    protected List<product> product = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        ProductBL proBL = new ProductBL();


        product = proBL.GetAll();
        

    }
}