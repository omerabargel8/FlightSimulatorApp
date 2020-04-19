using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class appViewModel : INotifyPropertyChanged
    {
    private IAppModel appModel;
    public event PropertyChangedEventHandler PropertyChanged;
        public appViewModel(IAppModel appModel)
        {
            this.appModel = appModel;
            navigationControlVM navigationControlVM = new navigationControlVM(appModel);
            mapViewModel mapVM = new mapViewModel(appModel);
            dashboardViewModel dashboardVM = new dashboardViewModel(appModel);
        }
    }
}
