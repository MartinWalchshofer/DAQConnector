using DAQConnector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DAQConnectorTester : MonoBehaviour
{
    List<IDAQ> _devices = null;

    void Start()
    {
        _devices = new List<IDAQ>();

        //get available devices
        List<string> devices = DAQSimulator.GetAvailableDevices();

        //add devices
        _devices.Add(new DAQSimulator(250, 10, 10));
        _devices.Add(new DAQSimulator(50, 100, 20));
        _devices.Add(new DAQSimulator(10, 100, 20));
        _devices.Add(new DAQSimulator(5, 100, 20));
        _devices.Add(new DAQSimulator(1, 100, 20));

        //attach to data available event
        foreach (IDAQ device in _devices)
            device.OnDataAvailable += DataAvailable;

        //open device / start data acquisition
        for (int i = 0; i < _devices.Count; i++)
            _devices[i].Open(devices[i]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        //close devices
        foreach (IDAQ device in _devices)
            device.Close();

        //detach from data available event / stop data acquisition
        foreach (IDAQ device in _devices)
            device.OnDataAvailable -= DataAvailable;

        _devices.Clear();

        GC.Collect();
    }

    private void DataAvailable(object sender, double[] e)
    {
        if (sender == null || e == null)
            Debug.Log("Invalid data received.");

        if (sender != null && e != null)
            Debug.Log(String.Format("Device: {0} \t Data: [{1}]", sender.ToString(), string.Join(",", e)));
    }
}
