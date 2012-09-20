using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using AGenerator;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Generator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ViewModel ViewModel { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (File.Exists("data.dat"))
            {
                
                Stream stream = File.Open("data.dat", FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                try
                {
                    ViewModel = (ViewModel)bFormatter.Deserialize(stream);
                }
                catch (Exception)
                {
                    
                }
               
                stream.Close();
            }

        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Stream stream = File.Open("data.dat", FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            try
            {
                bFormatter.Serialize(stream, ViewModel);
            }
            catch (Exception)
            {

            }
          
            stream.Close();
        }
    }
}
