using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace BoatMgmt
{
    public class PinCounter
    {
        private GpioPin pin;
        private long counter;
        
        public long Counter
        {
            get
            {
                return Interlocked.Read(ref counter);
            }
        }

        public PinCounter(int pinNum)
        {
            var gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                return;
            }

            pin = gpio.OpenPin(pinNum);
            pin.SetDriveMode(GpioPinDriveMode.InputPullDown);
            pin.ValueChanged += Pin_ValueChanged;
        }

        public void Reset()
        {
            Interlocked.Exchange(ref counter, 0);
        }
                
        private void Pin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            try
            {
                if (args.Edge == GpioPinEdge.RisingEdge)
                {
                    Interlocked.Increment(ref counter);
                }
            }
            catch (Exception) { }
        }
    }
}
