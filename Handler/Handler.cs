using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.Reflection;

namespace LightWeightRulesEngine
{
   public abstract class Handler
    {
        private static string _ConnectionString = String.Empty;
       
        public static string ConnectionString
        {
            get
            {
                string pathRaw = Assembly.GetExecutingAssembly().Location;
                ConnectionStringPath = pathRaw.Substring(0, pathRaw.IndexOf("bin") - 1);
                if (_ConnectionString == String.Empty)
                    _ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.Replace("XXXXXX", ConnectionStringPath);
                
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        public abstract int HandleRequest(ref int exitPath, ref XElement Application);
        
       private static string _ConnectionStringPath = string.Empty;
        public static string ConnectionStringPath
        {
            get { return _ConnectionStringPath; }
            set { _ConnectionStringPath = value; }
        }
     
    }
}
