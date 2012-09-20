using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator;

namespace AGenerator
{
    [Serializable]
    public class MetadataDefinition : LayerDefinition
    {
        public MetadataDefinition(ViewModel viewmodel)
            : base(viewmodel)
        {
            this.TypesDefinitions = new List<TypeDefinition>(){
                new TypeDefinition{
                    Namespace = "ApplySolutions.PCronos.Controllers",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\Controller.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Controllers",
                    Sufix = "Controller"
                }

            };
        }
    }
}
