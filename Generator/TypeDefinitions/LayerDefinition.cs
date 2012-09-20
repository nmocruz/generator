using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGenerator.Templating;
using Generator;

namespace AGenerator
{
    [Serializable]
    public class LayerDefinition 
    {
        public LayerDefinition(ViewModel mainViewModel) 
        {
            this.MainViewModel = mainViewModel;
        }
        public ViewModel MainViewModel { set; get; }
        public string Nome { set; get; }
        public bool Ativo { set; get; }
        public TypeDefinition this[string index]
        {
            get { return TypesDefinitions.FirstOrDefault(c => c.Name == index); }
        }
        public List<TypeDefinition> TypesDefinitions { get; set; } 
    }
}
