# DAQConnector
This is a very basic example showing how to receive time series data from different devices using a common interface.

## Interface: DAQConnector
Implement the interface IDAQ for your own devices. The DAQSimulator shows how to implement the interface.

## Example: DAQConnectorTester
This example shows how to receive data from multiple devices with different samplingRates, signal frequencies.<br/><br/>
![alt text](https://raw.githubusercontent.com/MartinWalchshofer/DAQConnector/main/DAQConnectorTester.png "DAQConnectorTester")

## Example: DAQConnectorUnityTester
This example shows how to receive data from multiple devices with different samplingRates, signal frequencies in Unity.<br/><br/>
![alt text](https://raw.githubusercontent.com/MartinWalchshofer/DAQConnector/main/DAQConnectorTesterUnity.png "DAQConnectorTesterUnity")

## Prerequisites
Visual Studio 2022
.NET Framework 4.8
Unity 2022.3.11f1

## How to build 'DAQConnector.dll'
- Open 'DAQConnector.sln'
- Build 'DAQConnector'
- 'DAQConnector.dll' is created in the target folder

## How to use 'DAQConnector.dll' in Unity
- Copy 'DAQConnector.dll' to your assets folder
- Open the Visual Studio soluion from Unity
- Namespaces and classes from 'DAQConnector.dll' should be visible and usable now