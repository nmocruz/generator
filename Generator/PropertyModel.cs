using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGenerator
{
    [Serializable]
    public class PropertyModel
    {
        public string Nome { get; set; }

        public bool Required { get; set; }

        public string DataType { get; set; }

        public string ColumnName { get; set; }

        public int? Length { get; set; }

        public string DisplayName { get; set; }
    }
}
