using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BoatMgmt.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        private static readonly int UPDATE_FREQ = 1;
        private Controller Controller;
        private int counter = 0;

        public Home()
        {
            this.InitializeComponent();
            
            Controller = Controller.Instance();

            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Interval = new TimeSpan(0, 0, UPDATE_FREQ);
            updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, object e)
        {
            try
            {
                txtBlock1.Text = string.Format("{0} MPG", Controller.LastMilesPerGallon());
                txtBlock2.Text = string.Format("{0} MPH", Controller.LastMilesPerHour());
                txtBlock3.Text = string.Format("{0} Gallons", Controller.TotalGallons());
                txtBlock4.Text = string.Format("{0} Miles", Controller.TotalMiles());

                txtCounter.Text = string.Format("Counter: {0}", ++counter);
            }
            catch (Exception) { }
        }
    }
}
