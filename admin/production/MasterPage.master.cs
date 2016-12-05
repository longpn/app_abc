using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class production_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserBL user = new UserBL();
        if (!user.isLogin())
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
