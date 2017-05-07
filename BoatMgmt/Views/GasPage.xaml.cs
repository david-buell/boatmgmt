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
        private Controller Controller;

        public GasPage()
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
                txtGasUsedGal.Text = string.Format("{0:0.##} gallons", Controller.GasUsedInGallons());
                txtGasUsedML.Text = string.Format("{0:0.##} ml", Controller.GasUsedInML());

                txtGasLeftGal.Text = string.Format("{0:0.##} gallons", 37 - Controller.GasUsedInGallons());
                txtGasLeftPercent.Text = string.Format("{0:0.##}%", ((37.0 - Controller.GasUsedInGallons()) / 37.0) * 100);

                txtGasRateGal.Text = string.Format("{0:0.##} gal / hour", Controller.CurrentGallonsPerHour());
                txtGasRateML.Text = string.Format("{0:0.##} ml / minute", Controller.CurrentMlPerMinute());

                txtCruisingSpeed.Text = string.Format("{0:0.##} miles / hour", Controller.CruisingSpeed);
                txtCruisingGas.Text = string.Format("{0:0.##} miles / gal", Controller.CruisingGas);

                txtDistanceCurrentSpeed.Text = string.Format("{0:0.##} miles @ current", Controller.DistanceLeft(SpeedEnum.Current));
                txtDistanceCruisingSpeed.Text = string.Format("{0:0.##} miles @ cruise", Controller.DistanceLeft(SpeedEnum.Cruise));
                txtDistanceMaxSpeed.Text = string.Format("{0:0.##} miles @ max", Controller.DistanceLeft(SpeedEnum.Max));
            }
            catch (Exception) { }
        }
    }
}
