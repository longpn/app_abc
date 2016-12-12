using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductBL
/// </summary>
public class ProductBL
{
	public ProductBL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<product> GetAll()
    {
        List<product> result = new List<product>();
        using (DataClassesDataContext db = new DataClassesDataContext())
        {
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
            db.Log = Console.Out;
            result = (from cate in db.products                     
                      select cate).ToList();


        }

        return result;
    }

}