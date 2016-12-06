using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryBL
/// </summary>
public class CategoryBL
{
    public CategoryBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public List<category> GetByType(int type)
    {
        List<category> result = new List<category>();
        using (DataClassesDataContext db = new DataClassesDataContext())
        {
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
            db.Log = Console.Out;
            result = (from cate in db.categories
                      where cate.type == type
                      select cate).ToList();


        }

        return result;
    }

}