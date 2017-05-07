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
    public sealed partial class SpeedPage : Page
    {
        private Controller Controller;

        public SpeedPage()
        {
            this.InitializeComponent();

            Controller = Controller.Instance();

            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Tick += TimerTick;
            updateTimer.Interval = new TimeSpan(0, 0, MainPage.UPDATE_FREQ);
            updateTimer.Start();

            Refresh();
        }

        private void TimerTick(object sender, object e)
        {
            Refresh();
        }

        private void Refresh()
        {
            try
            {
                txtSpeedMPH.Text = string.Format("{0:0.##} mph", Controller.CurrentMilesPerHour());
                txtMax.Text = string.Format("{0:0.##} mph", Controller.MaxSpeed());
                txtCruising.Text = string.Format("{0:0.##} mph", Controller.CruisingSpeed);
            }
            catch (Exception) { }
        }
    }

}
