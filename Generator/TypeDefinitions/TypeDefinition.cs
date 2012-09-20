using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGenerator
{
    [Serializable]
    public class TypeDefinition
    {
        public string Namespace { set; get; }
        public string TemplatePath { get; set; }
        public string OutputDir { get; set; }
        public string Sufix { get; set; }
        public string Prefix { get; set; }
        public bool DirectoryPerEntity { get; set; }
        public string Name { get; set; }
        private string usingNamespaces;

        public string UsingNamespaces
        {
            get { 
                return usingNamespaces == null ? "" : usingNamespaces; 
            }
            set { usingNamespaces = value; }
        }

       
        public string Modulo { get; set; }
        
    }
}
