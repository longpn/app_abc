using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            NameValueCollection nvc = Request.Form;
            UserBL user = new UserBL();
            if (user.CheckLogin(nvc["email"], nvc["password"]))
            {
                Response.Redirect("~/production/Default.aspx");
            }
        }
    }
}