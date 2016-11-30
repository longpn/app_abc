using dicho.Models;
using dicho.Models.OutputData;
using dicho.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.DatabaseInteract
{
    public class CategoryInteract
    {
        /// <summary>
        /// Get all categories by type 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static OutputDataModel GetAll(int type)
        {
            OutputDataModel outputData = new OutputDataModel();
            outputData.code = (int)Enums.StatusCode.Successful;
            outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);
            using (DichoDataContext db = new DichoDataContext())
            {
                var items = db.proc_Category_Get(type).ToList();
                if (items.Count > 0)
                {
                    List<CategoryOutputData> categoryList = new List<CategoryOutputData>();
                    foreach (var item in items)
                    {
                        CategoryOutputData category = new CategoryOutputData();
                        category.category_id = item.category_id;
                        category.category_name = item.category_name;
                        categoryList.Add(category);
                    }

                    outputData.Data = categoryList;
                }
            }

            return outputData;
        }
    }
}