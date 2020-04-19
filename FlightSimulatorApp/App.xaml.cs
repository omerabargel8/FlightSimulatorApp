using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //properties
        public IAppModel Model { get; private set; }
        public navigationControlVM NavigationControlVM { get; private set;}
        public mapVM MapVM { get; private set;}
        public dashboardVM DashboardVM { get; private set; }
        public errorBoardVM ErrorBoardVM { get; private set; }

        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            //creates all VM's
            Model = new myAppModel(new MyTelnetClient());
            MapVM = new mapVM(Model);
            DashboardVM = new dashboardVM(Model);
            NavigationControlVM = new navigationControlVM(Model);
            ErrorBoardVM = new errorBoardVM(Model);

        }
    }
}
