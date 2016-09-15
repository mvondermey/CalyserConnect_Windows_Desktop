using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Diagnostics;

// State object for receiving data from remote device.
public class StateObject
{
    // Client socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 256;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
}


namespace calyserconnect {
    public class Client
    {
        // The port number for the remote device.
        private const int port = 8081;

        // ManualResetEvent instances signal completion.

        private static ManualResetEvent allDone =
            new ManualResetEvent(false);

        public Client()
        {
            Debug.WriteLine(new MessageJSON().GetJSON());
            Debug.WriteLine("Start Client Thread");
            Thread serverThread = new Thread(StartClient);
            serverThread.Start();
            Debug.WriteLine("Done Client Thread");
        }

        // The response from the remote device.
        private static String response = String.Empty;

        private static void StartClient()
        {

            //
            Socket client = null;
            //
            // Connect to a remote device.
            try
            {

                // Get the DNS IP addresses associated with the host.
                IPAddress[] IPaddresses = Dns.GetHostAddresses("localhost");
                //

                allDone.Reset();

                    Debug.WriteLine("Create Socket ");

                    // Creates the Socket to send data over a TCP connection.
                    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    Debug.WriteLine("Doing connect ");

                    // Connect to the remote endpoint.
                    Debug.WriteLine("Connecting ");
                    Debug.WriteLine("Sent Async ");
                    client.BeginConnect(IPaddresses,port,
                        new AsyncCallback(ConnectCallback), client);
                    allDone.WaitOne();
                    //Debug.WriteLine("Connected...");

            }
            catch (Exception e)
            {
                Debug.WriteLine("Connect Exception");
                Debug.WriteLine(e.ToString());
            }
        }

        public static void ConnectCallback(IAsyncResult ar)
        {
            Debug.WriteLine("ConnectCallback.Start Thread");
            Socket client = (Socket)ar.AsyncState;
            //
            try
            {
                client.EndConnect(ar);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Connect Exception");
                Debug.WriteLine(e.ToString());
                return;
            }
            //
            Thread serverThread = new Thread(() => new SocketTask(client));
            serverThread.Start();
            Debug.WriteLine("ConnectCallback.Done Thread");
            //allDone.Set();
        }

    }
}
