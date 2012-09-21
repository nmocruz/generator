using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using Generator;

namespace AGenerator.Templating
{
    [Serializable]
    public class TextTemplateHost : MarshalByRefObject, ITextTemplatingEngineHost
    {
        #region ITemplateContext Members
        public EntityModel Model { get; set; }
        
        #endregion


        private AppDomain domain;
        private CompilerErrorCollection errorsValue;
        private Encoding fileEncodingValue = Encoding.UTF8;
        private string fileExtensionValue = ".txt";

        public string AssemblyPath { get; set; }

        public CompilerErrorCollection Errors
        {
            get { return errorsValue; }
        }

        public Encoding FileEncoding
        {
            get { return fileEncodingValue; }
        }

        public string FileExtension
        {
            get { return fileExtensionValue; }
        }

        private string _templateFile;
        public string TemplateFile
        {
            set { _templateFile = value; }
        }

        #region ITextTemplatingEngineHost Members

        object ITextTemplatingEngineHost.GetHostOption(string optionName)
        {
            string str;
            return (((str = optionName) != null) && (str == "CacheAssemblies"));
        }

        bool ITextTemplatingEngineHost.LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = String.Empty;
            location = String.Empty;

            //If the argument is the fully qualified path of an existing file,
            //then we are done.
            //----------------------------------------------------------------
            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }

                    //This can be customized to search specific paths for the file.
            //This can be customized to accept paths to search as command line
            //arguments.
            //----------------------------------------------------------------
            else
            {
                return false;
            }
        }

        void ITextTemplatingEngineHost.LogErrors(CompilerErrorCollection errors)
        {
            errorsValue = errors;
        }

        AppDomain ITextTemplatingEngineHost.ProvideTemplatingAppDomain(string content)
        {
            // Set up the AppDomainSetup
            var setup = new AppDomainSetup();
            setup.ApplicationBase = AssemblyPath;


            // Create the AppDomain      
            domain = AppDomain.CreateDomain("Generation App Domain", null, setup);
            domain.SetData("host", this);
            return domain;
        }

        string ITextTemplatingEngineHost.ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }
            string path = Path.Combine(Path.GetDirectoryName(((ITextTemplatingEngineHost)this).TemplateFile), assemblyReference);
            if (File.Exists(path))
            {
                return path;
            }
            return "";
        }

        System.Type ITextTemplatingEngineHost.ResolveDirectiveProcessor(string processorName)
        {
            throw new Exception("Directive Processor " + processorName + " not found");
        }

        string ITextTemplatingEngineHost.ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }
            return string.Empty;
        }

        string ITextTemplatingEngineHost.ResolvePath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }
            if (!File.Exists(path))
            {
                string str = Path.Combine(Path.GetDirectoryName(((ITextTemplatingEngineHost)this).TemplateFile), path);
                if (File.Exists(str))
                {
                    return str;
                }
            }
            return path;
        }

        void ITextTemplatingEngineHost.SetFileExtension(string extension)
        {
            fileExtensionValue = extension;
        }

        void ITextTemplatingEngineHost.SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            fileEncodingValue = encoding;
        }

        IList<string> ITextTemplatingEngineHost.StandardAssemblyReferences
        {
            get
            {
                return new[]
                                        {
                                                typeof(Uri).Assembly.Location, 
                                                Assembly.GetExecutingAssembly().Location
                                                
                                        };
            }
        }

        IList<string> ITextTemplatingEngineHost.StandardImports
        {
            get { return new[] { "System", typeof(TextTemplateHost).Namespace }; }
        }

        string ITextTemplatingEngineHost.TemplateFile
        {
            get { return _templateFile; }
        }

        #endregion

        public TypeDefinition TypeDefinition { get; set; }
    }
}

