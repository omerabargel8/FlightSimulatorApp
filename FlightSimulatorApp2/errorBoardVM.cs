﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp2
{
    public class errorBoardVM :INotifyPropertyChanged
    {
        private IAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public errorBoardVM(IAppModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public string VM_errors
        {
            get { return model.Errors; }
        }
    }
}
