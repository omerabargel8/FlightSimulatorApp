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

namespace FlightSimulatorApp2.controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private Point firstPoint = new Point();
        public static readonly DependencyProperty xPosProperty =
           DependencyProperty.Register("xPos", typeof(double), typeof(Joystick));
        public double xPos
        {
            get
            {
                Console.WriteLine("###############");
                return (double)GetValue(xPosProperty);
            }
            set { Console.WriteLine("$$$${0}", value); SetValue(xPosProperty, value); }
        }
        public static readonly DependencyProperty yPosProperty =
           DependencyProperty.Register("yPos", typeof(double), typeof(Joystick));
        public double yPos
        {
            get {return (double)GetValue(yPosProperty);}
            set {SetValue(yPosProperty, value); }
        }
        private void centerKnob_Completed(object sender, EventArgs e) { }
        public Joystick()
        {
            InitializeComponent();
        }


        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                firstPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - firstPoint.X;
                double y = e.GetPosition(this).Y - firstPoint.Y;
                if (Math.Sqrt(x*x + y*y) <= Base.Width / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    xPos = x / (Base.Width/2);
                    yPos = y / (Base.Width/2);
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            xPos = 0;
            yPos = 0;
        }

        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            xPos = 0;
            yPos = 0;
        }
    }

}
