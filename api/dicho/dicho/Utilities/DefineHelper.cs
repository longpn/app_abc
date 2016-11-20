using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Utilities
{
    public class DefineHelper
    {




        /// <summary>
        /// Defines all extension of the supported image
        /// </summary>
        public static List<string> SupportedImage
        {

            get
            {
                return new List<string>() { ".jpg", ".jpeg", "jpe", ".gif", ".png", ".bmp" };

            }

        }

    }
}