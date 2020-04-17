﻿using System;
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

namespace FlightSimulatorApp2.controls
{
    /// <summary>
    /// Interaction logic for Map.xaml
    /// </summary>
    public partial class map : UserControl
    {
        public map()
        {
            InitializeComponent();
            //set the dataContext from app
            DataContext = (Application.Current as App).MapVM;
        }
    }
}
