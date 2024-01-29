using System;
using System.Collections.Generic;
using System.Threading;

namespace DAQConnector
{
    /// <summary>
    /// This is an example how to implement the <see cref="IDAQ"/> interface for a device.
    /// The simulator device generates sine waves with a given frequency and amplitude.
    /// </summary>
    public class DAQSimulator : IDAQ
    {
        #region Events...

        public event EventHandler<double[]> OnDataAvailable;

        #endregion

        #region Private Members...

        private static List<string> _devices;
        private double _samplingRateHz = 250;
        private double _signalFrequencyHz = 10;
        private double _signalAmplitude = 50;
        private Thread _acquisitionThread;
        private bool _acquisitionThreadRunning;
        private string _deviceName;
        private double _t;

        #endregion

        /// <summary>
        /// Gets a list of available devices.
        /// </summary>
        /// <returns>List of available devices.</returns>
        public static List<string> GetAvailableDevices()
        {
            if (_devices == null)
            {
                _devices = new List<string>
                {
                    "DAQ Simulator 1",
                    "DAQ Simulator 2",
                    "DAQ Simulator 3",
                    "DAQ Simulator 4",
                    "DAQ Simulator 5",
                    "DAQ Simulator 6"
                };
            }
            return _devices;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DAQSimulator"/>
        /// </summary>
        /// <param name="samplingRateHz">The sampling rate of the device.</param>
        /// <param name="signalFrequencyHz">the signal Frequency in Hz.</param>
        /// <param name="signalAmplitude">The signal Amplitude</param>
        public DAQSimulator(double samplingRateHz, double signalFrequencyHz, double signalAmplitude) 
        {
            _samplingRateHz = samplingRateHz;
            _signalFrequencyHz = signalFrequencyHz;
            _signalAmplitude = signalAmplitude;
            _acquisitionThreadRunning = false;
        }

        /// <summary>
        /// Opens a dvices by it's name.
        /// </summary>
        /// <param name="deviceName">The device name as string.</param>
        /// <exception cref="Exception">An Exception is thrown if the requested device could not be found.</exception>
        public void Open(string deviceName)
        {
            bool foundDevice = false;
            foreach (string device in _devices)
            {
                if(device.Equals(deviceName))
                {
                    foundDevice = true;
                    break;
                }
            }

            if (!foundDevice)
                throw new Exception("Could not find requested device.");

            _deviceName = deviceName;

            StartAcquisitionThread();
        }

        /// <summary>
        /// Closes the device.
        /// </summary>
        public void Close()
        {
            StopAcquisitionThread();
        }

        /// <summary>
        /// Start the data generation thread.
        /// </summary>
        private void StartAcquisitionThread()
        {
            if(!_acquisitionThreadRunning)
            {
                _acquisitionThreadRunning=true;
                _acquisitionThread = new Thread(AcquisitionThread_DoWork);
                _acquisitionThread.Start();
            }
        }

        /// <summary>
        /// Stop the data generation thread
        /// </summary>
        private void StopAcquisitionThread()
        {
            if (_acquisitionThreadRunning)
            {
                _acquisitionThreadRunning = false;
                _acquisitionThread.Join(500);
            }
        }

        /// <summary>
        /// Data generation thread.
        /// Simulates a device sending time series signals.
        /// </summary>
        private void AcquisitionThread_DoWork()
        {
            _t = 0;
            double dTMs = 1.0 / _samplingRateHz;
            double y;
            while (_acquisitionThreadRunning)
            {
                _t += dTMs;
                y = Math.Sin(_t * 2 * Math.PI * _signalFrequencyHz) * _signalAmplitude;
                OnDataAvailable(_deviceName, new double[1] {y});
                Thread.Sleep((int)(dTMs * 1000));
            }
        }
    }
}
