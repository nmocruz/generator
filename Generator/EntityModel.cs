using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator;

namespace AGenerator
{
    [Serializable]
    public class EntityModel
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        
        public List<PropertyModel> Properties { get; set; }

    }
}
