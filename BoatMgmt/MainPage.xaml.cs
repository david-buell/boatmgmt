using BoatMgmt.Services.Navigation;
using BoatMgmt.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
        private Controller Controller;
        
        public MainPage()
        {
            this.InitializeComponent();

            Navigation.Frame = SplitViewFrame;
            Navigation.Navigate(typeof(Home));

            Controller = Controller.Instance();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void MenuMain_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(Home));
        }

        private void MenuGas_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(GasPage));
        }

        private void MenuSpeed_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(SpeedPage));
        }

        private void MenuGraph_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(GraphPage));
        }


        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(SettingsPage));
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, TimeSpan.FromSeconds(0.5));
        }
    }
}
