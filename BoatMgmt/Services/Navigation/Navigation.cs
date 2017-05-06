using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BoatMgmt.Services.Navigation
{
    public static class Navigation
    {
        private static Frame _frame;

        public static Frame Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }

        public static bool Navigate(Type sourcePageType)
        {
            if (_frame.CurrentSourcePageType != sourcePageType)
            {
                return _frame.Navigate(sourcePageType);
            }

            return true;
        }

    }
}
