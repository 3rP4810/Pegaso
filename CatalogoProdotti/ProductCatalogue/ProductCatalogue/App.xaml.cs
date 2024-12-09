﻿using System.Configuration;
using System.Data;
using System.Windows;

namespace ProductCatalogue
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow( new Models.ProductService());
            MainWindow.Show();
        }
    }

}