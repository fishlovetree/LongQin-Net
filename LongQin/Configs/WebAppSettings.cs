using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LongQin.Configs
{
    public class WebAppSettings
    {
        /// <summary>
        /// session名称
        /// </summary>
        public static string SessionName
        {
            get
            {
                return "longqin_session";
            }
        }

        /// <summary>
        /// cookie名称
        /// </summary>
        public static string CookieName
        {
            get
            {
                return "longqin_cookie";
            }
        }

        /// <summary>
        /// DES加密key
        /// </summary>
        public static string DesEncryptKey
        {
            get
            {
                return "we934okx09krefgj@sdlk$sdklwwsggc";
            }
        }
    }
}