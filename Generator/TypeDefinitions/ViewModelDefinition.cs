using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator;

namespace AGenerator
{

    [Serializable]
    public class ViewModelDefinition : LayerDefinition
    {
        public ViewModelDefinition(ViewModel viewmodel)
            : base(viewmodel)
        {
            this.TypesDefinitions = new List<TypeDefinition>(){
                new TypeDefinition(){
                    Name = "Collections",
                    Namespace = "PCronos.{0}.ViewModels",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\ViewModels.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\ViewModels",
                    Sufix = "ViewModel",
                },
                new TypeDefinition(){
                    Name = "Entity",
                    Namespace = "PCronos.{0}.ViewModels",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\ViewModels.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\ViewModels",
                    Sufix = "ViewModel",
                }
            };
        }
    }
}
