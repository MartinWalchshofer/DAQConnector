using DAQConnector;
using System;
using System.Collections.Generic;

namespace DAQConnectorTester
{
    internal class Program
    {
        /// <summary>
        /// This example shows how to receive data from multiple devices with different samplingRates, signal frequencies and signal amplitudes
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //get available devices
            List<string> devices = DAQSimulator.GetAvailableDevices();

            //open devices
            DAQSimulator simulator1 = new DAQSimulator(250, 10, 10);
            DAQSimulator simulator2 = new DAQSimulator(50, 100, 20);
            DAQSimulator simulator3 = new DAQSimulator(10, 100, 20);
            DAQSimulator simulator4 = new DAQSimulator(5, 100, 20);
            DAQSimulator simulator5 = new DAQSimulator(1, 100, 20);

            //attach to data available event
            simulator1.OnDataAvailable += DataAvailable;
            simulator2.OnDataAvailable += DataAvailable;
            simulator3.OnDataAvailable += DataAvailable;
            simulator4.OnDataAvailable += DataAvailable;
            simulator5.OnDataAvailable += DataAvailable;

            //open device / start data acquisition
            simulator1.Open(devices[0]);
            simulator2.Open(devices[1]);
            simulator3.Open(devices[2]);
            simulator4.Open(devices[3]);
            simulator5.Open(devices[4]);

            Console.WriteLine("Press ENTER to terminate data acquisition.");
            Console.ReadLine();

            //close devices
            simulator1.Close();
            simulator2.Close();
            simulator3.Close();
            simulator4.Close();
            simulator5.Close();

            //detach from data available event / stop data acquisition
            simulator1.OnDataAvailable -= DataAvailable;
            simulator2.OnDataAvailable -= DataAvailable;
            simulator3.OnDataAvailable -= DataAvailable;
            simulator4.OnDataAvailable -= DataAvailable;
            simulator5.OnDataAvailable -= DataAvailable;

            simulator1 = null;
            simulator2 = null;
            simulator3 = null;
            simulator4 = null;
            simulator5 = null;

            GC.Collect();
        }

        private static void DataAvailable(object sender, double[] e)
        {
            if (sender == null || e == null)
                Console.WriteLine("Invalid data received.");
            
            if (sender != null && e != null)
                Console.WriteLine("Device: {0} \t Data: [{1}]", sender.ToString(), string.Join(",", e));
        }
    }
}
