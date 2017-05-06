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
    public sealed partial class GasPage : Page
    {
        private static readonly int UPDATE_FREQ = 1;
        private Controller Controller;

        public GasPage()
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
                txtGasUsedGal.Text = string.Format("{0} Gallons", Controller.TotalGallons());
                txtGasUsedML.Text = string.Format("{0} ML", Controller.TotalML());

                txtGasLeftGal.Text = string.Format("{0} Gallons", 37 - Controller.TotalGallons());
                txtGasLeftPercent.Text = string.Format("{0}%", ((37.0 - Controller.TotalGallons()) / 37.0) * 100);
            }
            catch (Exception) { }
        }
    }
}
