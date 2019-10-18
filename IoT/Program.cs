using System;
using System.Device.Gpio;

namespace IoT
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new System.Device.Gpio.Drivers.Windows10Driver();
            
            var controller = new GpioController(PinNumberingScheme.Board, driver);
            controller.SetPinMode(1, PinMode.Output);
            controller.Write(1, PinValue.High);
        }
    }
}
