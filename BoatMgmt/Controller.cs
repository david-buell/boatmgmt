using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BoatMgmt
{
    public class Controller
    {
        private static Controller _instance = null;

        private static readonly int SPEED_PIN = 25;
        private static readonly int FLOW_PIN = 26;
        private static readonly int AVG_FREQ = 1;

        private static readonly int MIN_CRUISING_SPEED = 15;
        
        private static double FLOW_TO_ML = 0.46;
        private static readonly double ML_TO_GALLON = 0.000264172;
        private static double SPEED_TO_FEET = 1;
        private static readonly double FEET_TO_MILE = 1.0/5280.0;
        private static readonly double SEC_TO_HOURS = 3600;

        private PinCounter flowPin; 
        private long flowTotal;
        private PerfCounter flowCounter;
        
        private PinCounter speedPin;
        private long speedTotal;
        private PerfCounter speedCounter;

        private double maxSpeed;
        private double maxSpeedMPG;

        #region Properties

        public double CruisingSpeed
        {
            get;
            set;
        }

        public  double CruisingGas
        {
            get;
            set;
        }

        public double TankSize
        {
            get;
            set;
        }
        
        public double FlowToML
        {
            get
            {
                return FLOW_TO_ML;
            }
            set
            {
                FLOW_TO_ML = value;
            }
        }

        public double SpeedToFeet
        {
            get
            {
                return SPEED_TO_FEET;
            }
            set
            {
                SPEED_TO_FEET = value;
            }
        }

        #endregion

        private Controller()
        {
            TankSize = 37;
            maxSpeed = 0;

            flowPin = new PinCounter(FLOW_PIN);
            flowCounter = new PerfCounter();
            flowTotal = 0;

            speedPin = new PinCounter(SPEED_PIN);
            speedCounter = new PerfCounter();
            speedTotal = 0;
            
            DispatcherTimer updateTimer = new DispatcherTimer();
            updateTimer.Tick += TimerTick;
            updateTimer.Interval = new TimeSpan(0, 0, AVG_FREQ);
            updateTimer.Start();
            
        }

        public static Controller Instance()
        {
            if (_instance == null)
            {
                _instance = new Controller();
            }
            return _instance;
        }

        private void Refresh()
        {
            try
            {
                flowTotal += flowPin.Counter;
                flowCounter.Add(flowPin.Counter);
                flowPin.Reset();

                speedTotal += speedPin.Counter;
                speedCounter.Add(speedPin.Counter);
                speedPin.Reset();

                var mph = CurrentMilesPerHour();
                var mpg = CurrentMilesPerGallon();
                if (mph > MIN_CRUISING_SPEED)
                {
                    if (CruisingSpeed <= MIN_CRUISING_SPEED && mpg > 0.01)
                    {
                        CruisingSpeed = mph;
                        CruisingGas = mpg;
                    }

                    if (mpg < CruisingGas && mpg > 0.01)
                    {
                        CruisingSpeed = mph;
                        CruisingGas = mpg;
                    }
                }

                if (mph > maxSpeed)
                {
                    maxSpeed = mph;
                    maxSpeedMPG = mpg;
                }
            }
            catch (Exception) { }
        }

        private void TimerTick(object StateObj, object e)
        {
            Refresh();
        }

        public double GasUsedInML()
        {
            return flowTotal * FLOW_TO_ML;
        }

        public double GasUsedInGallons()
        {
            return GasUsedInML() * ML_TO_GALLON;
        }

        public double GasLeftInGallons()
        {
            return TankSize - GasUsedInGallons();
        }

        public double DistanceTraveledInMiles()
        {
            return speedTotal * SPEED_TO_FEET * FEET_TO_MILE;
        }

        public double CurrentMlPerMinute()
        {
            return flowCounter.LastValue() * FLOW_TO_ML * 60;
        }

        public double CurrentGallonsPerHour()
        {
            return flowCounter.LastValue() * FLOW_TO_ML * ML_TO_GALLON * SEC_TO_HOURS;
        }

        public double CurrentMilesPerHour()
        {
            return speedCounter.LastValue() * SPEED_TO_FEET * FEET_TO_MILE * SEC_TO_HOURS;
        }

        public double CurrentMilesPerGallon()
        {
            var miles = speedCounter.LastValue() * SPEED_TO_FEET * FEET_TO_MILE;
            var gallons = flowCounter.LastValue() * FLOW_TO_ML * ML_TO_GALLON;

            return gallons > 0 ? miles / gallons : 0;
        }

        public double MaxSpeed()
        {
            return maxSpeed;
        }
        
        public void FillTank(double amount)
        {
            if (amount >= TankSize)
            {
                flowTotal = 0;
            }
            else if (amount >= 0)
            {
                flowTotal = Convert.ToInt64(TankSize / ML_TO_GALLON / FLOW_TO_ML) -  Convert.ToInt64(amount / ML_TO_GALLON / FLOW_TO_ML);
            }
        }

        public double DistanceLeft(SpeedEnum speed)
        {
            switch(speed)
            {
                case SpeedEnum.Current:
                    return CurrentMilesPerGallon() * GasLeftInGallons();
                case SpeedEnum.Cruise:
                    return CruisingGas * GasLeftInGallons();
                case SpeedEnum.Max:
                    return maxSpeedMPG * GasLeftInGallons();
                default:
                    return 0;
            }
        }
    }
}
