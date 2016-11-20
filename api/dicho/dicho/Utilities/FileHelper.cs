using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading.Tasks;

namespace dicho.Utilities
{
    public class FileHelper
    {

        /// <summary>
        /// Format file name and convert it to lower
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FormatFileName(string fileName)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                if (Path.HasExtension(fileName))
                {
                    str = string.Format("{0}{1}", Guid.NewGuid().ToString().ToLower(), Path.GetExtension(fileName).ToLower());
                }
            }

            return str;
        }

        public static bool IsValidImage(string fileName)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(fileName))
            {
                if (Path.HasExtension(fileName))
                {
                    if (DefineHelper.SupportedImage.Contains(Path.GetExtension(fileName).ToLower()))
                    {
                        result = true;

                    }
                }
            }

            return result;
        }

        public static string GenerateUserFolderName(long userID)
        {
            string folderName = string.Empty;
            if (userID > 0 && userID < 10)
            {
                folderName = string.Format("u00{0}", userID);
            }
            else if (userID < 100)
            {
                folderName = string.Format("u0{0}", userID);
            }
            else
            {
                folderName = string.Format("{u{0}");
            }

            return folderName;
        }
        public static string GenerateBillFolderName(long billID)
        {
            string folderName = string.Empty;
            if (billID > 0 && billID < 10)
            {
                folderName = string.Format("b00{0}", billID);
            }
            else if (billID < 100)
            {
                folderName = string.Format("b0{0}", billID);
            }
            else
            {
                folderName = string.Format("{b{0}");
            }

            return folderName;
        }
        public static string CreateFolderUserTemp(long userID)
        {
            string path = @"c:\MyDir\" + GenerateUserFolderName(userID) + "\\Temp\\";

            // Determine whether the directory exists.
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            return path;
        }
        public static string CreateFolderUserBill(long userID, long billID)
        {
            string path = @"c:\MyDir\" + GenerateUserFolderName(userID) + "\\" + GenerateBillFolderName(billID) + "\\";

            // Determine whether the directory exists.
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            return path;
        }
        public static void MoveBillFile(long userID, long billID, List<string> fileNames)
        {
            string sourceFileName = HttpContext.Current.Server.MapPath("~/Uploads/" + FileHelper.GenerateUserFolderName(userID) + "/Temp");
            string destFileName = HttpContext.Current.Server.MapPath("~/Uploads/" + FileHelper.GenerateUserFolderName(userID) + FileHelper.GenerateBillFolderName(billID));
            if (!Directory.Exists(destFileName))
            {
                Directory.CreateDirectory(destFileName);
            }
            if (fileNames != null && fileNames.Count > 0)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        foreach (var item in fileNames)
                        {
                            File.Move(sourceFileName + "/" + item.ToString(), destFileName + "/" + item.ToString());
                        }
                    }
                    catch { }

                });

            }


        }


    }
}