using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DonaldWCFwebApi
{
    public static class Configuration
    {
        private static string _apiUri;
        public static string ApiUri
        {
            get { 
                _apiUri = _apiUri ?? ConfigurationManager.AppSettings["ApiUri"];
                return _apiUri;
            }
        }
    }
}