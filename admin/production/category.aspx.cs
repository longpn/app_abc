using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class production_category : System.Web.UI.Page
{
    protected List<category> cate = null;
    protected int type = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        CategoryBL cateBL = new CategoryBL();
      
        if (!IsPostBack && Request.HttpMethod=="POST" )
        {
            category cate = new category();
            cate.category_name = Request.Params["name"];
            cate.category_description = Request.Params["descr"];
            cate.category_id = int.Parse(Request.Params["cate_id"]);
            cate.sort_order = int.Parse(Request.Params["sort"]);
            cate.type = int.Parse(Request.Params["type"]);
            cate.created_date = DateTime.Now;
            if (cate.category_id > 0)
            {
                cateBL.UpdateCategory(cate);
            }
            else
            {
                cateBL.InsertCategory(cate);
            }
            Response.Redirect("/production/category.aspx?type=" + cate.type);
        }
        else
        {
            type = int.Parse(Request.QueryString["type"]);
            cate = cateBL.GetByType(type);
        }

    }
}