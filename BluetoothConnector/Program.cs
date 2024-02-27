using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using InTheHand.Net.Sockets;

class Program
{
    static void Main()
    {
        ListBluetoothDevices();
    }

    static void ListBluetoothDevices()
    {
        BluetoothClient bluetoothClient = new BluetoothClient();

        var devices = bluetoothClient.DiscoverDevices();

        Console.WriteLine("Discovered Bluetooth Devices:");
        var device1 = devices.FirstOrDefault(d => d.DeviceAddress.ToString() == "50F9589F53F6");
        foreach (BluetoothDeviceInfo device in devices)
        {
            Console.WriteLine($"Device Name: {device.DeviceName}");
            Console.WriteLine($"Device Address: {device.DeviceAddress}");
            Console.WriteLine($"Class of Device: {device.ClassOfDevice}");
            Console.WriteLine($"Connected: {device.Connected}");
            Console.WriteLine();
        }
        ConnectToBluetoothDevice(device1);
    }
    static void ConnectToBluetoothDevice(BluetoothDeviceInfo device)
    {
        try
        {
            BluetoothSecurity.PairRequest(device.DeviceAddress, null);
            BluetoothClient bluetoothClient = new BluetoothClient();

            BluetoothAddress bluetoothAddress = device.DeviceAddress;

            int timeout = 10000;

            bluetoothClient.Connect(bluetoothAddress, BluetoothService.SerialPort);

            Console.WriteLine($"Connected to Bluetooth device at address: {device.DeviceAddress} - {device.Authenticated} - {device.Connected}");

            Console.Read();
            bluetoothClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to Bluetooth device: {ex.Message}");
        }
    }

}
