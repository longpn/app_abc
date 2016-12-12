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

    public int InsertCategory(category cate)
    {

        using (DataClassesDataContext db = new DataClassesDataContext())
        {
            db.categories.InsertOnSubmit(cate);
            db.SubmitChanges();
            return cate.category_id;
        }
    }
    public category GetCategoryById(int id)
    {
        using (DataClassesDataContext db = new DataClassesDataContext())
        {
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
            db.Log = Console.Out;
            return (from x in db.categories
                    where x.category_id == id
                    select x).SingleOrDefault();
        }
    }


    public bool UpdateCategory(category cate)
    {
        try
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                //db.ObjectTrackingEnabled = false;
                db.DeferredLoadingEnabled = false;
                db.Log = Console.Out;
                category cateUpdate = (from x in db.categories
                                 where x.category_id == cate.category_id
                                 select x).SingleOrDefault();
                cateUpdate.category_name = cate.category_name;
                cateUpdate.category_description = cate.category_description;
                cateUpdate.type = cate.type;
                cateUpdate.sort_order = cate.sort_order;

                db.SubmitChanges();
                return true;
            }
        }
        catch (Exception e) { return false; }
    }

}