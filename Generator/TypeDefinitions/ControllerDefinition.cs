using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator;

namespace AGenerator
{

    [Serializable]
    public class ControllerDefinition : LayerDefinition
    {
        public ControllerDefinition(ViewModel viewmodel) : base(viewmodel)
        {

            this.TypesDefinitions= new List<TypeDefinition>( )
            {
                new TypeDefinition(){
                    Name = "Controllers",
                    Namespace = "PCronos.{0}.Controllers",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\Controllers.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Controllers",
                    Sufix = "Controller",
                    Prefix = ""
                }
            };
        }
    }
}
