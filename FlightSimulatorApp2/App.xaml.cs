using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        myAppModel model;
        public navigationControlVM NavigationControlVM { get; private set;}
        public mapViewModel MapVM { get; private set;}
        public dashboardViewModel DashboardVM { get; private set; }

        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            model = new myAppModel(new MyTelnetClient());
            NavigationControlVM = new navigationControlVM(model);
            MapVM = new mapViewModel(model);
            DashboardVM = new dashboardViewModel(model);
            model.connect("127.0.0.1", 5402);
            model.start();
        }
    }
}
