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
        // Create a Bluetooth client
        BluetoothClient bluetoothClient = new BluetoothClient();
        // Discover devices
        var devices = bluetoothClient.DiscoverDevices();

        // Display information about discovered devices
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
            // Create a Bluetooth client
            BluetoothClient bluetoothClient = new BluetoothClient();

            // Create a Bluetooth address from the device address string
            BluetoothAddress bluetoothAddress = device.DeviceAddress;

            // Set a timeout for the connection attempt (e.g., 10 seconds)
            int timeout = 10000; // 10 seconds

            // Attempt to connect to the specified Bluetooth device with a timeout
            bluetoothClient.Connect(bluetoothAddress, BluetoothService.SerialPort);

            // Connection successful
            Console.WriteLine($"Connected to Bluetooth device at address: {device.DeviceAddress} - {device.Authenticated} - {device.Connected}");

            // Perform your communication with the device here...
            Console.Read();
            // Close the Bluetooth client when done
            bluetoothClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to Bluetooth device: {ex.Message}");
        }
    }

}
