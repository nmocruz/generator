using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator;

namespace AGenerator
{

    [Serializable]
    public class RepositoryDefinition : LayerDefinition
    {
        public RepositoryDefinition (ViewModel viewmodel) : base(viewmodel)
	    {
            this.TypesDefinitions = new List<TypeDefinition>(){
                new TypeDefinition()
                {
                    Name = "Repositories",
                    Namespace = "PCronos.{0}.Repositories",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\Repositorios.tt",
                    UsingNamespaces = "ApplySolutions.Framework.DataAccess",
                    OutputDir = @"C:\work\Generator\Generator\Output\Repositorios",
                    Sufix = "Repository",
                    Prefix = ""
                },
                new TypeDefinition()
                {
                    Name = "Interfaces",
                    Namespace = "PCronos.{0}.Repositories.Interfaces",
                    UsingNamespaces ="ApplySolutions.Framework.DataAccess",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\IRepositorios.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Repositorios.Interfaces",
                    Sufix = "Repository",
                    Prefix = "I"
                },
            };
        }
    }
}
