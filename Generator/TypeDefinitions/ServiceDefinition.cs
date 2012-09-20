using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGenerator;

namespace Generator
{

    [Serializable]
    public class ServiceDefinition : LayerDefinition
    {
        public ServiceDefinition(ViewModel viewmodel)
            : base(viewmodel)
        {
            this.TypesDefinitions = new List<TypeDefinition>(){
                new TypeDefinition(){
                    Name = "Services",
                    Namespace = "PCronos.{0}.Services",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\ViewModels.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Services",
                    Sufix = "Service",
                    Prefix = ""
                },
                new TypeDefinition(){
                    Name = "Interfaces",
                    Namespace = "PCronos.{0}.Services.Interfaces",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\IServicos.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Services\Interfaces",
                    Sufix = "Service",
                    Prefix = "I"
                }
            };
        }
    }
}
