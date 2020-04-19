using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class navigationControlVM
    {
        private IAppModel model;
        private double throttle = 0;
        private double aileron = 0;
        //constructor
        public navigationControlVM(IAppModel model)
        {
            this.model = model;
        }
        //properties
        public double VM_xPos
        {
            set => model.Rudder = value;
        }
        public double VM_yPos
        {
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
