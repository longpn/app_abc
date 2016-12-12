using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Ajax_UploadImages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string path = "";
            string fileNames = "";
            if (Request.QueryString["id"].Equals("product"))
            {
                path = "~/Uploads/Products/Temp";
            }
            else if (Request.QueryString["id"].Equals("Dish"))
            {
                path = "~/Uploads/Dish/Temp";
            }
            else if (Request.QueryString["id"].Equals("Sets"))
            {
                path = "~/Uploads/Sets/Temp";
            }
            else if (Request.QueryString["id"].Equals("Category"))
            {
                path = "~/Uploads/Category/Temp";
            }
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    // get a stream
                    var stream = fileContent.InputStream;
                    // and optionally write the file to disk
                    var fileName = Path.GetFileName(file);
                    fileNames = fileNames + fileName + ",";
                    var pathFile = Path.Combine(Server.MapPath(path), fileName);
                    using (var fileStream = File.Create(pathFile))
                    {

                        stream.CopyTo(fileStream);
                        //File.Move(pathFile, Server.MapPath("~/Uploads/Jobs/"));
                    }

                }
            }
            Response.Write(fileNames.Trim(','));

        }
        catch (Exception ex)
        {
            Response.Write("");

        }
        Response.End();
    }
}