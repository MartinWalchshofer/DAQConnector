using System;
using System.Collections.Generic;

namespace DAQConnector
{
    public interface IDAQ
    {
        /// <summary>
        /// Event called whenever new data is available
        /// </summary>
        event EventHandler<double[]> OnDataAvailable;

        /// <summary>
        /// Opens a device by a given name.
        /// </summary>
        /// <param name="deviceName">The device name as string.</param>
        void Open(string deviceName);

        /// <summary>
        /// Closes an opened device.
        /// </summary>
        void Close();
    }
}
