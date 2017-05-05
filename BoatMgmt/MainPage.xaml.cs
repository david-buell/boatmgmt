using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BoatMgmt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static readonly int UPDATE_FREQ = 1;
        private Controller Controller;
        
        public MainPage()
        {
            this.InitializeComponent();

            Controller = new Controller();

            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Interval = new TimeSpan(0, 0, UPDATE_FREQ);
            updateTimer.Start();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void UpdateTimer_Tick(object sender, object e)
        {
            try
            {
                txtBlock1.Text = string.Format("{0} MPG", Controller.LastMilesPerGallon());
                txtBlock2.Text = string.Format("{0} MPH", Controller.LastMilesPerHour());
                txtBlock3.Text = string.Format("{0} Gallons", Controller.TotalGallons());
                txtBlock4.Text = string.Format("{0} Miles", Controller.TotalMiles());
            }
            catch (Exception) { }
        }
    }
}
