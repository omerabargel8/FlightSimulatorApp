using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.controls
{
    /// <summary>
    /// Interaction logic for errorBoard.xaml
    /// </summary>
    public partial class errorBoard : UserControl
    {
        public errorBoard()
        {
            InitializeComponent();
            //set the dataContext from app
            DataContext = (Application.Current as App).ErrorBoardVM;
        }
    }
}
