using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    public class navigationControlVM : INotifyPropertyChanged
    {
        private IAppModel model;
        private double xPos;
        private double yPos;
        private double throttle = 0;
        private double aileron = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public navigationControlVM(IAppModel model)
        {
            this.model = model;
        }
        public double VM_xPos
        {
            get {
                return this.xPos; 
            }
            set => model.Rudder = value;
        }
        public double VM_yPos
        {
            get {return this.yPos;}
            set => model.Elevator = value;
        }

        public double VM_throttle
        {
            get { return this.throttle; }
            set => model.Throttle = value;
        }
        public double VM_aileron
        {
            get { return this.aileron; }
            set => model.Aileron = value;
        }

    }
}
