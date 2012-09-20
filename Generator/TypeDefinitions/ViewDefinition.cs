using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AGenerator;

namespace Generator
{

    [Serializable]
    public class ViewDefinition : LayerDefinition
    {
        public ViewDefinition(ViewModel viewmodel)
            : base(viewmodel)
        {
            this.TypesDefinitions = new List<TypeDefinition>(){
                new TypeDefinition(){
                    
                    Name ="EditView",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\EditView.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Views",
                    Sufix = "",
                    Prefix = "Edit",
                    DirectoryPerEntity = true
                },
                new TypeDefinition(){
                    
                    Name = "DetailsView",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\DetailView.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Views",
                    Sufix = "",
                    Prefix = "Detail",
                    DirectoryPerEntity = true
                },
                new TypeDefinition(){
                    
                    Name = "SearchView",
                    TemplatePath = MainViewModel.TemplatesBaseDir + @"\IndexView.tt",
                    OutputDir = @"C:\work\Generator\Generator\Output\Views",
                    Sufix = "",
                    Prefix = "Index",
                    DirectoryPerEntity = true
                }
                //,
                //new TypeDefinition(){
                    
                //    TemplatePath = MainViewModel.TemplatesBaseDir + @"\DeleteView.tt",
                //    OutputDir = @"C:\work\Generator\Generator\Output\Views",
                //    Sufix = "",
                //    Prefix = "Delete",
                //    DirectoryPerEntity = true
                //}
            };
        }
    }
}
