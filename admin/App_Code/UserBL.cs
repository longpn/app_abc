using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserBL
/// </summary>
public class UserBL
{
    public UserBL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool CheckLogin(string email, string password)
    {
        bool result = false;
        using (DataClassesDataContext db = new DataClassesDataContext())
        {
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
            db.Log = Console.Out;
            var queryUsers = (from usr in db.users
                              where usr.username == email && usr.password == password
                              select usr).SingleOrDefault();

            if (queryUsers != null && queryUsers.user_id > 0)
            {
                HttpContext.Current.Session["appdicho_username"] = queryUsers.fullname;
                HttpContext.Current.Session["appdicho_userid"] = queryUsers.user_id;
                result = true;
            }
        }

        return result;
    }
    public bool isLogin()
    {

        if (HttpContext.Current.Session["appdicho_username"] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Logout()
    {
        HttpContext.Current.Session.Clear();

    }
  
}