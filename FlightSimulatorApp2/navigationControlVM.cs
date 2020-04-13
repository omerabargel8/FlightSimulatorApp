using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    class navigationControlVM : INotifyPropertyChanged
    {
        private myAppModel model;
        private double xPos;
        private double yPos;
        public event PropertyChangedEventHandler PropertyChanged;
        public navigationControlVM(myAppModel model)
        {
            this.model = model;
        }
        public double VM_xPos
        {
            get {
                return this.xPos; 
            }
            set
            {
                if (this.xPos != value)
                {
                    this.xPos = value;
                    model.setRudder(value);
                }
            }
        }
        public double VM_yPos
        {
            get
            {
                return this.yPos;
            }
            set
            {
                if (this.yPos != value)
                {
                    this.yPos = value;
                    model.setElevator(value);
                }
            }
        }
    }
}
