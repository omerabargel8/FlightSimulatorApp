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

namespace FlightSimulatorApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        flightViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            myAppModel b = new myAppModel(new MyTelnetClient());
            vm = new flightViewModel(b);
            DataContext = vm;
            b.connect("127.0.0.1", 5402);
            b.start();
        }
    }
}
