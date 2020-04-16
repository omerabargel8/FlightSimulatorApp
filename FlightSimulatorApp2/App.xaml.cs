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
        public IAppModel Model { get; private set; }
        public navigationControlVM NavigationControlVM { get; private set;}
        public mapViewModel MapVM { get; private set;}
        public dashboardViewModel DashboardVM { get; private set; }
        public errorBoardVM ErrorBoardVM { get; private set; }

        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            Model = new myAppModel(new MyTelnetClient());
            MapVM = new mapViewModel(Model);
            DashboardVM = new dashboardViewModel(Model);
            NavigationControlVM = new navigationControlVM(Model);
            ErrorBoardVM = new errorBoardVM(Model);

        }
    }
}
