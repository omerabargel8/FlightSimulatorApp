using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class errorBoardVM :INotifyPropertyChanged
    {
        private IAppModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        //constructor
        public errorBoardVM(IAppModel model)
        {
            this.model = model;
            //creates this delegate method for bunding values from the model to view
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
        //properties
        public string VM_errors
        {
            get { return model.Errors; }
        }
        public string VM_status
        {
            get { return model.Status; }
        }
    }
}
