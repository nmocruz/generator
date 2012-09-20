using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Linq;
using Microsoft.VisualStudio.TextTemplating;
using AGenerator;
using AGenerator.Templating;

namespace Generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            ViewModel viewModel = null;
            if ((App.Current as App).ViewModel == null)
            {
               
                viewModel = new ViewModel();
                viewModel.ControllerDefinition = new ControllerDefinition(viewModel);
                viewModel.RepositoryDefinition = new RepositoryDefinition(viewModel);
                viewModel.ServiceDefinition = new ServiceDefinition(viewModel);
                viewModel.ViewDefinition = new ViewDefinition(viewModel);
                viewModel.ViewModelDefinition = new ViewModelDefinition(viewModel);
            }
            else
                viewModel = (App.Current as App).ViewModel;
 
            Engine engine = new Engine();
            TextTemplateHost host;
            
            viewModel.Ler = new ActionCommand(
                a => {
                    if(Directory.Exists(a as string))
                        Directory.GetFiles(a as string, "*.hbm.xml").ToList().ForEach(c =>
                        {
                            XDocument doc = XDocument.Load(c);
                        
                            viewModel.Entidades.Add(new EntityViewModel 
                            { 
                                FilePath = c,
                                Nome = doc.Descendants().Where(it=>it.Name.LocalName == "class").FirstOrDefault().Attribute("name").Value
                            });
                        });
                },
                a =>  true
            );

            viewModel.Gerar = new ActionCommand(
                c => {
                    var entities = new List<EntityModel>();
                    string filecontent = "";
                    string template = "";
                   
                    viewModel.Entidades.Where(it => it.Ativo).ToList().ForEach(it => 
                    {
                        XDocument doc = XDocument.Load(it.FilePath);
                        var entity = new EntityModel()
                        {
                            ClassName = it.Nome.Contains(",") ? it.Nome.Split(',')[0].Contains(".") ? it.Nome.Split(',')[0].Substring(it.Nome.Split(',')[0].LastIndexOf(".")+1) : it.Nome.Split(',')[0] : it.Nome,
                            Properties = doc.Descendants()
                                            .Where(d => d.Name.LocalName == "property" || d.Name.LocalName == "many-to-one" || d.Name.LocalName =="set")
                                            .Select(d => new PropertyModel()
                                            {
                                                Nome = d.Attribute("name").Value,
                                                Required = d.Attribute("not-null") == null ? false : d.Attribute("not-null").Value == "true",
                                                DataType = d.Attribute("type") ==null ? d.Attribute("name").Value : d.Attribute("type").Value,
                                                ColumnName = d.Attribute("column") == null ? null : d.Attribute("column").Value.Replace("`", ""),
                                                Length = d.Attribute("length") == null ? null : (int?)Int32.Parse(d.Attribute("length").Value.Replace("`", "")),
                                                DisplayName = d.Attribute("name").Value
                                            }).ToList()
                        };
                        entities.Add(entity);    
                        
                    });

                    foreach (LayerDefinition item in viewModel.Layers.Where(it=>it.Ativo  ))
                    {
                        foreach (var typedef in item.TypesDefinitions)
                        {
                            host = new TextTemplateHost() { TemplateFile = typedef.TemplatePath };
                            template = File.ReadAllText(typedef.TemplatePath);

                            foreach (EntityModel entity in entities)
                            {
                                host.Model = entity;
                                host.TypeDefinition = typedef;

                                filecontent = engine.ProcessTemplate(template, host);
                                

                                if (!Directory.Exists(typedef.OutputDir + "\\" + (typedef.DirectoryPerEntity ? "\\" + entity.ClassName + "\\" : "")))
                                    Directory.CreateDirectory(typedef.OutputDir + "\\" + (typedef.DirectoryPerEntity ? "\\" + entity.ClassName + "\\" : ""));
                               
                                if(host.Errors.HasErrors)
                                    File.WriteAllText(typedef.OutputDir + "\\" + (typedef.DirectoryPerEntity ? "\\" + entity.ClassName + "\\" : "") + typedef.Prefix + entity.ClassName + typedef.Sufix + ".cs", host.Errors.ToString());
                                else
                                    File.WriteAllText(typedef.OutputDir + "\\" + (typedef.DirectoryPerEntity ? "\\" + entity.ClassName + "\\" : "") + typedef.Prefix + entity.ClassName + typedef.Sufix + ".cs", filecontent);
                                
                            }
                        }
                        
                    }
                   
                },
                c => true);

            DataContext = viewModel;
        }
    }
}
