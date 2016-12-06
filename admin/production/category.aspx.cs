using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class production_category : System.Web.UI.Page
{
    protected List<category> cate = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        int type = int.Parse(Request.QueryString["type"]);
        CategoryBL cateBL = new CategoryBL();
        cate = cateBL.GetByType(type);

    }
}