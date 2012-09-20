using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using System.ComponentModel;
using Generator;

namespace AGenerator
{
    public class ActionCommand : ICommand
    {
        public Action<object> Action { get; set; }
        public Predicate<object> CanExecutePredicate { get; set; }
        
        public ActionCommand(Action<object> action, Predicate<object> canExecute)
        {
            this.Action = action;
            this.CanExecutePredicate = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return CanExecutePredicate == null || CanExecutePredicate(parameter); 
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Action(parameter);
        }
    }
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            Entidades = new ObservableCollection<EntityViewModel>();
            this.HbmPath = @"C:\work\Generator\Generator\Hbms";
            this.TemplatesBaseDir = @"C:\Projetos\prk\Generator\Generator\Templates";
        }

        private string hbmPath;

        public string HbmPath
        {
            get { return hbmPath; }
            set
            {
                hbmPath = value; 
                OnPropertyChanged("HbmPath");
            }
        }
        private RepositoryDefinition _repositoryDefinition;

        public RepositoryDefinition RepositoryDefinition
        {
            get { return _repositoryDefinition; }
            set { _repositoryDefinition = value; OnPropertyChanged("RepositoryDefinition"); }
        }

        public ServiceDefinition ServiceDefinition { get; set; }
        public ControllerDefinition ControllerDefinition { get; set; }
        public ViewModelDefinition ViewModelDefinition { get; set; }
        public ViewDefinition ViewDefinition { get; set; }

        public string TemplatesBaseDir { get; set; }
        public bool Views { get; set; }
        public bool ViewModels { get; set; }
        public bool Mapping { get; set; }
        public bool Repositorios { get; set; }
        public bool Servicos { get; set; }

        IList<LayerDefinition> layers;
        public IList<LayerDefinition> Layers 
        { 
            get {
                if(layers==null)
                    layers= new List<LayerDefinition>(){
                        this.ControllerDefinition,
                        this.RepositoryDefinition,
                        this.ServiceDefinition,
                        this.ViewModelDefinition,
                        this.ViewDefinition
                        };
                return layers;
            }
        }
        public ObservableCollection<EntityViewModel> Entidades { get; set; }
        public ICommand Ler { get; set; }
        public ICommand Gerar { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string p) { if(PropertyChanged!=null) PropertyChanged(this,new PropertyChangedEventArgs(p)); }
    }
    public class EntityViewModel
	{
        public string Nome { get; set; }
        public bool Ativo{ get; set; }

        public string FilePath { get; set; }
    }
}
