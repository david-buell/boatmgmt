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
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            txtTankSize.Text = Controller.Instance().TankSize.ToString();
            txtGasLeft.Text = String.Format("{0:0.##}", Controller.Instance().GasLeftInGallons());
            txtGasConv.Text = Controller.Instance().FlowToML.ToString();
            txtSpeedConv.Text = Controller.Instance().SpeedToFeet.ToString();
        }

        #region Event Handlers

        private void txtTankSize_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.Instance().TankSize = Convert.ToDouble(txtTankSize.Text);
                txtGasLeft.Text = String.Format("{0:0.##}", Controller.Instance().GasLeftInGallons());
            }
            catch (Exception) { }
        }

        private void txtGasLeft_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.Instance().FillTank(double.Parse(txtGasLeft.Text));
                txtGasLeft.Text = String.Format("{0:0.##}", Controller.Instance().GasLeftInGallons());
            }
            catch (Exception) { }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Controller.Instance().FillTank(Controller.Instance().TankSize);
            txtGasLeft.Text = String.Format("{0:0.##}", Controller.Instance().GasLeftInGallons());
        }

        private void txtSpeedConv_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.Instance().SpeedToFeet = double.Parse(txtSpeedConv.Text);
            }
            catch (Exception) { }
        }

        private void txtGasConv_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.Instance().FlowToML = double.Parse(txtGasConv.Text);
            }
            catch (Exception) { }
        }

        #endregion
    }
}
