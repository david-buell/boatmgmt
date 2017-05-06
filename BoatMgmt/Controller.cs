using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoatMgmt
{
    public class Controller
    {
        private static Controller _instance = null;

        private static readonly int SPEED_PIN = 25;
        private static readonly int FLOW_PIN = 26;
        private static readonly int AVG_FREQ = 1000;

        private static readonly double SPEED_TO_MILES = 1;

        private static readonly double FLOW_TO_ML = 0.46;
        private static readonly double SPEED_TO_MILE = 1;
        private static readonly double ML_TO_GALLON = 0.000264172;
        private static readonly double SEC_TO_HOURS = 3600;

        private PinCounter flowPin; 
        private long flowTotal;
        private PerfCounter flowCounter;

        private PinCounter speedPin;
        private long speedTotal;
        private PerfCounter speedCounter;

        private Controller()
        {
            flowPin = new PinCounter(FLOW_PIN);
            flowCounter = new PerfCounter();
            flowTotal = 0;

            speedPin = new PinCounter(SPEED_PIN);
            speedCounter = new PerfCounter();
            speedTotal = 0;

            Timer timer = new Timer(new TimerCallback(TimerTask), null, AVG_FREQ, AVG_FREQ);
        }

        public static Controller Instance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
        }

        private void TimerTask(object StateObj)
        {
            try
            {
                flowTotal += flowPin.Counter;
                flowCounter.Add(flowPin.Counter);
                flowPin.Reset();

                speedTotal += speedPin.Counter;
                speedCounter.Add(speedPin.Counter);
                speedPin.Reset();
            }
            catch (Exception) { }
        }

        public double TotalML()
        {
            return flowTotal * FLOW_TO_ML;
        }

        public double TotalGallons()
        {
            return TotalML() * ML_TO_GALLON;
        }

        public double TotalMiles()
        {
            return speedTotal * SPEED_TO_MILES;
        }

        public double LastGallonsPerHour()
        {
            return flowCounter.LastValue() * FLOW_TO_ML * ML_TO_GALLON * SEC_TO_HOURS;
        }

        public double LastMilesPerHour()
        {
            return speedCounter.LastValue() * SPEED_TO_MILES * SEC_TO_HOURS;
        }

        public double LastMilesPerGallon()
        {
            var miles = speedCounter.LastValue() * SPEED_TO_MILES;
            var gallons = flowCounter.LastValue() * FLOW_TO_ML * ML_TO_GALLON;

            return gallons > 0 ? miles/gallons : 0;
        }
    }
}
