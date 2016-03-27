using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace calyserconnect
{
    class Server
    {
        static string output = "";
        byte[] bytesSent;

        public Server()
        {
            StartListening();
        }

    // Thread signal.
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public static void StartListening() {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        // The DNS name of the computer
        // running the listener is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve("localhost");
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        Debug.WriteLine("ipAdsress "+ipAddress);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8081);

        // Create a TCP/IP socket.
        Socket listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp );

        // Bind the socket to the local endpoint and listen for incoming connections.
        try {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true) {
                // Set the event to nonsignaled state.
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept( 
                    new AsyncCallback(AcceptCallback),
                    listener );

                // Wait until a connection is made before continuing.
                allDone.WaitOne();
            }

        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }

    public static void AcceptCallback(IAsyncResult ar) {
        Debug.WriteLine("AccetCallback.Start Thread");
        Thread serverThread = new Thread(() => new SocketTask(ar));
        serverThread.Start();
        Debug.WriteLine("AcceptCallback.Done Thread");
        allDone.Set();
    }
//
}
    }

